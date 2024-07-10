using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private Transform[] obstacles;
    [SerializeField] Vector2 currentSize;
    [SerializeField] int[] minMax = new int[2]; //�����Ǵ� ��ֹ� �ִ��ּ� ����



    void SetupRoom()
    {
        int ran = Random.Range(minMax[0], minMax[1]);
        float x = Random.Range(0 ,currentSize.x);
        float y = Random.Range(0, currentSize.y); //���߿� ũ���� ���ݸ�ŭ ����.

        for (int i = 0; i < ran; i++)
        {
            Transform tf = Instantiate(obstacles[Random.Range(0, obstacles.Length - 1)], transform);
            tf.position = new Vector3(x - currentSize.x / 2, y - currentSize.y / 2, 0);


        }

        //navmesh bake
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
