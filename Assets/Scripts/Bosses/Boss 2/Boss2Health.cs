using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class Boss2Health : MonoBehaviour
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
    public void DamageBoss(float damage)
    {

        currentHealth -= damage;

        //gm.boss2.CheckDead();

        if (currentHealth <= maxHealth - currentPhaseIncrement)
        {
            ChangePhase();
        }
    }

    public void ChangePhase()
    {
        if (currentPhase < maxPhaseCount)
        {
            currentPhase++;

            if (currentPhase < phaseIncrements.Count)
            {
                currentPhaseIncrement = phaseIncrements[currentPhase];
            }

            Debug.Log("Trigger Phase Change!");

            if (currentPhase > 1)
            {
                // Do Phase 2 Stuff
            }
        }
    }
}
