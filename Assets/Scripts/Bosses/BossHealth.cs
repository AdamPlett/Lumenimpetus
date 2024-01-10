using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth;

    [Header("Phases")]
    [SerializeField] int currentPhase = 1;
    [Space(5)]
    [SerializeField] List<int> phaseIncrements;

    public int GetCurrentPhase()
    {
        return currentPhase;
    }
}
