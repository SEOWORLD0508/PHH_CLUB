using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopping : MonoBehaviour
{
    [SerializeField]
    float interactionDistance;

    [SerializeField]
    public ItemHolder[] ItemList;  // �������� �Ǹ��� �� �ִ� ������ ���

    private Inventory playerInventory;
    private GameObject player;

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
            if (Input.GetKeyDown(KeyCode.E))
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
}
