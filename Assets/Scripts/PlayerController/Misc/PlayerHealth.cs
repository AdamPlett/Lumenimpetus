using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;


public class PlayerHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100f;
    public bool dead = false;
    public float iFrames = 3f;
    public float iFrameTimer = 0f;
    public bool invincible = false;
    
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
            KillPlayer();
        }
        else
        {
            dead = false;
        }
        if (invincible)
        {
            iFrameTimer -= Time.deltaTime;
            if (iFrameTimer < 0)
            {
                invincible = false;
            }
        }

        //test key
     /*   if (Input.GetKeyDown(KeyCode.K))
        {
            DamagePlayer(10);
        }*/
    }

    private void KillPlayer()
    {
        gm.pm.freeze = true;
        gm.pm.cam.locked = true;

        if (gm.boss1 != null)
        {
            gm.boss1.LookAtPlayer();
            gm.cameraRef.transform.LookAt(gm.boss1.viewPoint);
        }

        gm.ui.sword.SetActive(false);
        gm.ui.pmc.GameOverScreen();
        // Activate Death UI
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
        if (!invincible)
        {
            currentHealth -= damage;
            if (gm.pm.state != PlayerMovement.MovementState.dashing) gm.pm.BossHitsPlayerStun();
            gm.pm.ResetCombo();
            gm.pm.attackCount = 0;
            if (currentHealth < 0)
            {
                currentHealth = 0;
                dead = true;
            }
            invincible = true;
            iFrameTimer = iFrames;
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
