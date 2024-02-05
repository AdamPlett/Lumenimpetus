using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class Laser : MonoBehaviour
{
    public LineRenderer lineRenderer;

    public void DrawLaser(Vector3 start, Vector3 end)
    {
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }

    public void CastCollider()
    {
        Vector3 direction = lineRenderer.GetPosition(1) - lineRenderer.GetPosition(0);
        
        if(Physics.CapsuleCast(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1), 5f, direction, out RaycastHit hit))
        {
            if(hit.collider.tag.Equals("Player"))
            {
                gm.ph.DamagePlayer(7.5f);
            }
        }
    }
}
