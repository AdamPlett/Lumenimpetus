using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedBasic : RangedAttack
{
    GameObject bulletInstance;
    public Rigidbody rb;
    public float shotTimer = 0f;
    

    private void Start()
    {
        

    }
    // Update is called once per frame
    private void Update()
    {
        base.Update();
        if (shooting)
        {
            MoveBullet();
            shotTimer += Time.deltaTime;
            if (shotTimer >= duration)
            {
                Destroy(bulletInstance);
                ResetBullet();
            }
        }
    }
    public override void Shoot(Vector3 targetPosition)
    {
        if (targetPosition != null)
        {
            shootDirection = GetDirection(startPosition.position, targetPosition);
        }
        else
        {
            shootDirection = startPosition.up;
        }
        StartCoroutine(ShotDelay());
        SpawnBullet();
        ResetTimer();
        
    }
    private IEnumerator ShotDelay()
    {
        firing = true;
        yield return new WaitForSeconds(shotDelay);
        firing = false;
        shooting = true;
        Debug.Log("Bang");
    }

    public void SpawnBullet()
    {
        bulletInstance = Instantiate(bulletPrefab, startPosition.position, Quaternion.Euler(startPosition.up));
        bulletInstance.transform.parent = null;
        rb = bulletInstance.GetComponent<Rigidbody>();
        bulletInstance.GetComponent<Bullet>().InitBullet(this);
    }

    public void MoveBullet()
    {
        if (rb != null) rb.velocity = shootDirection.normalized * speed * Time.deltaTime;
    }
    
    public void ResetBullet()
    {
        shooting = false;
        shotTimer = 0;
    }

}
