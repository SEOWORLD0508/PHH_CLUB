using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// NPC �ڵ�

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

            if (Input.GetKeyDown(KeyCode.E)) // ���ͷ���
            {
                print("interacted"); // 상점창 호출 코드
            }


        }



    }



}