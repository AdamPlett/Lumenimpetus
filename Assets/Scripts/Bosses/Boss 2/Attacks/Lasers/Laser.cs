using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LineRenderer lineRenderer;

    public void DrawLaser(Transform start, Transform end)
    {
        lineRenderer.SetPosition(0, start.position);
        lineRenderer.SetPosition(1, end.position);
    }
}
