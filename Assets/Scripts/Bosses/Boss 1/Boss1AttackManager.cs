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
    [SerializeField] private GameObject energyMine;
    [SerializeField] private GameObject explosiveMine;

    [Header("Grapple Hook")]
    public float grappleRange;
    public float grappleCooldown;
    public float grappleTimer;
    public bool canGrapple;

    [Header("Misc")]
    public LayerMask playerLayer;

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
            foreach (var obj in targets)
            {
                if (obj.tag.Equals("Player"))
                {
                    return true;
                }
            }
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
            foreach (var obj in targets)
            {
                if (obj.tag.Equals("Player"))
                {
                    return true;
                }
            }
        }

        return false;
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
