using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    private [SerializeField] float maxHP;
    private [SerializeField] float maxHPLimit;
    private [SerializeField] float currentHP;

    private [SerializeField] float deathFXTime;

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
