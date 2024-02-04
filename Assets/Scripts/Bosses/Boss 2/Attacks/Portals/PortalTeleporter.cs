using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
            SetTeleportingTrue();
            TeleportPlayer();

            Debug.Log("Player has entered the portal");
        }
    }

    private void TeleportPlayer()
    {
        Vector3 portalToPlayer = player.position - transform.position;

        // Set player velocity to portal face direction
        float magnitude = Vector3.Magnitude(gm.pm.playerVelocity);
        gm.pm.rb.velocity = magnitude * destination.forward;

        // adjust rotation
        Quaternion lookRotation = Quaternion.LookRotation(destination.forward, destination.up);
        Vector3 eulerAngles;
        eulerAngles = lookRotation.eulerAngles;

        gm.pm.cam.xRotation = eulerAngles.x;
        gm.pm.cam.yRotation = eulerAngles.y;
        
        gm.pm.cam.camHolder.rotation = lookRotation;
        gm.pm.cam.orientation.rotation = Quaternion.Euler(0, eulerAngles.y, 0);

        // adjust position
        player.position = destination.position + (destination.forward);
        Debug.Log(lookRotation);
        Invoke("SetTeleportingFalse", 0.25f);
    }

    private void SetTeleportingTrue()
    {
        gm.pm.teleporting = true;
        gm.pm.cam.locked = true;
    }

    private void SetTeleportingFalse()
    {
        gm.pm.teleporting = false;
        gm.pm.cam.locked = false;
    }
}
