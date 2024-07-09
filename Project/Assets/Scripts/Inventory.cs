using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEditor.Progress;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;

public enum ItemType
{
    Weapon,
    Etc
}
[System.Serializable]
public class ItemHolder
{
    public Item item;
    public int count;
}


public class Inventory : MonoBehaviour
{

    public List<ItemHolder> weapons = new List<ItemHolder>();

    public List<ItemHolder> equipments = new List<ItemHolder>();

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



    void Setup()
    {

        List<ItemHolder> temp = equipments;
        foreach (ItemHolder item in temp)
        {
            if(item.count <= 0)  equipments.Remove(item);
        }

        if (GameManager.Instance.InventoryBool)
        {
            inventoryBase.gameObject.SetActive(true);



            for (int i = 0; i < slots.Length; i++)
            {

                if (i < equipments.Count)
                {
                    slots[i].item = equipments[i].item;
                    slots[i].count = equipments[i].count;
                }
                else
                {
                    slots[i].item = null;
                    slots[i].count = 0;
                }
                if (slots[i].count == 0)
                    slots[i].countBase.gameObject.SetActive(false);
                else
                {
                    slots[i].countBase.gameObject.SetActive(true);
                    slots[i].countText.text = slots[i].count.ToString();
                }
            }


        }
        else
        {
            inventoryBase.gameObject.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    void AddItem(List<ItemHolder> _itemHolder, Item _target)
    {
        bool t = false;
        foreach (ItemHolder item in _itemHolder)
        {
            if(item.item == _target)
            {
                item.count++;
                t = true;
            }    
        }

        ItemHolder it = new ItemHolder();
        it.item = _target;
        it.count = 1;
        if (!t) _itemHolder.Add(it);
    }   


    // Update is called once per frame
    void Update()
    {
        Setup();


        if (Input.GetKeyDown(KeyCode.K))
            equipments[0].count = 0;

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
                            AddItem(equipments, closest.item);
                            break;
                        case ItemType.Weapon:
                            if (weapons.Count == 2)
                                weapons.RemoveAt(0);
                            AddItem(weapons, closest.item);
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
