using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class Explosion : MonoBehaviour
{
    public float damage;
    public eMine mineType;

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider Hit");

        if (other.gameObject.tag.Equals("Destructible") && mineType == eMine.explosive)
        {
            Debug.Log("destructible object hit" );
            BreakObject destroy = other.gameObject.GetComponent<BreakObject>();

            if (other.gameObject.GetComponent<BreakObject>()!=null)
            {
                destroy.breakObject();
            }
        }
        if (other == gm.playerCollider)
        {
            if (!gm.ph.invincible)
            {
                Vector3 direction = (gm.pm.transform.position - transform.position).normalized;
                direction.y = 0;
                if (mineType == eMine.explosive) { 
                    gm.pm.PlayerKnockback(direction, 200f);
                    ScreenShake.Shake(1f, 2f);
                    gm.ph.DamagePlayer(damage);
                }
                else if (mineType == eMine.energy)
                {
                    gm.pm.PlayerKnockback(direction, 100f);
                    ScreenShake.Shake(0.5f, 1f);
                    gm.ph.DamagePlayer(damage);
                }
            }
        }

    }

}
