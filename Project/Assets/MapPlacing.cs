using System;
using UnityEngine;

public class MapPlacing : MonoBehaviour
{
    private void Start()
    {
        System.Random num = new Random();
        string result = " ";
        int [,] MapArr = new int[6,6];
        int i;
        int j;
        for(i=0; i<6; i++){
            for(j=0; j<5; j++){
                MapArr[i,j] = num.Next(3);
            }
        }
        for(i=0; i<6; i++){
            MapArr[i,3] = 4; 
            MapArr[i,1] = 4; 
        }
        for(i=0; i<6; i++){
            MapArr[4,i] = 4; 
        }
        MapArr[3,2] = 5;
        MapArr[5,2] = 4; 
        MapArr[5,0] = 3; 
        for(i=0; i<6; i++){
            for(j=0; j<5; j++){
                result = result + MapArr[i,j] + "  ";
            }
            result = result + "\n ";
        }
        Console.WriteLine(result);
    }
}
