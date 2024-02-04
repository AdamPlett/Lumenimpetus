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


    [SerializeField] private float timer;
    public GameObject bulletPrefab;
    public Vector3 startPosition;
    public Vector3 shootDirection;

    public abstract void Shoot(Vector3 targetPosition);

    public virtual void CooldownTimer()
    {
        if (timer < cooldown)
        {
            timer += Time.deltaTime;
        }
        if (timer >= cooldown)
        {
            canShoot = true;
            timer = cooldown;
        }
    }

    public void Update()
    {
        if (!canShoot)
        {
            CooldownTimer();
        }
        else if (canShoot)
        {
            if (Input.GetKeyDown(gm.pm.rangedKey))
            {
                Instantiate(bulletPrefab);

            }
        }

    }

    public virtual void ResetTimer()
    {
        if (canShoot)
        {
            timer = 0;
            canShoot = false;
        }
    }

    public virtual void DamageBoss()
    {
        gm.bh.DamageBoss(damage);
    }

    public virtual void MoveBullet(float speed)
    {

    }
            
}
