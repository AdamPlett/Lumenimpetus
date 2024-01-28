using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class Boss1Health : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 200f;


    [Header("Phases")]
    [SerializeField] int currentPhase = 1;
    [SerializeField] int maxPhaseCount;
    [SerializeField] int currentPhaseIncrement;
    [Space(5)]
    [SerializeField] List<int> phaseIncrements;
   
    private void Start()
    {
        currentHealth = maxHealth;
        currentPhaseIncrement = phaseIncrements[0];   
    }

    public int GetCurrentPhase()
    {
        return currentPhase;
    }
    public void DamageBoss(float damage) {
        
        currentHealth -= damage;

        gm.boss1.CheckDead();

        if (currentHealth <= maxHealth - currentPhaseIncrement)
        {
            ChangePhase();
        }
    }

    public void ChangePhase()
    {
        if(currentPhase < maxPhaseCount)
        {
            currentPhase++;

            if (currentPhase < phaseIncrements.Count)
            {
                currentPhaseIncrement = phaseIncrements[currentPhase];
            }

            Debug.Log("Trigger Phase Change!");

            if (currentPhase > 1)
            {
                gm.boss1.TriggerPhaseChange();

                gm.boss1.weapons.enraged = true;
                gm.boss1.weapons.meleeRange = 20f;
                gm.boss1.weapons.meleeCooldown = 1f;
                gm.boss1.weapons.cannonCooldown = 2f;

                gm.boss1.weapons.currentAmmo = eMine.explosive;
            }
        }
    }
}
