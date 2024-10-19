using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeController : MonoBehaviour
{
    public GameObject shape; // 도형 오브젝트
    public Transform playerView; // 플레이어 시점의 위치
    private Vector3 initialPosition; // 도형의 초기 위치

    void Start()
    {
        initialPosition = shape.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleShapePosition();
        }
    }

    void ToggleShapePosition()
    {
        if (shape.transform.position == initialPosition)
        {
            shape.transform.position = playerView.position; // 도형을 플레이어 시점으로 이동
        }
        else
        {
            shape.transform.position = initialPosition; // 도형을 원래 위치로 이동
        }
    }
}
