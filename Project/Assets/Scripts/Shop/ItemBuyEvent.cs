using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;

public class ItemBuyEvent : MonoBehaviour
{
    public ItemPrefab saleItemPrefab;
    public List<Item> ItemForSaleList;
    ItemHolder[] StaticItemForSaleList;  // 상점에서 판매할 수 있는 아이템 목록 ( 정해져있는거 )
    ItemHolder[] DynamicItemForSaleList; // 상점에서 판매할 수 있는 아이템 목록 ( 랜덤 )
    // Start is called before the first frame update
    private void Start()
    {
        StaticItemForSaleList = FindObjectOfType<Shopping>().GetComponent<Shopping>().StaticItemForSaleList;
        DynamicItemForSaleList = FindObjectOfType<Shopping>().GetComponent<Shopping>().DynamicItemForSaleList;
        DynamicItemForSaleList = ShuffleArray<ItemHolder>(DynamicItemForSaleList);
        

        for(var i = 0; i <4; i++)
        {
            
            Item item_ = DynamicItemForSaleList[i].item;
            Sprite Image;
            Image = item_.sprite;
            ItemForSaleList.Add(item_);
            transform.GetChild(0).GetChild(0).GetChild(i).GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = item_.name;
            transform.GetChild(0).GetChild(0).GetChild(i).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = (item_.values[0].ToString() + "$");
            transform.GetComponentInChildren<Image>().sprite = Image;
            //transform.GetChild(0).GetChild(0).GetChild(i).GetChild(1).GetComponent<Image>().sprite = Image;
        }
        for(var j = 0; j < StaticItemForSaleList.Length; j++)
        {
            Item item_ = StaticItemForSaleList[j].item;
            Sprite Image;
            Image = item_.sprite;

            ItemForSaleList.Add(item_);
            transform.GetChild(0).GetChild(0).GetChild(j+4).GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = item_.name;
            transform.GetChild(0).GetChild(0).GetChild(j+4).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = (item_.values[0].ToString() + "$");
            transform.GetChild(0).GetChild(0).GetChild(j+4).GetChild(1).GetComponent<Image>().sprite = Image;
        }


    }

    public void OnClickBuyItemButton(int k)
    {
        Inventory Inv = FindObjectOfType<Inventory>().gameObject.GetComponent<Inventory>();
        Item target_item = ItemForSaleList[k];
        if (target_item.values[0] <= GameManager.Instance.Gold)
        {
            switch (target_item.itemType)
            {
                case ItemType.Weapon:
                    if (Inv.weapons.Count == 2)
                        Inv.weapons.RemoveAt(0); // 대충 구현해둠 일단은 아이템이 사라지고 바뀌게 해놓음
                    Inv.AddItem(Inv.weapons,target_item);
                    break;
                case ItemType.Etc:
                    Inv.AddItem(Inv.equipments, target_item);
                    break;
                case ItemType.Passive: // 패시브 아이템 효과 구현하도록 하는 코드
                    target_item.PassiveItemEffect();
                    break;
            }
            GameManager.Instance.Gold -= ItemForSaleList[k].values[0];
            if (k >= 0 && k <= 3)
            {
                disableBuyButton(k);
            }
        }
        
        
        
        
    }

    void disableBuyButton(int k)
    {
        transform.GetChild(0).GetChild(0).GetChild(k).GetChild(0).gameObject.GetComponent<Button>().interactable = false;
    }
    private T[] ShuffleArray<T>(T[] array)
    {
        int random1, random2;
        T temp;

        for (int i = 0; i < array.Length; ++i)
        {
            random1 = Random.Range(0, array.Length);
            random2 = Random.Range(0, array.Length);

            temp = array[random1];
            array[random1] = array[random2];
            array[random2] = temp;
        }

        return array;
    }
}
