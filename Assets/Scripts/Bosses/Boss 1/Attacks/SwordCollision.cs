using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class SwordCollision : MonoBehaviour
{
    public Boss1AttackManager bam;

    public void OnTriggerEnter(Collider other) { 
        
        if (other == gm.playerCollider) {
            gm.ph.DamagePlayer(bam.meleeDamage);
        }

    }
}
