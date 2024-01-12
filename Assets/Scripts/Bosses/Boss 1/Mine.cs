using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Mine : MonoBehaviour
{
    public eMine mineType;

    [Header("Bullet Variables")]
    public float damage;
    public float bulletSpeed;

    [Header("Explosion Variables")]
    public float detonationBuffer;
    public GameObject explosionFX;
    public bool mineLanded = false;

    [Header("Player Detection")]
    public LayerMask playerLayer;
    public float playerDetectionMin;
    public float playerDetectionMax;

    private Vector3 bulletDirection;

    public void InitBullet(Vector3 direction)
    {
        bulletDirection = direction;

        transform.parent = null;
    }

    private void Update()
    {
        if (bulletDirection != null && !mineLanded)
        {
            Collider[] nearbyTargets = Physics.OverlapSphere(transform.position, playerDetectionMin, playerLayer);

            if (nearbyTargets.Length == 0)
            {
                Collider[] fartherTargets = Physics.OverlapSphere(transform.position, playerDetectionMax, playerLayer);

                if (fartherTargets.Length > 0)
                {
                    bulletDirection = (fartherTargets[0].transform.position - transform.position).normalized;
                    transform.position += bulletDirection * bulletSpeed * Time.deltaTime * 0.75f;
                }
                else
                {
                    transform.position += bulletDirection * bulletSpeed * Time.deltaTime;
                }
            }
            else
            {
                Invoke("Detonate", 0.5f);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(mineType == eMine.energy)
        {
            if (!collision.gameObject.tag.Equals("Enemy"))
            {
                Detonate();
                mineLanded = true;
            }
        }
        else if(mineType == eMine.explosive)
        {
            if (!collision.gameObject.tag.Equals("Enemy"))
            {
                if (collision.gameObject.tag.Equals("Player"))
                {
                    Detonate();
                    mineLanded = true;
                }
                else
                {
                    Detonate();
                    mineLanded = true;
                }
            }
        }
    }

    public void Detonate()
    {
        SpawnExplosion();
    }

    public void SpawnExplosion()
    {
        Instantiate(explosionFX, transform);
        Destroy(this.gameObject, 0.1f);
    }
}
