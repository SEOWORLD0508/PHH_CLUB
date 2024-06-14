using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Equipment
}


public class Inventory : MonoBehaviour
{

    public List<Item> weapons = new List<Item>();

    public List<Item> equipments = new List<Item>();





    public void AddItem(Item _item)
    {
        if(_item.itemType == ItemType.Weapon)
        {
            weapons.Add(_item); // s
        } else if(_item.itemType == ItemType.Equipment)
        {
            weapons.Add(_item);
        }
    }

    public void RemoveItem(Item _item)
    {
        if (_item.itemType == ItemType.Weapon)
        {
            weapons.Remove(_item); // s
        }
        else if (_item.itemType == ItemType.Equipment)
        {
            weapons.Remove(_item);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
