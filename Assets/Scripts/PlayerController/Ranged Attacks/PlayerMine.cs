using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;


public class PlayerMine : MonoBehaviour
{
    private PlayerRangedMine prm;

    [Header("Bullet Variables")]
    public float rotationSpeed;

    [Header("Explosion Variables")]
    public float detonationBuffer;
    public ParticleSystem explosionFX;
    public Collider explosionCollider;
    public AudioSource explosionSFX;

    [Header("Booleans")]
    public bool landed = false;
    public bool exploded = false;
    public bool canTrack = true;

    [Header("Boss Detection")]
    public LayerMask bossLayer;
    public float bossDetectionMin;
    public float bossDetectionMax;
    
    private GameObject boss;

    // Update is called once per frame
    void Update()
    {
        if (!landed)
        {
            if (SearchForBoss() && canTrack)
            {
                Quaternion lookTarget = Quaternion.LookRotation(GetDirectionToBoss());
                transform.rotation = Quaternion.Slerp(transform.rotation, lookTarget, rotationSpeed * Time.deltaTime);
                Debug.Log("Tracking");
                MoveMine(prm.speed);
            }
            else
            {
                MoveMine(prm.speed);
            }
        }
    }

    public void InitBullet(PlayerRangedMine attack)
    {
        transform.parent = null;
        prm = attack;
        boss = gm.bossRef;

        transform.forward = prm.shootDirection;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.tag.Equals("Player"))
        {
            Detonate();
            landed = true;
        }
    }
    public void Detonate()
    {
        SpawnExplosion();
        exploded = true;
    }
    public void SpawnExplosion()
    {
        explosionCollider.enabled = true;
        transform.rotation = Quaternion.identity;

        explosionSFX?.Play();
        if (explosionFX)
        {
            explosionFX.Play();
            Destroy(gameObject, 0.5f);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void MoveMine(float speed)
    {
        if (prm.rb != null) 
        {
            prm.rb.velocity = transform.forward * speed;

        }
    }
    public Vector3 GetDirectionToBoss()
    {
        Vector3 bossPosition = boss.transform.position;
        bossPosition.y += 1;
        return (bossPosition - transform.position).normalized;
    }

    public float GetDistanceToBoss()
    {
        return Vector3.Distance(transform.position, boss.transform.position);
    }

    private bool SearchForBoss()
    {
        float distance = GetDistanceToBoss();

        if (distance < bossDetectionMax && distance > bossDetectionMin)
        {
            return true;
        }
        else
        {
            if (distance < bossDetectionMin)
            {
                canTrack = false;
            }

            return false;
        }
    }
}
