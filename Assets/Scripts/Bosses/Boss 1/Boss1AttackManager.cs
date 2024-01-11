using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float cannonRange;
    public float cannonCooldown;
    public float cannonTimer;
    public bool canShoot;
    [Space(8)]
    public eMine currentAmmo;
    public GameObject energyMine;
    public GameObject explosiveMine;
    public Transform bulletSpawnPoint;

    [Header("Grapple Hook")]
    public float grappleRange;
    public float grappleCooldown;
    public float grappleTimer;
    public bool canGrapple;

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
        Collider[] targets = Physics.OverlapSphere(transform.position, meleeRange, playerLayer);

        if (targets.Length > 0)
        {
            return true;
        }

        return false;
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
            cannonTimer = meleeCooldown;
            canShoot = true;
        }
    }

    public bool CheckCannonRange()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, cannonRange, playerLayer);

        if (targets.Length > 0)
        {
            return true;
        }

        return false;
    }

    public void Shoot()
    {
        Vector3 directionToPlayer = (playerRef.transform.position - transform.position).normalized;
        
        if(currentAmmo == eMine.energy)
        {
            GameObject mine = Instantiate(energyMine, bulletSpawnPoint);
            mine.GetComponent<Mine>().InitBullet(directionToPlayer);
        }
        else if (currentAmmo == eMine.energy)
        {
            GameObject mine = Instantiate(explosiveMine, bulletSpawnPoint);
            mine.GetComponent<Mine>().InitBullet(directionToPlayer);
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
            grappleTimer = meleeCooldown;
            canGrapple = true;
        }
    }


    #endregion
}
