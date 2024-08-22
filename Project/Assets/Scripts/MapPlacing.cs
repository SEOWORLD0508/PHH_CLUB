using System;
using System.Collections.Generic;
using UnityEngine;
//모듈 선언

struct MapSize //맵 크기
{
    public int width;
    public int height;
}

public struct RoomStr
{
    public bool isEntered; //플레이어가 있는지 없는지지
    public int RoomKind;  //방 종류
    public bool isCleared; //깼는지 안깼는지
    public int EnemyAmount; //몬스터 수
}

public struct RoomPer //맵의 방 수 비율
{
    public double Room1;
    public double Room2;
    public double Room3;
}

public struct RoomNumInfo
{
    public int aisle;
    public int check;
}

public class MapPlacing : MonoBehaviour
{
    [SerializeField]
    Transform[] roomPrefabs;

    [SerializeField]
    List<Transform> rooms;

    [SerializeField]
    float GridSize;
    //코드에서 유니티에 상호작용 할수 있게 함
    private void Start()
    {
        /*방 종류
        0~2 : 랜덤으로 배치되는 방(상자방, 몬스터 방 etc..)
        3 : 체크 포인트
        4 : 복도(플레이어가 이동하는 곳)
        */
        int i, j;
        string result = "";
        MapSize mapSize;
        RoomNumInfo roomNumInfo;
        mapSize.width = 5; //홀수여야 함
        mapSize.height = 6;
        roomNumInfo.aisle = 4;
        roomNumInfo.check = 3;
        int Width = mapSize.width;
        int Height = mapSize.height;
        RoomPer roomPer;
        roomPer.Room1 = 0.3;
        roomPer.Room2 = 0.4;
        roomPer.Room3 = 0.3;
        int[] RanArr = CreateMapRandArr(Width, Height, roomPer);
        int[,] Map = CreateMap(Width, Height, RanArr, roomNumInfo);
        RoomStr[] RoomInfo = CreateMapStr(Width, Height, RanArr);
        for (i = 0; i < Height; i++)
        {
            for (j = 0; j < Width; j++)
            {
                result = result + Map[i, j] + " ";
                rooms.Add(Instantiate(roomPrefabs[Map[i, j]], new Vector2(j * GridSize, -1 * i * GridSize), Quaternion.identity));
            }
            result = result + "\n";
        }
        Debug.Log(result); //출력/유니티에 반영
    }

    public RoomStr[] CreateMapStr(int Width, int Height, int[] RanArr)
    {
        RoomStr[] roomStr = new RoomStr[RanArr.Length];
        int i;
        for (i = 0; i < RanArr.Length; i++)
        {
            roomStr[i].isEntered = false;
            roomStr[i].RoomKind = RanArr[i];
            roomStr[i].isCleared = false;
        }
        return roomStr;
    }

    public static int[,] CreateMap(int Width, int Height, int[] RanArr, RoomNumInfo roomNumInfo)
    {
        if (isGoodWidth(Width) == false)
        {
            int[,] ErrorResult = new int[1, 1];
            ErrorResult[0, 0] = -1;
            return ErrorResult;
        }
        int i, j;
        int[,] MapArr = CreateMapBaseArr(Width, Height, roomNumInfo);
        //복도 생성
        Debug.Log(RanArr.Length);
        int k = 0;
        for (i = 0; i < Height; i++)
        {
            for (j = 0; j < Width; j++)
            {
                if (MapArr[i, j] != roomNumInfo.aisle)
                {
                    if (k == RanArr.Length)
                    {
                        break;
                    }
                    MapArr[i, j] = RanArr[k];
                    k++;
                }
            }
        } //방 비율대로 랜덤 생성/배치
        MapArr[Height - 1, 0] = roomNumInfo.check; //체크 포인트
        return MapArr;
    }

    public static int[] CreateMapRandArr(int width, int height, RoomPer percent) //비율대로 랜덤 방 생성하는 함수
    {
        if (isGoodWidth(width) == false)
        {
            int[] ErrorResult = new int[1];
            ErrorResult[0] = -1;
            return ErrorResult;
        }
        string result = "";
        int i;
        if (percent.Room1 + percent.Room2 + percent.Room3 != 1)
        {
            int[] ErrorResult = new int[1];
            ErrorResult[0] = -1;
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

    public static int[,] FirstToSec(string ArrNum, int width, int height) //방구조를 일차원배열에서 이차원배열로 변환하는 함수
    {
        int i, j;
        int k = 0;
        string[] numbers = ArrNum.Split(" ");
        int len = ArrNum.Length / 2;
        int[,] ResultMapArr = new int[height, width];
        for (i = 0; i < width; i++)
        {
            for (j = 0; j < height; j++)
            {
                ResultMapArr[j, i] = Int32.Parse(numbers[k]);
                k++;
            }
        }
        return ResultMapArr;
    }

    public static string SecToFirst(int[,] SecArr, int width, int height) //방구조를 이차원배열에서 일차원배열로 변환하는 함수
    {
        int i, j;
        string result = "";
        for (i = 0; i < width; i++)
        {
            for (j = 0; j < height; j++)
            {
                result = result + SecArr[j, i] + " ";
            }
        }
        return result;
    }

    public static int[] ShuffleArray(int[] array) //배열 내부 요소를 랜덤으로 섞는 함수
    {
        int random1, random2, k;
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

    public static int[,] CreateMapBaseArr(int width, int height, RoomNumInfo roomNumInfo) //맵 복도 배치하는 함수
    {
        if (isGoodWidth(width) == false)
        {
            int[,] ErrorResult = new int[1, 1];
            ErrorResult[0, 0] = -1;
            return ErrorResult;
        }
        int[,] MapArr = new int[height, width];
        int i, j;
        for (i = 0; i < width; i++)
        {
            MapArr[height - 2, i] = roomNumInfo.aisle;
            MapArr[height - 1, i] = roomNumInfo.aisle;
        }
        for (i = 1; i < width; i = i + 2)
        {
            for (j = 0; j < height - 2; j++)
            {
                MapArr[j, i] = roomNumInfo.aisle;
            }
        }
        return MapArr;
    }

    public static bool isGoodWidth(int width)
    {
        if (width % 2 == 1)
        {
            return true;
        }
        return false;
    }
}
