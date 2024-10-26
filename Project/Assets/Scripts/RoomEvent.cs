using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEvent : MonoBehaviour
{
    static int Width = MapPlacing.instance.PblcWidth; //맵의 가로
    static int Height = MapPlacing.instance.PblcHeight; //맵의 세로
    int i, j;
    bool[,] MapMobSpawned = new bool[Height, Width];
    void Start()
    {
        for (i = 0; i < Height; i++)
        {
            for (j = 0; j < Width; j++)
            {
                MapMobSpawned[i,j] = false;
            }
        }
    }
    void Update()
    {
        for (i = 0; i < Height; i++)
        {
            for (j = 0; j < Width; j++)
            {
                if (MapPlacing.instance.RoomInfo[i, j].isCleared == false && MapPlacing.instance.RoomInfo[i, j].EnemyAmount == 0)
                {
                    RoomClear(i, j);
                }
                else
                {
                    if (MapPlacing.instance.RoomInfo[i, j].isCleared == false && MapPlacing.instance.RoomInfo[i, j].EnemyAmount == MapPlacing.instance.EnemyPblc)
                    {
                        //적 생성 코드
                    }
                }
            }
        }
    }

    public static void MinusEnemy(int y, int x) //적 수 차감
    {
        MapPlacing.instance.RoomInfo[y, x].EnemyAmount--;
    }

    public static void RoomClear(int y, int x) //방 클리어 선언
    {
        MapPlacing.instance.RoomInfo[y, x].isCleared = true;
    }
}
