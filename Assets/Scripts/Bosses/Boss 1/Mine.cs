using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public eMine mineType;
    public float damage;
    public float bulletSpeed;
    public float detonationBuffer;
    public GameObject explosionFX;

    [Header("Player Detection")]
    public LayerMask playerLayer;
    public float playerDetectionRange;

    private Vector3 bulletDirection;

    public void InitBullet(Vector3 direction)
    {
        bulletDirection = direction;

        transform.parent = null;
    }

    private void Update()
    {
        if (bulletDirection != null)
        {
            Collider[] targets = Physics.OverlapSphere(transform.position, playerDetectionRange, playerLayer);

            if (targets.Length > 0)
            {
                bulletDirection = (targets[0].transform.position - transform.position).normalized;
            }

            transform.position += bulletDirection * bulletSpeed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            Detonate();
        }
    }

    public void Detonate()
    {
        SpawnExplosion();
    }

    public void SpawnExplosion()
    {
        Destroy(this.gameObject);
    }
}
