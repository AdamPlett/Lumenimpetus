using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class Boss2Health : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 200f;


    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void DamageBoss(float damage)
    {

        currentHealth -= damage;

        //gm.boss2.CheckDead();
    }
}
