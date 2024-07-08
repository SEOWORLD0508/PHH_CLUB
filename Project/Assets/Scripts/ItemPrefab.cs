using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPrefab : MonoBehaviour
{
    public Item item;
    public int amount;


    public void DecreaseAmount(int _amount)
    {
        amount -= _amount;

        if(amount <= 0)
        {
            Destroy(gameObject);
        }
    }
}
