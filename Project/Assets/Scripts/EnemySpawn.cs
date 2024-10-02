using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//미완성

public class EnemySpawn : MonoBehaviour
{
    void Start()
    {
        int i, j, k, l;

        int Width = MapPlacing.instance.PblcWidth; //맵의 가로
        int Height = MapPlacing.instance.PblcHeight; //맵의 세로

        int RoomSize = 20; //방의 가로 세로 길이

        string[] sMap = MapPlacing.instance.result.Split(" ");

        int[,] Map = new int[Height, Width];
        int[,,,] RoomInMap = new int[Height, Width, RoomSize, RoomSize]; //맵과 방의 정보가 저장됨, 4차원 배열

        k = 0;
        for (i = 0; i < Height; i++)
        {
            for (j = 0; j < Width; j++)
            {
                Map[i, j] = int.Parse(sMap[k]);
                k++;
                if (Map[i, j] <= 2)
                {
                    int[,] EnemyRoom = CreateEnemyRoom(RoomSize, MapPlacing.instance.EnemyPblc);
                    for (int rk = 0; rk < RoomSize; rk++)
                    {
                        for (l = 0; l < RoomSize; l++)
                        {
                            RoomInMap[i, j, rk, l] = EnemyRoom[rk, l];
                        }
                    }
                }

            }
        }

        // 방 출력 테스트
        /*
        string result = "";
        for (k = 0; k < RoomSize; k++)
        {
            for (l = 0; l < RoomSize; l++)
            {
                result = result + RoomInMap[0, 0, k, l] + " ";
            }
            result = result + "\n";
        }
        Debug.Log(result);
        */
    }

    //적 위치 표시된 방 생성 함수 / 0 : 적 없음 , 1 : 적 있음
    public static int[,] CreateEnemyRoom(int size, int amount)
    {
        int i, j;
        int k = 0;
        int[,] Room = new int[size, size];
        System.Random Rnum = new System.Random();
        for (i = 0; i < amount; i++)
        {
            Room[Rnum.Next(size), Rnum.Next(size)] = 1;
        }
        for (i = 0; i < size; i++)
        {
            for (j = 0; j < size; j++)
            {
                if (Room[i, j] != 1)
                {
                    Room[i, j] = 0;
                }
            }
        }
        return Room;
    }
}
