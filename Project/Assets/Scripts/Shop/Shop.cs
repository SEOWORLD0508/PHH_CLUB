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
    public ItemHolder[] StaticItemForSaleList;  // �������� �Ǹ��� �� �ִ� ������ ��� ( �������ִ°� )
    public ItemHolder[] DynamicItemForSaleList; // �������� �Ǹ��� �� �ִ� ������ ��� ( ���� )



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
            // E Ű�� ���� ������ ��ȣ�ۿ�
            if (Input.GetKeyDown(KeyCode.T))
            {   // ���� UI ���� / �ݱ�
                //OpenShopUI(); 
                print("Shop Opened");
                GameManager.Instance.ShopOnOff = !GameManager.Instance.ShopOnOff;
                print(GameManager.Instance.ShopOnOff);
                
            }
        }
        

    }
    /*
    // ���� UI�� ���� �޼ҵ�
    void OpenShopUI()
    {
        Debug.Log("Shop Opened");
        
        // UI�� ����, �÷��̾ �Ǹ��� �� �ִ� ������ ����� �����ݴϴ�.
        //ShowInventoryItemsForSale();
    }
    */
    /*
    // �κ��丮���� �Ǹ��� �� �ִ� �������� �����ְ� �Ǹ��ϴ� ����
    void ShowInventoryItemsForSale()
    {
        foreach (ItemHolder itemHolder in playerInventory.equipments)
        {
            if (itemHolder.count > 0)
            {
                // ����: UI ��ư�� ������ �÷��̾ ������ �� �ֵ��� ��
                Debug.Log($"Item: {itemHolder.item.ItemName}, Count: {itemHolder.count}");

                // UI ��ư�� ������ �� SellItem �޼ҵ带 ȣ���ϵ��� ����
                // �����δ� UI ��ư�� �־�� ������, ���⼭�� �����ϰ� ó��
                // ��ư Ŭ�� �� ������ �Ǹ�
                SellItem();
            }
        }
    }
    */
    // ������ �Ǹ� �޼ҵ�
    public void SellItem() // Item itemToSell
    {
        for(var i = GameManager.Instance.ItemToSellList.Count - 1; i >= 0; i--)
        {
            // �������� �Ǹ� ���� ��� (���� ������ 50%)
            Item ItemToSell = GameManager.Instance.ItemToSellList[i].GetComponent<ItemPrefab>().item;
            float sellPrice = ItemToSell.values[0] * 0.5f;

            // �κ��丮���� ������ ����
            //playerInventory.RemoveItem(playerInventory.equipments, ItemToSell, 1);
            //Destroy(ItemToSell);
            // ������ ���� ���� �ؾ��� ����
            // �÷��̾��� �� ����
            GameManager.Instance.Gold += sellPrice;

            // UI ������Ʈ
            GameManager.Instance.UpdateGoldUI();
            Destroy(GameManager.Instance.ItemToSellList[i]);
            Debug.Log($"Sold {ItemToSell.ItemName} for {sellPrice} Gold.");
        }
        /*
        foreach(GameObject ItemToSellObject in GameManager.Instance.ItemToSellList)
        {
            // �������� �Ǹ� ���� ��� (���� ������ 50%)
            Item ItemToSell= ItemToSellObject.GetComponent<ItemPrefab>().item;
            float sellPrice = ItemToSell.values[0] * 0.5f;

            // �κ��丮���� ������ ����
            //playerInventory.RemoveItem(playerInventory.equipments, ItemToSell, 1);
            //Destroy(ItemToSell);
            // ������ ���� ���� �ؾ��� ����
            // �÷��̾��� �� ����
            GameManager.Instance.Gold += sellPrice;

            // UI ������Ʈ
            GameManager.Instance.UpdateGoldUI();
            
            Debug.Log($"Sold {ItemToSell.ItemName} for {sellPrice} Gold.");
            
        }
        */
        GameManager.Instance.ItemToSellList.Clear();
        
    }
   

    // �� UI ������Ʈ -> GameManager.Instance.UpdateGoldUI()


    // ������ �尬�ٰ� �����°� ����
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
