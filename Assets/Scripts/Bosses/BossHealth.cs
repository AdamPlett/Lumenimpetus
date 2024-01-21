using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 200f;


    [Header("Phases")]
    [SerializeField] int currentPhase = 1;
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
        if (currentHealth <= maxHealth - currentPhaseIncrement)
        {
            ChangePhase();
        }
    }
    public void ChangePhase()
    {
        currentPhaseIncrement = phaseIncrements[currentPhase];
        currentPhase++;
    }
}
