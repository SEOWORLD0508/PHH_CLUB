using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class Shopping : MonoBehaviour
{
    [SerializeField]
    float interactionDistance;

    [SerializeField]
    public ItemHolder[] StaticItemForSaleList;  // 상점에서 판매할 수 있는 아이템 목록 ( 정해져있는거 )
    public ItemHolder[] DynamicItemForSaleList; // 상점에서 판매할 수 있는 아이템 목록 ( 랜덤 )



    private GameObject player;


   
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        //playerInventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        float dis = Vector3.Distance(player.transform.position, transform.position);
        if (dis < interactionDistance)
        {
            // E 키를 눌러 상점과 상호작용
            if (Input.GetKeyDown(KeyCode.T))
            {   // 상점 UI 열기 / 닫기
                //OpenShopUI(); 
                print("Shop Opened");
                GameManager.Instance.ShopOnOff = !GameManager.Instance.ShopOnOff;
                print(GameManager.Instance.ShopOnOff);
                
            }
        }
        

    }
    /*
    // 상점 UI를 여는 메소드
    void OpenShopUI()
    {
        Debug.Log("Shop Opened");
        
        // UI를 열고, 플레이어가 판매할 수 있는 아이템 목록을 보여줍니다.
        //ShowInventoryItemsForSale();
    }
    */
    /*
    // 인벤토리에서 판매할 수 있는 아이템을 보여주고 판매하는 로직
    void ShowInventoryItemsForSale()
    {
        foreach (ItemHolder itemHolder in playerInventory.equipments)
        {
            if (itemHolder.count > 0)
            {
                // 예시: UI 버튼을 생성해 플레이어가 선택할 수 있도록 함
                Debug.Log($"Item: {itemHolder.item.ItemName}, Count: {itemHolder.count}");

                // UI 버튼을 눌렀을 때 SellItem 메소드를 호출하도록 연결
                // 실제로는 UI 버튼이 있어야 하지만, 여기서는 간단하게 처리
                // 버튼 클릭 시 아이템 판매
                SellItem();
            }
        }
    }
    */
    // 아이템 판매 메소드
    public void SellItem() // Item itemToSell
    {
        for(var i = GameManager.Instance.ItemToSellList.Count - 1; i >= 0; i--)
        {
            // 아이템의 판매 가격 계산 (구매 가격의 50%)
            Item ItemToSell = GameManager.Instance.ItemToSellList[i].GetComponent<ItemPrefab>().item;
            float sellPrice = ItemToSell.values[0] * 0.5f;

            // 인벤토리에서 아이템 제거
            //playerInventory.RemoveItem(playerInventory.equipments, ItemToSell, 1);
            //Destroy(ItemToSell);
            // 아이템 제거 구현 해야함 ㅇㅇ
            // 플레이어의 돈 증가
            GameManager.Instance.Gold += sellPrice;

            // UI 업데이트
            GameManager.Instance.UpdateGoldUI();
            Destroy(GameManager.Instance.ItemToSellList[i]);
            Debug.Log($"Sold {ItemToSell.ItemName} for {sellPrice} Gold.");
        }
        /*
        foreach(GameObject ItemToSellObject in GameManager.Instance.ItemToSellList)
        {
            // 아이템의 판매 가격 계산 (구매 가격의 50%)
            Item ItemToSell= ItemToSellObject.GetComponent<ItemPrefab>().item;
            float sellPrice = ItemToSell.values[0] * 0.5f;

            // 인벤토리에서 아이템 제거
            //playerInventory.RemoveItem(playerInventory.equipments, ItemToSell, 1);
            //Destroy(ItemToSell);
            // 아이템 제거 구현 해야함 ㅇㅇ
            // 플레이어의 돈 증가
            GameManager.Instance.Gold += sellPrice;

            // UI 업데이트
            GameManager.Instance.UpdateGoldUI();
            
            Debug.Log($"Sold {ItemToSell.ItemName} for {sellPrice} Gold.");
            
        }
        */
        GameManager.Instance.ItemToSellList.Clear();
        
    }
   

    // 돈 UI 업데이트 -> GameManager.Instance.UpdateGoldUI()


    // 아이템 드갔다가 나가는거 ㅇㅇ
    private void OnTriggerEnter2D(Collider2D other)
    {   
        if ((other.CompareTag("Item")))
        {
            GameObject ItemObject = other.gameObject;
            GameManager.Instance.ItemToSellList.Add(ItemObject);
            /*
            Item ItemToSell = other.gameObject.GetComponent<ItemPrefab>().item;
            GameManager.Instance.ItemToSellList.Add(ItemToSell); 
            */
            print("Enter : " + other.name);
        }
        {

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {   
        if ((other.CompareTag("Item")))
        {
            GameObject ItemObject = other.gameObject;
            GameManager.Instance.ItemToSellList.Remove(ItemObject);
            /*
            Item ItemToSell = other.gameObject.GetComponent<ItemPrefab>().item;
            GameManager.Instance.ItemToSellList.Remove(ItemToSell);
            */
            print("Exit : " + other.name);

        }
        {

        }
    }
}
