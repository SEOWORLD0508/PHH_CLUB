using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;


public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Inventory inven;

    public void OnPointerEnter(PointerEventData eventData)
    {
          //Debug.Log("Mouse exit");
          inven.closestSlot = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Mouse exit");
        if(inven.closestSlot == this)
            inven.closestSlot = null;
    }
   
    public UnityEngine.UI.Image image;
    
    public TMP_Text countText;

    public int count;
    public Item item;
    public Transform countBase;

}
