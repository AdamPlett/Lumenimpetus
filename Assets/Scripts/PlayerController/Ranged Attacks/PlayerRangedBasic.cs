using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class PlayerRangedBasic : RangedAttack
{
    GameObject bulletInstance;
    private Rigidbody rb;
    public float shotTimer = 0f;
    public AudioClip shotSFX;
    
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
        StartCoroutine(ShotDelay(targetPosition));
        ResetTimer();
    }
    private IEnumerator ShotDelay(Vector3 targetPosition)
    {
        firing = true;
        yield return new WaitForSeconds(shotDelay);
        //gets position to shoot at
        if (targetPosition != null)
        {
            shootDirection = GetDirection(startPosition.position, targetPosition);
        }
        else
        {
            shootDirection = startPosition.up;
        }
        SpawnBullet();

        //play shot SFX
        gm.pm.swordSFX.PlayOneShot(shotSFX);

        Invoke(nameof(AnimationBuffer), 1f);
        shooting = true;
        Debug.Log("Bang");
    }

    private void AnimationBuffer()
    {

        firing = false;
    }

    public void SpawnBullet()
    {
        bulletInstance = Instantiate(bulletPrefab, startPosition.position, Quaternion.Euler(startPosition.right));
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
