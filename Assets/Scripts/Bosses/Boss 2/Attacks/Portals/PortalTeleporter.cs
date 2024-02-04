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
        // Get direction of portal to player
        Vector3 portalToPlayer = (player.position - transform.position).normalized;

        Vector3 faceDirection = destination.forward + portalToPlayer;

        Debug.DrawRay(destination.position, faceDirection, Color.green, 5f);

        // Set player velocity to portal face direction
        float magnitude = Vector3.Magnitude(gm.pm.playerVelocity);
        gm.pm.rb.velocity = magnitude * destination.forward;

        // adjust rotation
        Quaternion lookRotation = Quaternion.LookRotation(destination.forward, destination.up);
        Vector3 eulerAngles;
        eulerAngles = lookRotation.eulerAngles;

        gm.pm.cam.xRotation = eulerAngles.x;
        gm.pm.cam.yRotation = eulerAngles.y;

        // adjust position
        player.position = destination.position + (destination.forward);
        Debug.Log(lookRotation);
        Invoke("SetTeleportingFalse", 0.25f);
    }

    private void SetTeleportingTrue()
    {
        gm.pm.teleporting = true;
    }

    private void SetTeleportingFalse()
    {
        gm.pm.teleporting = false;
    }
}
