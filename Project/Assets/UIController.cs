using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(GameManager))]
public class UIController : GameManager
{
    [SerializeField]
    Image healthBar, staminaBar;

    [SerializeField]
    float barSpeed;

    public float targetH, targetS; // (currentValue/MaxValue)

    private void Update() 
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, targetH, barSpeed);
        staminaBar.fillAmount = Mathf.Lerp(staminaBar.fillAmount, targetS, barSpeed);
    }

}
