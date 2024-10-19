using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeController : MonoBehaviour
{
    public GameObject shape; // ���� ������Ʈ
    public Transform playerView; // �÷��̾� ������ ��ġ
    private Vector3 initialPosition; // ������ �ʱ� ��ġ

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
            shape.transform.position = playerView.position; // ������ �÷��̾� �������� �̵�
        }
        else
        {
            shape.transform.position = initialPosition; // ������ ���� ��ġ�� �̵�
        }
    }
}
