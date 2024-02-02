using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;
using static UnityEngine.Rendering.DebugUI;

public class PortalTeleporter : MonoBehaviour
{
    public Transform player;
    public Transform destination;

    public bool playerOverlapping = false;

    public void Start()
    {
        player = gm.playerRef.transform;
    }

    public void InitPortal(Transform linkedPortal)
    {
        player = gm.playerRef.transform;
        destination = linkedPortal;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player") && !gm.pm.teleporting)
        {
            gm.pm.teleporting = true;
            TeleportPlayer();

            Debug.Log("Player has entered the portal");
        }
    }

    private void TeleportPlayer()
    {
        Vector3 portalToPlayer = player.position - transform.position;

        // Set player velocity to portal face direction
        float magnitude = Vector3.Magnitude(gm.pm.playerVelocity);
        gm.pm.playerVelocity = magnitude * transform.up * -1f;

        // adjust rotation
        gm.pm.cam.xRotation = destination.rotation.x;
        gm.pm.cam.yRotation = destination.rotation.y;

        // adjust position
        player.position = destination.position + (transform.up * -2f);

        Invoke("SetTeleportingFalse", 0.5f);
    }

    private void SetTeleportingFalse()
    {
        gm.pm.teleporting = false;
    }
}
