using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;
using TMPro;
using Unity.VisualScripting;

public class ComboCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text currentScoreTextView;

    public void UpdateText(float count)
    {
        currentScoreTextView.text = "Combo\n x" + count;
    }
}
