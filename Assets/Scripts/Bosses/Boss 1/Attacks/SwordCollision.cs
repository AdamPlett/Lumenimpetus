using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class SwordCollision : MonoBehaviour
{
    public Boss1AttackManager bam;

    public void OnTriggerEnter(Collider other) { 
        
        if (other == gm.playerCollider) {
            if (!gm.ph.invincible)
            {
                Vector3 direction = (gm.pm.transform.position - transform.position).normalized;
                direction.y = 0;
                gm.pm.PlayerKnockback(direction, 50f);
                ScreenShake.Shake(0.4f, 0.5f);
                gm.ph.DamagePlayer(bam.meleeDamage);
            }
        }

    }
}
