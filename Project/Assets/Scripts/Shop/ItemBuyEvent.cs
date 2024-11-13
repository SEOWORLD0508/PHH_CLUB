using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBuyEvent : MonoBehaviour
{
    public ItemPrefab saleItemPrefab;
    // Start is called before the first frame update
    
    public void OnClickBuyItemButton()
    {
        print(transform.parent.gameObject.name);

        //print(saleItemPrefab.amount);
        //print(saleItemPrefab.item.name);
        /*
        saleItemPrefab.amount -= 1;
        if (saleItemPrefab.amount <= 0)
        {
            disableBuyButton();
        }
        */
        
    }

    void disableBuyButton()
    {

    }
}
