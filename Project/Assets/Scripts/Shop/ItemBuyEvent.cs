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
            transform.GetChild(0).GetChild(0).GetChild(i).GetChild(2).GetChild(1).GetComponent<SpriteRenderer>().sprite = Image;
        }
        for(var j = 0; j < StaticItemForSaleList.Length; j++)
        {
            Item item_ = StaticItemForSaleList[j].item;
            Sprite Image;
            Image = item_.sprite;

            ItemForSaleList.Add(item_);
            transform.GetChild(0).GetChild(0).GetChild(j+4).GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = item_.name;
            transform.GetChild(0).GetChild(0).GetChild(j+4).GetChild(2).GetChild(1).GetComponent<SpriteRenderer>().sprite = Image;
        }


    }

    public void OnClickBuyItemButton(int k)
    {
        if (ItemForSaleList[k].values[0] <= GameManager.Instance.Gold)
        {
            GameManager.Instance.Gold -= ItemForSaleList[k].values[0];
            if (k >= 0 && k <= 3)
            {
                disableBuyButton(k);
            }
        }
        
        //AddItem()
        
        
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
