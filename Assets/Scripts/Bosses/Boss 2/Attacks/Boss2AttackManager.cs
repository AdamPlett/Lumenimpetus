using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading;
using UnityEngine;
using static GameManager;

public class Boss2AttackManager : MonoBehaviour
{
    [Header("Lasers")]
    public Laser laser;
    public Transform laserSpawnPoint;
    private Vector3 laserTarget;
    public float basicLaserCooldown;
    private float basicLaserTimer;
    public LayerMask laserHitMask;
    public LayerMask laserStopMask;
    public bool shootingLaser;

    [Header("Portals")]
    public GameObject mainPortal;

    [Header("Attack Points")]
    public Transform[] attack1PortalPoints;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(basicLaserTimer >= basicLaserCooldown)
        {
            basicLaserTimer = 0;
            StartCoroutine(BasicLaser());
        }
        else
        {
            basicLaserTimer += Time.deltaTime;
        }
    }

    IEnumerator BasicLaser()
    {
        gm.boss2.anim.SwitchAnimation(gm.boss2.anim.LaserHash);

        yield return new WaitForSeconds(1f);

        ActivateLaser();

        Vector3 directionToPlayer = (gm.playerRef.transform.position - transform.position).normalized;

        Debug.DrawRay(laserSpawnPoint.position, directionToPlayer * 100f, Color.yellow, 1f);

        if (Physics.Raycast(laserSpawnPoint.position, directionToPlayer, out RaycastHit laserHit, 100f, laserStopMask))
        {
            laserTarget = laserHit.point;
        }
        else
        {
            laserTarget = transform.forward * 100f;
        }

        Vector3 laserStart = laserSpawnPoint.position;
        Vector3 laserEnd = laserStart;

        float timer = 0f;

        while(timer < 1f)
        {
            timer += Time.deltaTime;

            if(timer > 1f)
            {
                timer = 1f;
            }

            laserEnd = Vector3.Lerp(laserSpawnPoint.position, laserTarget, timer);
            laser.DrawLaser(laserStart, laserEnd);

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        DeactivateLaser();
        gm.boss2.anim.SwitchAnimation(gm.boss2.anim.IdleHash);
    }

    private void ActivateLaser()
    {
        shootingLaser = true;
        laser.gameObject.SetActive(true);
    }

    private void DeactivateLaser()
    {
        shootingLaser = false;
        laser.gameObject.SetActive(false);
    }

    private void ActivateMainPortal()
    {
        mainPortal.SetActive(true);
    }

    private void DeactivateMainPortal()
    {
        mainPortal.SetActive(false);
    }
}
