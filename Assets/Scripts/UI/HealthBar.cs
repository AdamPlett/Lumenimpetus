using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Stats")]
    public float maxHealth = 100f;
    public float health = 100f;
    public float lerpSpeed = .5f;

    [Header("References")]
    public Slider healthSlider;
    public Slider DmgOverTimeSlider;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //if health of entity differs from the slider than change the slider value to current health
        if (healthSlider.value != health)
        {
            healthSlider.value = health;
        }
        //Slows decreases background dmg over time bar to health bar's current value
        if (healthSlider.value != DmgOverTimeSlider.value)
        {
            DmgOverTimeSlider.value = Mathf.Lerp(DmgOverTimeSlider.value, healthSlider.value, lerpSpeed);
        }
        //Test damage for health bar
        if (Input.GetKeyDown(KeyCode.T))
        {
            takeDamage(10);
        }
    }
    //Code to test damage on health bars
    void takeDamage(float dmg)
    {
        health -= dmg;
    }
}
