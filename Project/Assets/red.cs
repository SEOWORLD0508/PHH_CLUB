using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class red : MonoBehaviour
{
    
    [SerializeField]
    RawImage target;

    [SerializeField]
    float lifeITme =  3;
    float current;
    private void Start()
    {
        current = target.color.a *255;
    }
    private void Update()
    {
       
        current = Mathf.Lerp(current, 0, lifeITme* Time.deltaTime);
        target.color = new Color(255, 255, 255, current/255);
        if(current < 1) { Destroy(gameObject); }
    }

}
