using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{

    [Header("Bars")]
    [SerializeField]
    Image healthBar, staminaBar;


    [SerializeField]
    float barSpeed;

    public float targetH, targetS; // (currentValue/MaxValue)

    [Space]

    [Header("PauseMenu")]
    [SerializeField]
    Transform pauseBase;


    private void Update() 
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, targetH, barSpeed);
        staminaBar.fillAmount = Mathf.Lerp(staminaBar.fillAmount, targetS, barSpeed);

        pauseBase.gameObject.SetActive(GameManager.Instance.Pause);    
    }

}
