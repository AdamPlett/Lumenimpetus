using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class Laser : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public LayerMask playerMask;

    public void DrawLaser(Vector3 start, Vector3 end)
    {
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);

        CastCollider();
    }

    public void CastCollider()
    {
        Vector3 direction = lineRenderer.GetPosition(1) - lineRenderer.GetPosition(0);
        float distance = Vector3.Distance(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1));

        if(Physics.SphereCast(lineRenderer.GetPosition(0), 1f, direction, out RaycastHit hit, distance, playerMask))
        {
            if(hit.collider.tag.Equals("Player"))
            {
                gm.ph.DamagePlayer(3f);
            }
        }
    }
}
