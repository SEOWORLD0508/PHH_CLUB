using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Slot : MonoBehaviour, IPointerEnterHandler
{

    public void OnPointerEnter(PointerEventData eventData)
    {
        //do stuff
    }
   
    public Image image;
    
    public TMP_Text countText;

    public int count;
    public Item item;
    public Transform countBase;

}
