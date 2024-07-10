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

    [SerializeField]
    float mouseSlotDis;


    float a = 0.5f;

    [SerializeField]
    Canvas canvas;

    void Setup()
    {
        //if (equipments.Count == 0) return;
        List<ItemHolder> temp = new List<ItemHolder>(); /// 이렇게 안했었는데 직접적으로 수정하니까 작동은 하는데 에러도 같이 나더라고...
        foreach (ItemHolder item in equipments)
        {
            if(item.count <= 0)  temp.Add(item);
        }
        foreach (ItemHolder item in temp)
        {
            equipments.Remove(item);
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
        bool t = false; //이미 해당 아이템이 인벤토리에 있으면 count만 늘리고, 없으면 새로 하나 만듬
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

    void RemoveItem(List<ItemHolder> _itemHolder, Item _target, int amount)
    {
        
        foreach (ItemHolder item in _itemHolder)
        {
            if (item.item == _target) {
                item.count -= amount;
                break;
                
            }
        }

        

    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (equipments.Count > 0)
            {
                Slot closest = GetClosestHolder();
                if(closest.item != null){
                    print(GetClosestHolder());
                    Instantiate(GetClosestHolder().item.prefab, transform.position, Quaternion.identity);
                    RemoveItem(equipments, GetClosestHolder().item, 1);
                }
            }
        }
            Setup();

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



    Slot GetClosestHolder()
    {
       Vector2 mousePos = Input.mousePosition;
        
        //mousePos = Input.mousePosition / canvas.scaleFactor;
        //mousePosImg.anchoredPosition = startPos;

        Slot result = null;
        float dis = mouseSlotDis;

       
        for (int i = 0; i < slots.Length; i++)
        {
            if(dis >= Vector3.Distance(slots[i].transform.position, mousePos))
            {
                result = slots[i];
                dis = Vector3.Distance(slots[i].transform.position, mousePos);
            }

        }

        return result;


    }

}
