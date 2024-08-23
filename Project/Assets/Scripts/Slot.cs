using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;


public class Slot : MonoBehaviour, IPointerEnterHandler
{
    public Inventory inven;

    public void OnPointerEnter(PointerEventData eventData)
    {
          Debug.Log("Mouse exit");
          inven.targetSlot = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse exit");
        if(inven.targetSlot == this)
            inven.targetSlot = null;
    }
   
    public UnityEngine.UI.Image image;
    
    public TMP_Text countText;

    public int count;
    public Item item;
    public Transform countBase;

}
