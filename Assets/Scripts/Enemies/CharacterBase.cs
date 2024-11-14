using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    [SerializeField] private float maxHP;
    [SerializeField] private float maxHPLimit;
    [SerializeField] private float currentHP;

    [SerializeField] private float deathFXTime;

    public void AddHP(float healAmount)
    {
        currentHP += healAmount;

        if (currentHP < 0) currentHP = 0;
        if (currentHP > maxHP) currentHP = maxHP;

    }

    public void AddMaxHP(float maxHpBoost)
    {
        maxHP += maxHpBoost;

        if (maxHP <= 0) maxHP = 1;
        if (maxHP > maxHPLimit) maxHP = maxHPLimit;
    }

    public void Death()
    {
        PlayDeathFX();
        Destroy(gameObject, deathFXTime);
    }

    public void PlayDeathFX()
    {

    }
}
