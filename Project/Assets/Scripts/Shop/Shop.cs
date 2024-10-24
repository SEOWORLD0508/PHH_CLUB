using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shopping : MonoBehaviour
{
    [SerializeField]
    float interactionDistance;

    [SerializeField]
    public ItemHolder[] ItemForSaleList;  // �������� �Ǹ��� �� �ִ� ������ ���

    private Inventory playerInventory;
    private GameObject player;

    public List<Item> ItemForSellList;
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        playerInventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        float dis = Vector3.Distance(player.transform.position, transform.position);
        if (dis < interactionDistance)
        {
            // E Ű�� ���� ������ ��ȣ�ۿ�
            if (Input.GetKeyDown(KeyCode.P))
            {
                OpenShopUI();  // ���� UI ����
            }
        }
    }

    // ���� UI�� ���� �޼ҵ�
    void OpenShopUI()
    {
        Debug.Log("Shop Opened");
        // UI�� ����, �÷��̾ �Ǹ��� �� �ִ� ������ ����� �����ݴϴ�.
        ShowInventoryItemsForSale();
    }

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
                SellItem(itemHolder.item);
            }
        }
    }

    // ������ �Ǹ� �޼ҵ�
    public void SellItem(Item itemToSell)
    {
        // �κ��丮���� �ش� ������ �Ǹ�
        ItemHolder itemInInventory = playerInventory.equipments.Find(holder => holder.item == itemToSell);
        if (itemInInventory == null || itemInInventory.count <= 0)
        {
            Debug.Log("You don't have this item to sell!");
            return;
        }

        // �������� �Ǹ� ���� ��� (���� ������ 50%)
        float sellPrice = itemToSell.values[0] * 0.5f;

        // �κ��丮���� ������ ����
        playerInventory.RemoveItem(playerInventory.equipments, itemToSell, 1);

        // �÷��̾��� �� ����
        GameManager.Instance.Gold += sellPrice;

        // UI ������Ʈ
        UpdateGoldUI();

        Debug.Log($"Sold {itemToSell.ItemName} for {sellPrice} Gold.");
    }

    // �� UI ������Ʈ
    void UpdateGoldUI()
    {
        // �� ���� UI ���� ���� �߰�
        Debug.Log($"Current Gold: {GameManager.Instance.Gold}");
    }


    // Applies an upwards force to all rigidbodies that enter the trigger.
    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("Item")))
        {
            Item ItemForSell = other.gameObject.GetComponent<ItemPrefab>().item;
            print(ItemForSell);
            if (!(ItemForSellList.Contains(ItemForSell)))
                  {
                  ItemForSellList.Add(other.gameObject.GetComponent<ItemPrefab>().item);
                  print("Enter : " + other.name);
                
            }
           
        }
        {

        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if ((other.CompareTag("Item")))
        {
            Item ItemForSell = other.gameObject.GetComponent<ItemPrefab>().item;
            print(ItemForSell);
            if ((ItemForSellList.Contains(ItemForSell)))
            {
                ItemForSellList.Remove(other.gameObject.GetComponent<ItemPrefab>().item);
                print("Exit : " + other.name);

            }

        }
        {

        }
    }
}
