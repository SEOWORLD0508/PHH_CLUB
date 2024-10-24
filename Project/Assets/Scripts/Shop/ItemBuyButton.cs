using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBuyButton : MonoBehaviour
{
    [SerializeField]
    public Item item;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnClickBuyItemButton()
    {
        if (GameManager.Instance.Gold < item.values[0])
        {
            print("응 못사"); // 누가 좀 구현해봐
        }
        else
        {
            GameManager.Instance.Gold -= item.values[0];
            GameManager.Instance.UpdateGoldUI();
        }
    }
}
