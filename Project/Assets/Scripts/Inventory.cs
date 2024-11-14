using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using JetBrains.Annotations;
using static UnityEditor.Progress;

public enum ItemType
{
    Weapon,
    Etc,
    Passive
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
    Slot[] slots;

    public Slot closestSlot;
    Slot desSlot;
    

    float a = 0.5f;

    [SerializeField]
    Canvas canvas;


    bool DescriptionOn = false;

    void Refresh()
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
            GameManager.Instance.InventoryBase.gameObject.SetActive(true);
  


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
                {
                    slots[i].countBase.gameObject.SetActive(false);
                }
                else
                {
                    slots[i].countBase.gameObject.SetActive(true);
                    slots[i].countText.text = slots[i].count.ToString();
                }
            }


        }
        else
        {
            GameManager.Instance.InventoryBase.gameObject.SetActive(false);
            DescriptionOn = false;
            GameManager.Instance.DescriptionBase.gameObject.SetActive(DescriptionOn);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Refresh();
        foreach(Slot s  in slots)
        {
            s.inven = this;
        }
    }

    public void AddItem(List<ItemHolder> _itemHolder, Item _target)
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

    public void RemoveItem(List<ItemHolder> _itemHolder, Item _target, int amount)
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

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (equipments.Count > 0 && closestSlot != null)
            {
                if (closestSlot.item != null)
                {

                    print(closestSlot);
                    DescriptionOn = true;
                    desSlot = closestSlot;

                }
                else
                    DescriptionOn = false;
            }
            else
                DescriptionOn = false;
           
            GameManager.Instance.DescriptionBase.gameObject.SetActive(DescriptionOn);
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




        if (DescriptionOn)
        {
            if(desSlot == null) return ;
            if (desSlot.item == null) { DescriptionOn = false; GameManager.Instance.DescriptionBase.gameObject.SetActive(false); return; }

            GameManager.Instance.NameText.text = desSlot.item.ItemName;
            GameManager.Instance.DescriptionText.text = desSlot.item.ItemDescription;
            if (Input.GetKeyDown(KeyCode.Q))
            {
                DropItem(desSlot.item);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                UseItem(desSlot.item);
                //print(desSlot.item.ItemName + "is Used");
                RemoveItem(equipments, desSlot.item, 1);
                
            }

        }
        Refresh();
        


    }

    public void DropItem(Item _item)
    {
        
        if (_item != null)
            Instantiate(_item.prefab, transform.position, Quaternion.identity);
        List<ItemHolder> t = (_item.itemType == ItemType.Etc) ? equipments : weapons;
        RemoveItem(t, _item, 1);

    }
    public void UseItem(Item _item)
    {
        PlayerStatus Player_ = transform.parent.GetComponent<PlayerStatus>();
        // 회복량 %
        if(_item.specialIndex == 1)
        {
            Player_.health += Player_.maxHp * _item.values[2] / 100;
        }
        // 침식도
        else if(_item.specialIndex == 2 )
        {
            Player_.Vamp += _item.values[2];
        }
    }

}
