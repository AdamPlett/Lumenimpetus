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

    public void Update()
    {
        if (gm.ph.currentHealth <= 0)
        {
            gm.boss2.Dance();
        }
    }

    public void DamageBoss(float damage)
    {
        if(!gm.boss2.teleporting && currentHealth > 0)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                gm.boss2.Die();
            }
        }
    }
}
