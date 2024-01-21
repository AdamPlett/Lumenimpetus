using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    
    private bool dead = false;

    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            dead = true;
        }
        else
        {
            dead = false;
        }
    }

    private void HealPlayer(float healAmt)
    {
        currentHealth += healAmt;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    private void DamagePlayer(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
            dead = true;
        }
        
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }


}
