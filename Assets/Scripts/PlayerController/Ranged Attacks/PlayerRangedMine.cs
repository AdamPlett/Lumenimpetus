using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class PlayerRangedMine : RangedAttack
{
    GameObject mineInstance;
    public Rigidbody rb;
    public float shotTimer = 0f;
    public AudioClip playerMineSFX;

    // Update is called once per frame
    void Update()
    {
        base.Update();

    }
    public override void Shoot(Vector3 targetPosition)
    {
        StartCoroutine(ShotDelay());
        ResetTimer();
    }
    private IEnumerator ShotDelay()
    {
        firing = true;
        yield return new WaitForSeconds(shotDelay);

        Vector3 targetPosition = GetShotTarget();
        if (targetPosition != null)
        {
            shootDirection = GetDirection(startPosition.position, targetPosition);
        }
        else
        {
            shootDirection = startPosition.up;
        }

        SpawnMine();

        //play shot SFX
        gm.pm.swordSFX.PlayOneShot(playerMineSFX);

        Invoke(nameof(AnimationBuffer), 1f);
        shooting = true;
        Debug.Log("Bang");
    }
    private void AnimationBuffer()
    {

        firing = false;
    }
    public void SpawnMine()
    {
        mineInstance = Instantiate(bulletPrefab, startPosition.position, Quaternion.Euler(startPosition.right));
        mineInstance.transform.parent = null;
        rb = mineInstance.GetComponent<Rigidbody>();
        mineInstance.GetComponent<PlayerMine>().InitBullet(this);
    }
}
