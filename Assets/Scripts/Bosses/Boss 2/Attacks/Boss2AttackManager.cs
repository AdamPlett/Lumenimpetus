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
    public float specialLaserCooldown;
    private float specialLaserTimer;
    public LayerMask laserHitMask;
    public LayerMask laserStopMask;
    public bool shootingLaser;
    private bool mainLaser;

    [Header("Portals")]
    public GameObject mainPortal;

    [Header("Attack Points")]
    public GameObject[] attack1Portals;
    public GameObject[] attack2Portals;
    public GameObject[] attack3Portals;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!shootingLaser)
        {
            if (basicLaserTimer >= basicLaserCooldown)
            {
                basicLaserTimer = 0;
                StartCoroutine(BasicLaser());
                return;
            }
            else
            {
                //basicLaserTimer += Time.deltaTime;
            }

            if (specialLaserTimer >= specialLaserCooldown && gm.boss2.onGroundLayer)
            {
                specialLaserTimer = 0;
                StartCoroutine(SpecialAttack2());
                //SelectRandomSpecial();
                return;
            }
            else
            {
                specialLaserTimer += Time.deltaTime;
            }

            if(mainLaser)
            {
                DrawLaserMain();
            }
        }
    }

    private void SelectRandomSpecial()
    {
        int randomInt = Random.Range(0, 3);

        if(randomInt == 0)
        {
            StartCoroutine(SpecialAttack1());
        }
        else if(randomInt == 1)
        {
            StartCoroutine(SpecialAttack2());
        }
        else if (randomInt == 2)
        {
            StartCoroutine(SpecialAttack3());
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

    IEnumerator SpecialAttack1()
    {
        gm.boss2.anim.SwitchAnimation(gm.boss2.anim.LaserHash);

        yield return new WaitForSeconds(0.5f);

        ActivateMainPortal();
        ActivateSpecialPortals(attack1Portals);

        yield return new WaitForSeconds(1f);

        PortalLaser[] portalLasers = new PortalLaser[attack1Portals.Length];

        for (int i = 0; i < portalLasers.Length; i++)
        {
            portalLasers[i] = attack1Portals[i].GetComponent<PortalLaser>();
        }

        foreach(var portalLaser in portalLasers)
        {
            Vector3 directionToPlayer = gm.playerRef.transform.position - portalLaser.gameObject.transform.position;
            Vector3 target = gm.playerRef.transform.position + directionToPlayer;

            portalLaser.ActivateLaser();
            shootingLaser = true;

            float timer = 0f;

            while (timer < 1f)
            {
                timer += Time.deltaTime * 2f;

                if (timer > 1f)
                {
                    timer = 1f;
                }

                Vector3 laserEnd = Vector3.Lerp(laserSpawnPoint.position, target, timer);
                portalLaser.target.position = laserEnd;

                yield return null;
            }

            portalLaser.DeactivateLaser();
            shootingLaser = false;

            yield return new WaitForSeconds(0.5f);
        }

        DeactivateSpecialPortals(attack1Portals);
        DeactivateMainPortal();

        gm.boss2.anim.SwitchAnimation(gm.boss2.anim.IdleHash);
    }

    IEnumerator SpecialAttack2()
    {
        gm.boss2.anim.SwitchAnimation(gm.boss2.anim.LaserHash);

        yield return new WaitForSeconds(0.5f);

        ActivateMainPortal();
        ActivateSpecialPortals(attack2Portals);

        yield return new WaitForSeconds(1f);

        PortalLaser[] portalLasers = new PortalLaser[attack2Portals.Length];

        for (int i = 0; i < portalLasers.Length; i++)
        {
            portalLasers[i] = attack2Portals[i].GetComponent<PortalLaser>();
        }

        foreach (var portalLaser in portalLasers)
        {
            Vector3 directionToPlayer = gm.playerRef.transform.position - portalLaser.gameObject.transform.position;
            Vector3 target = gm.playerRef.transform.position + directionToPlayer;

            portalLaser.ActivateLaser();
            shootingLaser = true;

            float timer = 0f;

            while (timer < 1f)
            {
                timer += Time.deltaTime * 2f;

                if (timer > 1f)
                {
                    timer = 1f;
                }

                Vector3 laserEnd = Vector3.Lerp(laserSpawnPoint.position, target, timer);
                portalLaser.target.position = laserEnd;

                yield return null;
            }

            portalLaser.DeactivateLaser();
            shootingLaser = false;

            yield return new WaitForSeconds(0.5f);
        }

        DeactivateSpecialPortals(attack2Portals);
        DeactivateMainPortal();

        gm.boss2.anim.SwitchAnimation(gm.boss2.anim.IdleHash);
    }

    IEnumerator SpecialAttack3()
    {
        gm.boss2.anim.SwitchAnimation(gm.boss2.anim.LaserHash);

        yield return new WaitForSeconds(1f);

        shootingLaser = true;
        ActivateSpecialPortals(attack3Portals);

        yield return new WaitForSeconds(1f);

        PortalLaser[] portalLasers = new PortalLaser[attack3Portals.Length];

        for(int i = 0; i < portalLasers.Length; i++)
        {
            portalLasers[i] = attack3Portals[i].GetComponent<PortalLaser>();
            portalLasers[i].ActivateLaser();
        }

        float timer = 0f;

        while (timer < 1f)
        {
            timer += Time.deltaTime;

            if (timer > 1f)
            {
                timer = 1f;
            }

            foreach (var portalLaser in portalLasers)
            {
                Vector3 laserEnd = Vector3.Lerp(laserSpawnPoint.position, portalLaser.gameObject.transform.forward * 25f, timer);
                portalLaser.target.position = laserEnd;
            }

            yield return null;
        }

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < portalLasers.Length; i++)
        {
            portalLasers[i].DeactivateLaser();
        }

        DeactivateSpecialPortals(attack3Portals);
        shootingLaser = false;

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

    private void DrawLaserMain()
    {
        laser.DrawLaser(laserSpawnPoint.position, mainPortal.transform.position);
    }

    private void ActivateMainPortal()
    {
        mainPortal.SetActive(true);
        ActivateLaser();
        laserTarget = mainPortal.transform.position;
        mainLaser = true;
    }

    private void DeactivateMainPortal()
    {
        mainPortal.SetActive(false);
        DeactivateLaser();
        mainLaser = false;
    }

    private void ActivateSpecialPortals(GameObject[] portals)
    {
        foreach(var portal in portals)
        {
            portal.SetActive(true);
        }
    }

    private void DeactivateSpecialPortals(GameObject[] portals)
    {
        foreach (var portal in portals)
        {
            portal.SetActive(false);
        }
    }
}
