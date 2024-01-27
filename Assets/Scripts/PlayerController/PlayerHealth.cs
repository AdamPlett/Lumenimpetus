using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;


public class PlayerHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100f;
    public bool dead = false;
    public float damageSSDuration = 0.4f;
    public float damageSSStrength = 0.6f;
    
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

        //test key
        if (Input.GetKeyDown(KeyCode.K))
        {
            DamagePlayer(10);
        }
    }

    private void KillPlayer()
    {
        gm.pm.freeze = true;
        gm.pm.cam.locked = true;
        gm.boss1.LookAtPlayer();
        gm.cameraRef.transform.LookAt(gm.boss1.viewPoint);
        gm.playerCanvas.enabled = false;
        gm.ui.sword.SetActive(false);
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
        currentHealth -= damage;

        ScreenShake.Shake(damageSSDuration, damageSSStrength);
        if(gm.pm.state != PlayerMovement.MovementState.dashing) gm.pm.BossHitsPlayerStun();
        gm.pm.ResetCombo();
        gm.pm.attackCount = 0;
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
