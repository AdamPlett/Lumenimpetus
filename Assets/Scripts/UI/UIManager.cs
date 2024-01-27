using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;


public class UIManager : MonoBehaviour
{

    public BossHealthBar bhb;
    public PlayerHealthBar phb;
    public GameObject crosshair;
    public GameObject fireball;
    public GameObject canDash;
    public ComboCounter cc;
    public GameObject comboText;

    void Update()
    {
        if (gm.pm.combo && gm.pm.comboCount > 2 && !comboText.activeSelf)
        {
            comboText.SetActive(true);
        }
        else if (!gm.pm.combo && comboText.activeSelf)
        {
            comboText.SetActive(false);
        }
    }
}
