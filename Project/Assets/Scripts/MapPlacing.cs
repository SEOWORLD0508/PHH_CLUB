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

public struct RoomPer
{
    public double Room1;
    public double Room2;
    public double Room3;
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
        string result = "";
        int[,] MapArr = new int[6, 6];
        int i, j;
        RoomPer roomPer;
        roomPer.Room1 = 0.3;
        roomPer.Room2 = 0.4;
        roomPer.Room3 = 0.3;
        for (i = 0; i < 5; i++)
        {
            MapArr[i, 3] = 4;
            MapArr[i, 1] = 4;
        }
        for (i = 0; i < 6; i++)
        {
            MapArr[4, i] = 4;
            MapArr[5, i] = 4;
        }
        //MapArr[3, 2] = 4; //5는 방 인식 안됨 why?
        int[] RanArr = new int[30];
        RanArr = CreateMapRandArr(5, 6, roomPer);
        Debug.Log(RanArr.Length);
        RoomStr[] roomStr = new RoomStr[RanArr.Length];
        for(i=0; i<RanArr.Length; i++){
            roomStr[i].isEntered = false;
            roomStr[i].RoomKind = RanArr[i];
            roomStr[i].isCleared = false;
        }
        int k = 0;
        for (i = 0; i < 6; i++)
        {
            for (j = 0; j < 5; j++)
            {
                if (MapArr[i, j] != 4)
                {
                    if (k == RanArr.Length)
                    {
                        break;
                    }
                    MapArr[i, j] = RanArr[k];
                    k++;
                }
            }
        }
        MapArr[5, 0] = 3; //체크 포인트
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

    public static int[] CreateMapRandArr(int width, int height, RoomPer percent)
    {
        string result = "";
        int i;
        int[] ErrorResult = new int[1];
        ErrorResult[0] = -1;
        if (width % 2 != 1)
        { //짝수라면 맵 생성이 불편
            return ErrorResult;
        }
        if (percent.Room1 + percent.Room2 + percent.Room3 != 1)
        { //확률 다 더하면 1되어야 함
            return ErrorResult;
        }
        int len = ((height / 2) + 1) * (width - 2);
        int[] MapFirst = new int[len];
        for (i = 0; i < len; i++)
        {
            if (i <= len * percent.Room1)
            {
                MapFirst[i] = 0;
            }
            else if (i <= len * (percent.Room1 + percent.Room2))
            {
                MapFirst[i] = 1;
            }
            else if (i <= len * (percent.Room1 + percent.Room2 + percent.Room3))
            {
                MapFirst[i] = 2;
            }
        }
        MapFirst = ShuffleArray(MapFirst);
        return MapFirst;
    }

    public static int[,] FirstToSec(string ArrNum, int width, int height)
    {
        int i, j;
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
        int i, j;
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

    public static int[] ShuffleArray(int[] array)
    {
        int random1, random2;
        int k;
        System.Random num = new System.Random();
        for (int i = 0; i < array.Length; ++i)
        {
            random1 = num.Next(array.Length);
            random2 = num.Next(array.Length);
            k = array[random1];
            array[random1] = array[random2];
            array[random2] = k;
        }
        return array;
    }
}