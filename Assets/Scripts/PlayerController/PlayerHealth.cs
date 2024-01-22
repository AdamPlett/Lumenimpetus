using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100f;
    
    private bool dead = false;

    
    
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
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

        //test key
        if (Input.GetKeyDown(KeyCode.K))
        {
            DamagePlayer(10);
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

    public void DamagePlayer(float damage)
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
