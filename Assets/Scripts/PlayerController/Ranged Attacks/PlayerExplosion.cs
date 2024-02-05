using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class PlayerExplosion : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider Hit");


        if (other.gameObject == gm.bossRef)
        {
            gm.playerRef.GetComponentInChildren<RangedAttack>().DamageBoss();
        }
    }
}
