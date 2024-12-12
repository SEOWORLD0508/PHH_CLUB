using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Indicator : MonoBehaviour
{

    [SerializeField]
    GameObject red;
  
    [SerializeField]
    PlayerStatus status;
   

    public void makeRed(float dmg)
    {

        GameObject ob = Instantiate(red, transform);
        ob.GetComponent<UnityEngine.UI.RawImage>().color = new Color(255, 255, 255, (float)(dmg/status.maxHp));

    }



}
