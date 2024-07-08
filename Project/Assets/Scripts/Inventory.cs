using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using static UnityEditor.Progress;

public enum ItemType
{
    Weapon,
    Etc
}


public class Inventory : MonoBehaviour
{

    public List<Item> weapons = new List<Item>();

    public List<Item> equipments = new List<Item>();

    [SerializeField]
    int itemLayer;
    [SerializeField]
    TMP_Text popUpText;

    [SerializeField]
    float itemAcquireDistance;

    [SerializeField]
    public Transform inventoryBase;
    [SerializeField]
    Slot[] slots;


    float a = 0.5f;

    public void AddItem(Item _item)
    {
        if(_item.itemType == ItemType.Weapon)
        {
            weapons.Add(_item); // s
        } else if(_item.itemType == ItemType.Etc)
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
        else if (_item.itemType == ItemType.Etc)
        {
            weapons.Remove(_item);
        }
    }


    void Setup()
    {
       
    }
    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {


        if (GameManager.Instance.InventoryBool)
        {
            inventoryBase.gameObject.SetActive(true);
            
            foreach (Slot item in slots)
            {
                if (item.count == 0)
                    item.countBase.gameObject.SetActive(false);
                else
                {
                    item.countBase.gameObject.SetActive(true) ;
                    item.countText.text = item.count.ToString();
                }
            }

        } else
        {
            inventoryBase.gameObject.SetActive(false);
        }


        ItemPrefab[] temp = FindObjectsOfType<ItemPrefab>();

        if (temp.Length > 0)
        {
            ItemPrefab closest = null;
            float dis = 9999;
            foreach (ItemPrefab item in temp)
            {
                if (Vector2.Distance(item.transform.position, transform.position) < itemAcquireDistance)
                {
                    if (dis > Vector2.Distance(item.transform.position, transform.position))
                    {
                        dis = Vector2.Distance(item.transform.position, transform.position);
                        closest = item;
                    }

                }
            }

            if (closest)
            {
                popUpText.text = "Press <color=yellow>E</color> to collect <color=yellow>" + closest.item.ItemName;
                popUpText.transform.position =Camera.main.WorldToScreenPoint( closest.transform.position + Vector3.down * 1);
                popUpText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    closest.DecreaseAmount(1);
                    switch(closest.item.itemType)
                    {
                        case ItemType.Etc:
                            equipments.Add(closest.item);
                            break;
                        case ItemType.Weapon:
                            if (weapons.Count == 2)
                                weapons.RemoveAt(0);
                            weapons.Add(closest.item);
                            break;
                    }
                }

            }
            else
            {
                popUpText.gameObject.SetActive(false);
            }
        }
        else
        {
            popUpText.gameObject.SetActive(false);
        }
    }




    //private void OnTriggerStay(Collider other)
    //{
    //    print(other.gameObject);
    //    if(other.gameObject.layer == itemLayer)
    //    {
    //        popUpText.text = "Press <color=yellow>E</color> to collect <color=yellow>" + other.gameObject.name;
    //        popUpText.gameObject.SetActive(true);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.layer == itemLayer)
    //    {
    //        popUpText.gameObject.SetActive(false);
    //    }
    //}

}
