using System;
using System.Collections.Generic;
using UnityEngine;

struct RoomStr
{
    public bool isEntered; //플레이어가 있는지 없는지지
    public int RoomKind;  //방 종류
    public bool isCleared; //깼는지 안깼는지
    public int EnemyAmount; //몬스터 수
}

public class MapPlacing : MonoBehaviour
{
    [SerializeField]
    Transform[] roomPrefabs;

    [SerializeField]
    List<Transform> rooms;

    [SerializeField]
    float GridSize;

    private void Start()
    {
        System.Random num = new System.Random();
        string result = "";
        int[,] MapArr = new int[6, 6];
        int i;
        int j;
        for (i = 0; i < 6; i++)
        {
            for (j = 0; j < 5; j++)
            {
                MapArr[i, j] = num.Next(3);
            }
        }
        for (i = 0; i < 6; i++)
        {
            MapArr[i, 3] = 4;
            MapArr[i, 1] = 4;
        }
        for (i = 0; i < 6; i++)
        {
            MapArr[4, i] = 4;
        }
        MapArr[3, 2] = 4; //5는 방 인식 안됨 why?
        MapArr[5, 2] = 4;
        MapArr[5, 0] = 3; 
        for (i = 0; i < 6; i++)
        {
            for (j = 0; j < 5; j++)
            {
                result = result + MapArr[i, j] + " ";
                rooms.Add(Instantiate(roomPrefabs[MapArr[i, j]], new Vector2(j * GridSize,
                    -1 * i * GridSize), Quaternion.identity));
            }
            result = result + "\n";
        }
        Debug.Log(result);
    }
    public static int[,] FirstToSec(string ArrNum, int width, int height)
    {
        int i;
        int j;
        int k = 0;
        string[] numbers = ArrNum.Split(" ");
        int len = ArrNum.Length / 2;
        int[,] ResultMapArr = new int[6, 6];
        for (i = 0; i < width; i++)
        {
            for (j = 0; j < height; j++)
            {
                ResultMapArr[i, j] = Int32.Parse(numbers[k]);
                k++;
            }
        }
        return ResultMapArr;
    }

    public static string SecToFirst(int[,] SecArr, int width, int height)
    {
        int i;
        int j;
        string result = "";
        for (i = 0; i < width; i++)
        {
            for (j = 0; j < height; j++)
            {
                result = result + SecArr[i, j] + " ";
            }
        }
        return result;
    }
}