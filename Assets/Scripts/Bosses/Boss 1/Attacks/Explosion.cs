using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class Explosion : MonoBehaviour
{
    public float damage;
    public void OnTriggerEnter(Collider other)
    {

        if (other == gm.playerCollider)
        {
            gm.ph.DamagePlayer(damage);
        }

    }

}
