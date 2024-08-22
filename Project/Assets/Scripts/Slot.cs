using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Slot : MonoBehaviour , IPointerEnterHandler
{
    public Inventory inven;

    public void OnPointerEnter(PointerEventData eventData)
    {
          Debug.Log("Mouse exit");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse exit");
    }
   
    public Image image;
    
    public TMP_Text countText;

    public int count;
    public Item item;
    public Transform countBase;

}
