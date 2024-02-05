using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static GameManager;
using static UnityEngine.Rendering.DebugUI;

public class PortalLaser : MonoBehaviour
{
    public Laser laser;
    public Transform target;
    public bool laserActive;

    public void Update()
    {
        if(laserActive && target != null)
        {
            laser.DrawLaser(transform.position, target.position);
        }
    }

    public void ActivateLaser()
    {
        laserActive = true;
        laser.gameObject.SetActive(true);
    }

    public void DeactivateLaser()
    {
        laserActive = false;
        laser.gameObject.SetActive(false);
    }
}
