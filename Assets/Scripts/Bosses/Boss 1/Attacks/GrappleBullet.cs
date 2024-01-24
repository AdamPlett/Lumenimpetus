using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class GrappleBullet : MonoBehaviour
{
    public Boss1AttackManager grapple;

    private Vector3 direction;
    private Vector3 startPos;

    public void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
        if(gameObject.activeSelf)
        {
            if (collision.gameObject.tag.Equals("Player"))
            {
                grapple.pulling = true;
                //Debug.Log("Player has been hit by grapple!");
                Debug.Log(collision.gameObject.name);
                gm.pm.freeze = true;
                ResetBullet();
            }
            else if (!collision.gameObject.tag.Equals("Enemy") && !collision.gameObject.tag.Equals("Projectile"))
            {
                Debug.Log(collision.gameObject.name);

                grapple.noHit = true;
                grapple.DeactivateGrapple();
                ResetBullet();
            }
        }
    }

    public void InitBullet(Transform target)
    {
        transform.parent = null;

        startPos = transform.position;

        Vector3 direction = target.position - transform.position;
        transform.forward = direction;
    }

    public void ResetBullet()
    {
        transform.position = Vector3.zero;
        gameObject.SetActive(false);
    }

    public void MoveBullet(float speedFactor)
    {
        float distance = Vector3.Distance(transform.position, startPos);

        if(distance < grapple.pullRangeMax)
        {
            if (!grapple.pulling)
            {
                transform.position += transform.forward * Time.deltaTime * grapple.pullSpeed * speedFactor;
            }
        }
        else
        {
            grapple.noHit = true;
            grapple.DeactivateGrapple();
            ResetBullet();

            Debug.Log("Grapple missed player!");
        }
    }
}
