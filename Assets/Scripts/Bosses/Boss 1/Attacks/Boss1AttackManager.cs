using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public enum eMine { energy, explosive }

public class Boss1AttackManager : MonoBehaviour
{
    [Header("Energy Sword")]
    [SerializeField] private GameObject blade;

    [Space(8)]
    public float meleeRange;
    public float meleeDamage;
    public float meleeCooldown;
    public float meleeTimer;
    public bool canMelee;
    [Space(8)]
    public int comboCounter;

    [Header("Cannon")]
    public float cannonRangeMin;
    public float cannonRangeMax;
    public float cannonCooldown;
    public float cannonTimer;
    public bool canShoot;
    [Space(8)]
    public eMine currentAmmo;
    public GameObject energyMine;
    public GameObject explosiveMine;
    public Transform bulletSpawnPoint;
    public AudioSource cannonShotSFX;

    [Header("Grapple Hook - Grapple")]
    public LayerMask grappleLayer;
    public Transform grappleTarget;
    public Transform prevTarget;
    public LineRenderer lineRender;
    [Space(10)]
    public GameObject grappleBullet;
    public GrappleBullet bulletScript;
    public bool swinging;
    public bool pulling;
    public bool noHit;
    [Space(8)]
    public float grappleCooldown;
    public float grappleTimer;
    public bool canGrapple;
    [Space(8)]
    public float grappleRangeMin;
    public float grappleRangeMax;
    public float grappleSpeed;
    [Space(8)]
    public float pullRangeMin;
    public float pullRangeMax;
    public float pullSpeed;
    public float slamSpeed;

    [Header("Misc")]
    public LayerMask playerLayer;
    public GameObject playerRef;

    public void Update()
    {
        SetCanMelee();
        SetCanShoot();
        SetCanGrapple();
    }

    #region Melee

    public void SetCanMelee()
    {
        if (meleeTimer < meleeCooldown)
        {
            meleeTimer += Time.deltaTime;
            canMelee = false;
        }
        else
        {
            meleeTimer = meleeCooldown;
            canMelee = true;
        }
    }

    public bool CheckMeleeRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerRef.transform.position);

        if(distanceToPlayer < meleeRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ActivateBlade()
    {
        blade.SetActive(true);
    }

    public void DeactivateBlade()
    {
        blade.SetActive(false);
    }

    #endregion

    #region Cannon

    public void SetCanShoot()
    {
        if (cannonTimer < cannonCooldown)
        {
            cannonTimer += Time.deltaTime;
            canShoot = false;
        }
        else
        {
            cannonTimer = cannonCooldown;
            canShoot = true;
        }
    }

    public bool CheckCannonRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerRef.transform.position);

        if(distanceToPlayer < cannonRangeMax && distanceToPlayer > cannonRangeMin)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Shoot()
    {
        cannonShotSFX?.Play();
        if(currentAmmo == eMine.energy)
        {
            GameObject mine = Instantiate(energyMine, bulletSpawnPoint);
            mine.GetComponent<Mine>().InitBullet(playerRef);
        }
        else if (currentAmmo == eMine.explosive)
        {
            GameObject mine = Instantiate(explosiveMine, bulletSpawnPoint);
            mine.GetComponent<Mine>().InitBullet(playerRef);
        }
    }

    #endregion

    #region Grapple

    public void SetCanGrapple()
    {
        if (grappleTimer < grappleCooldown)
        {
            grappleTimer += Time.deltaTime;
            canGrapple = false;
        }
        else
        {
            grappleTimer = grappleCooldown;
            canGrapple = true;
        }
    }

    public bool CheckGrappleRange()
    {
        Collider[] grapplePoints = Physics.OverlapSphere(transform.position, grappleRangeMax, grappleLayer);

        if(grapplePoints.Length > 0 )
        {
            foreach(var point in grapplePoints)
            {
                if(point.gameObject.transform != grappleTarget)
                {
                    prevTarget = grappleTarget;
                    grappleTarget = point.transform;
                    return true;
                }
            }
        }

        return false;
    }

    public bool CheckPullRange()
    {
        Collider[] grapplePoints = Physics.OverlapSphere(transform.position, pullRangeMax, playerLayer);

        if (grapplePoints.Length > 0)
        {
            grappleTarget = grapplePoints[0].transform;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ShootGrapple()
    {
        ActivateGrapple();
        
        grappleBullet.SetActive(true);

        grappleBullet.transform.parent = lineRender.transform;
        grappleBullet.transform.position = lineRender.transform.position;
        bulletScript.InitBullet(gm.playerRef.transform);
    }

    public void ActivateGrapple()
    {
        lineRender.enabled = true;
    }

    public void DeactivateGrapple()
    {
        lineRender.enabled = false;
    }

    public void StartSwing()
    {
        swinging = true;
    }

    public void EndSwing()
    {
        swinging = false;
    }

    #endregion
}
