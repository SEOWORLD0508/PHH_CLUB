using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellButton : MonoBehaviour
{
    public Button myButton; // ¹öÆ°
    // Start is called before the first frame update
    void Start()
    {
       
        myButton.onClick.AddListener(SellItem);
    }

    void SellItem()
    {
        print(1);
        Shopping StoreKeeper = FindObjectOfType<Shopping>();
        StoreKeeper.SellItem();
    }
    
}
