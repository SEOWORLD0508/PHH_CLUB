using System;
using System.Collections.Generic;
using UnityEngine;

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
        MapArr[5,0] = 3; //[y,x] วüลย?
        for(i=0; i<6; i++){
            for(j=0; j<5; j++){
                result = result + MapArr[i,j] + "  ";
                rooms.Add(Instantiate(roomPrefabs[MapArr[i, j]], new Vector2(j * GridSize,
                    -1 * i * GridSize), Quaternion.identity));
            }
            result = result + "\n ";
        }
        Debug.Log(result);

        //Console.WriteLine(result);
      //  Debug.Log(result);


    }
}
