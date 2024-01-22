using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static GameManager;

public class Mine : MonoBehaviour
{
    public eMine mineType;

    [Header("Bullet Variables")]
    public float damage;
    public float bulletSpeed;
    public float rotationSpeed;
    public Rigidbody bulletRB;

    [Header("Explosion Variables")]
    public float detonationBuffer;
    public ParticleSystem explosionFX;
    public Collider explosionCollider;

    [Header("Booleans")]
    public bool landed = false;
    public bool exploded = false;
    public bool canTrack = true;

    [Header("Player Detection")]
    public LayerMask playerLayer;
    public float playerDetectionMin;
    public float playerDetectionMax;

    private Vector3 bulletDirection;
    private GameObject player;

    public void InitBullet(GameObject playerRef)
    {
        transform.parent = null;
        player = playerRef;

        transform.forward = GetDirectionToPlayer();
    }

    private void Update()
    {
        if (!landed)
        {
            if(SearchForPlayer() && canTrack)
            {
                Quaternion lookTarget = Quaternion.LookRotation(GetDirectionToPlayer());
                transform.rotation = Quaternion.Slerp(transform.rotation, lookTarget, rotationSpeed * Time.deltaTime);

                MoveMine(bulletSpeed);
            }
            else
            {
                MoveMine(bulletSpeed);
            }
        }
    }

    private bool SearchForPlayer()
    {
        float distance = GetDistanceToPlayer();

        if(distance < playerDetectionMax && distance > playerDetectionMin)
        {
            return true;
        }
        else
        {
            if(distance < playerDetectionMin)
            {
                canTrack = false;
            }
            
            return false;
        }
    }

    private void MoveMine(float speed)
    {
        bulletRB.velocity = transform.forward * speed;
    }

    public Vector3 GetDirectionToPlayer()
    {
        return (player.transform.position - transform.position).normalized;
    }

    public float GetDistanceToPlayer()
    {
        return Vector3.Distance(transform.position, player.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(mineType == eMine.energy)
        {
            if (!collision.gameObject.tag.Equals("Enemy"))
            {
                Detonate();
                landed = true;
                if (collision.gameObject.tag.Equals("Player"))
                {
                    gm.ph.DamagePlayer(damage);
                }
            }

        }
        else if(mineType == eMine.explosive)
        {
            if (!collision.gameObject.tag.Equals("Enemy"))
            {
                landed = true;

                if (collision.gameObject.tag.Equals("Player"))
                {
                    Detonate();
                    gm.ph.DamagePlayer(damage);
                }
                else
                {
                    Detonate();
                }
            }
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
        
        if(explosionFX)
        {
            explosionFX.Play();
            Destroy(gameObject, 0.5f);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
