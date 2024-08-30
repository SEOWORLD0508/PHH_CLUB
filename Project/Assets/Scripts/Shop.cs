using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// NPC 코드

public class Shopping : MonoBehaviour
{
    [SerializeField]
    float interactionDistance;

    [SerializeField]
    public ItemHolder[] ItemList;

    void Setup()
    {

    }
    void Start()
    {
    }
    void Update()
    {
        GameObject Player = FindObjectOfType<PlayerMovement>().gameObject;

        float dis = Vector3.Distance(Player.transform.position, transform.position);
        if (dis < interactionDistance)
        {

            if (Input.GetKeyDown(KeyCode.E)) // 인터렉션
            {
                print("interacted");
            }


        }



    }



}