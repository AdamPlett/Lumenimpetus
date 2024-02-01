using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class PortalTeleporter : MonoBehaviour
{
    public Transform player;
    public Transform destination;

    private bool playerOverlapping = false;

    public void InitPortal(Transform linkedPortal)
    {
        player = gm.playerRef.transform;
        destination = linkedPortal;
    }

    private void Update()
    {
        if(playerOverlapping)
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dot = Vector3.Dot(transform.up, portalToPlayer);

            // If true, this means player has crossed the portal
            if(dot < 0f)
            {
                float rotationOffset = Quaternion.Angle(transform.rotation, destination.rotation) + 180f;
                player.Rotate(Vector3.up, rotationOffset);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationOffset, 0f) * portalToPlayer;
                player.position = destination.position + positionOffset;

                playerOverlapping = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            playerOverlapping = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            playerOverlapping = false;
        }
    }
}
