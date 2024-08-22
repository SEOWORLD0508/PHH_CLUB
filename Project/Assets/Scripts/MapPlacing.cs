//모듈 선언
using System;
using System.Collections.Generic;
using UnityEngine;

//맵 크기
struct MapSize
{
    public int width;
    public int height;
}

//방 정보
public struct RoomStr
{
    public bool isEntered; //플레이어가 있는지 없는지
    public int RoomKind;  //방 종류
    public bool isCleared; //깼는지 안깼는지
    public int EnemyAmount; //몬스터 수
}

//맵의 방 수 비율
public struct RoomPer
{
    public double Room1;
    public double Room2;
    public double Room3;
}

//방 종류 정수로 표현
public struct RoomNumInfo
{
    public int aisle;
    public int check;
}

public class MapPlacing : MonoBehaviour
{
    //코드에서 유니티에 상호작용 할수 있게 함
    [SerializeField]
    Transform[] roomPrefabs;

    [SerializeField]
    List<Transform> rooms;

    [SerializeField]
    float GridSize;
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
        mapSize.width = 5; //홀수여야 함
        mapSize.height = 6;
        int Width = mapSize.width;
        int Height = mapSize.height;
        
        RoomNumInfo roomNumInfo;
        roomNumInfo.aisle = 4;
        roomNumInfo.check = 3;
        
        RoomPer roomPer;
        roomPer.Room1 = 0.3;
        roomPer.Room2 = 0.4;
        roomPer.Room3 = 0.3;

        int[] RanArr = CreateMapRandArr(Width, Height, roomPer);
        int[,] Map = CreateMap(Width, Height, RanArr, roomNumInfo);
        RoomStr[] RoomInfo = CreateMapStr(Width, Height, RanArr);

        //출력 / 유니티에 반영
        for (i = 0; i < Height; i++)
        {
            for (j = 0; j < Width; j++)
            {
                result = result + Map[i, j] + " ";
                rooms.Add(Instantiate(roomPrefabs[Map[i, j]], new Vector2(j * GridSize, -1 * i * GridSize), Quaternion.identity));
            }
            result = result + "\n";
        }
        Debug.Log(result);
    }

    //방 정보 배열 생성 함수
    public RoomStr[] CreateMapStr(int Width, int Height, int[] RanArr)
    {
        RoomStr[] roomStr = new RoomStr[RanArr.Length];
        int i;
        for (i = 0; i < RanArr.Length; i++)
        {
            roomStr[i].isEntered = false;
            roomStr[i].RoomKind = RanArr[i];
            roomStr[i].isCleared = false;
            roomStr[i].EnemyAmount = 0;
        }
        return roomStr;
    }

    //이차원 배열 맵 생성 함수
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

    //비율대로 랜덤 방 생성하는 함수
    public static int[] CreateMapRandArr(int width, int height, RoomPer percent) 
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

    //방구조를 일차원배열에서 이차원배열로 변환하는 함수
    public static int[,] FirstToSec(string ArrNum, int width, int height)
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

    //방구조를 이차원배열에서 일차원배열로 변환하는 함수
    public static string SecToFirst(int[,] SecArr, int width, int height)
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

    //배열 내부 요소를 랜덤으로 섞는 함수
    public static int[] ShuffleArray(int[] array)
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

    //맵 복도 배치하는 함수
    public static int[,] CreateMapBaseArr(int width, int height, RoomNumInfo roomNumInfo)
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

    //맵 가로가 홀수인지 확인하는 함수
    public static bool isGoodWidth(int width)
    {
        if (width % 2 == 1)
        {
            return true;
        }
        return false;
    }
}
