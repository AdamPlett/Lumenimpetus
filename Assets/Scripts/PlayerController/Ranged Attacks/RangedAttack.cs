using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public enum EWeapons 
{ 
    none,
    basic,
    mine,
    laser
};

public abstract class RangedAttack : MonoBehaviour
{
    public EWeapons currentWeapon;
    public float damage;
    public float speed;
    public float duration;
    public float cooldown;
    public float shotDelay;
    public bool canShoot;
    public bool shooting;
    public bool firing;
    public bool bulletSpawned;


    [SerializeField] private float cooldownTimer;
    public GameObject bulletPrefab;
    public Transform startPosition;
    public Vector3 shootDirection;
    public bool hitTarget;
    [Header("UI")]
    public ImageAnimation canShootAnim;

    public abstract void Shoot(Vector3 targetPosition);

    public virtual void CooldownTimer()
    {
        if (cooldownTimer < cooldown)
        {
            cooldownTimer += Time.deltaTime;
        }
        if (cooldownTimer >= cooldown)
        {
            canShoot = true;
            cooldownTimer = cooldown;
        }
    }

    public void Update()
    {
        if (!canShoot)
        {
            CooldownTimer();
            canShootAnim.setActiveFalse();
        }
        else
        {
            Debug.Log(canShoot);
            canShootAnim.setActiveTrue();
        }
    }

    public virtual void ResetTimer()
    {
        if (canShoot)
        {
            cooldownTimer = 0;
            canShoot = false;
        }
    }

    public virtual void DamageBoss()
    {
        gm.bh.DamageBoss(damage + (damage * gm.pm.comboMultiplier));
        gm.pm.Combo();

    }

    //gets target to shoot at
    public Vector3 GetShotTarget()
    {
        hitTarget = Physics.Raycast(gm.pm.cam.transform.position, gm.pm.cam.transform.forward, out RaycastHit hit);
        return hit.point;
    }
    //returns direction to shoot at
    public virtual Vector3 GetDirection(Vector3 start, Vector3 target)
    {
        return (target - start).normalized;
    }
}
