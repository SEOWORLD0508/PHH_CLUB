using System;
using UnityEngine;

public class MapPlacing : MonoBehaviour
{
    private void Start()
    {

        System.Random num = new System.Random();
        string result = " ";
        int[,] MapArr = new int[6, 6];



        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (MapArr[i, j] != 4 || MapArr[i, j] != 3)
                {
                    MapArr[i, j] = num.Next(3);
                }
            }
        }

        for (int i = 0; i < 6; i++)
        {
            //Console.WriteLine(num.Next(3));
            MapArr[i, 3] = 4;
            MapArr[i, 1] = 4;
        }
        for (int i = 0; i < 6; i++)
        {
            MapArr[4, i] = 4;
        }
        MapArr[5, 2] = 4;
        MapArr[5, 0] = 3;
        MapArr[3, 2] = 5;
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                result = result + MapArr[i, j] + "  ";
            }
            result = result + "\n ";
        }
        Console.WriteLine(result);
    }
}