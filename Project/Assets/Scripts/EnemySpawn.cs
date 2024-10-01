using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//미완성

public class EnemySpawn : MonoBehaviour
{
    void Start()
    {
        int i, j;
        int k = 0;
        int Width = MapPlacing.instance.PblcWidth;
        int Height = MapPlacing.instance.PblcHeight;
        string[] sMap = MapPlacing.instance.result.Split(" ");
        int[,] Map = new int[Height, Width];
        for (i = 0; i < Height; i++)
        {
            for (j = 0; j < Width; j++)
            {
                Map[i, j] = int.Parse(sMap[k]);
                k++;
            }
        }
    }

    public static int[,] CreateEnemyRoom(int size, int amount)
    {
        int i, j;
        int[,] Room = new int[size, size];
        System.Random Rnum = new System.Random();
        int[,] SpawnPlace = new int[amount, amount];
        for (i = 0; i < size; i++)
        {
            for (j = 0; j < size; j++)
            {

            }
        }
        return SpawnPlace;
    }
}
