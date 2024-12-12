using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
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
    [SerializeField]
    Slot[] weaponSlots;
    


    public Slot closestSlot;
    Slot desSlot;
    

    float a = 0.5f;

    [SerializeField]
    Canvas canvas;


    bool DescriptionOn = false;

    public bool popUpInMouse;

    public PlayerStatus player;

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
                    //print(equipments[i].item.sprite);
                    slots[i].image.sprite = equipments[i].item.sprite;
                }
                else
                {
                    slots[i].item = null;
                    slots[i].image.sprite = null;
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
            for (int i = 0; i < weaponSlots.Length; i++)
            {

                if (i < weapons.Count)
                {
                    weaponSlots[i].item = weapons[i].item;
                    weaponSlots[i].count = weapons[i].count;
                    weaponSlots[i].image.sprite = weapons[i].item.sprite;
                }
                else
                {
                    weaponSlots[i].item = null;
                    weaponSlots[i].count = 0;
                    weaponSlots[i].image.sprite = null;
                }
                if (weaponSlots[i].count == 0)
                {
                    weaponSlots[i].countBase.gameObject.SetActive(false);
                }
                else
                {
                    weaponSlots[i].countBase.gameObject.SetActive(true);
                    //weaponSlots[i].countText.text = weaponSlots[i].count.ToString();
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
       
        desSlot = weaponSlots[0];

        SelectWeapon(weapons[0].item);

    }

    public void AddItem(List<ItemHolder> _itemHolder, Item _target)
    {
        bool t = false; //이미 해당 아이템이 인벤토리에 있으면 count만 늘리고, 없으면 새로 하나 만듬
        int i = 0;

            foreach (ItemHolder item in _itemHolder)
            {
                if (item.item != null) i++;
                if (item.item == _target)
                {
                    t = true;
                    if(_target.itemType != ItemType.Weapon) // 해당 무기; 이미 있으면 스킵!
                        item.count++;

                }
            }
        
        //else
        //{

        //}

        if (!t) 
        {
            ItemHolder it = new ItemHolder();
            it.item = _target;
            it.count = 1;
            _itemHolder.Add(it);
            if(weapons.Count > 2)
            {
                DropItem(weapons[0].item);
            }
        }
    }   

    public void RemoveItem(List<ItemHolder> _itemHolder, Item _target, int amount)
    {
        

        List<ItemHolder> remove = new List<ItemHolder>();
        foreach (ItemHolder item in _itemHolder)
        {
            if (item.item == _target) {
                item.count -= amount;        
                if(item.count == 0)  remove.Add(item);
                break;
                
            }
        }
        foreach(ItemHolder item in remove)
        {
            _itemHolder.Remove(item);
        }

        

    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if ((equipments.Count + weapons.Count > 0) && (closestSlot != null || popUpInMouse))
            {
                if (closestSlot && closestSlot.item != null)
                {
                    //print(closestSlot);
                    DescriptionOn = true;
                    desSlot = closestSlot;


                }
                else if(!popUpInMouse)
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
                    
                    switch(closest.item.itemType)
                    {
                        default:
                            AddItem(equipments, closest.item);
                            break;
                        case ItemType.Weapon:
                            AddItem(weapons, closest.item);
                            
                            player.UpdateStatus();
                            //print("Update Weapon");
                            break;
                    }
                    closest.DecreaseAmount(1);
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
            GameManager.Instance.ItemSprite.sprite = desSlot.item.sprite;
            if (Input.GetKeyDown(KeyCode.Q))
            {
               QPress();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
         
                EPress();
            }

        }
        Refresh();
      

    }

    public void QPress(){
         if(desSlot.item.itemType == ItemType.Weapon) 
                {
                    Refresh();
                    if (weapons.Count == 1) return;
                    SelectWeapon(weaponSlots[0].item);
                    //print("Update Weapon");
                }
                DropItem(desSlot.item);
    }

    public void EPress(){
               UseItem(desSlot.item);
                //print(desSlot.item.ItemName + "is Used");
                if (desSlot.item.itemType == ItemType.Etc)
                { 
                    RemoveItem(equipments, desSlot.item, 1);
                }
    }

    public void DropItem(Item _item)
    {
        
        if (_item != null)
            Instantiate(_item.prefab, transform.position, Quaternion.identity);
        List<ItemHolder> t = (_item.itemType == ItemType.Weapon) ?  weapons : equipments;
        RemoveItem(t, _item, 1);

    }
    public void UseItem(Item _item)
    {

        if(_item.itemType == ItemType.Weapon){
            SelectWeapon(_item);
        }

        PlayerStatus Player_ = FindObjectOfType<PlayerStatus>();
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

    void SelectWeapon(Item _target){
        player.item = _target;
        player.UpdateStatus();
        Refresh();
        for(int i = 0; i < weaponSlots.Length;i++){
            print(weaponSlots[i].item);
            highlights[i].gameObject.SetActive((weaponSlots[i].item) && weaponSlots[i].item == _target);
               
        }
    }

    [SerializeField]
    Transform[] highlights;

}
