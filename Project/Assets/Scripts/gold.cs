using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class goldDisplayer : MonoBehaviour
{
    public GameManager gameManager;
    public TMP_Text GoldText;


    // Start is called before the first frame update
    void Update()
    {
        if (gameManager != null && GoldText != null)
        {
            GoldText.text = "Gold: " + gameManager.GetGold().ToString();
        }
    }
}