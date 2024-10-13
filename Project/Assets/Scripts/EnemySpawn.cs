using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public static EnemySpawn instance; //인스턴스 생성

    private void Awake()
    {
        if (EnemySpawn.instance == null)
        {
            EnemySpawn.instance = this;
        }
    }
    public int[,,,] pCoordinateInMap; //인스턴스 전달용
    void Start()
    {
        int i, j, k, l;

        int Width = MapPlacing.instance.PblcWidth; //맵의 가로
        int Height = MapPlacing.instance.PblcHeight; //맵의 세로

        int RoomSize = 20; //방의 가로 세로 길이

        string[] sMap = MapPlacing.instance.result.Split(" "); //MapPlacing.cs의 result 텍스트로 맵 가져옴

        int[,] Map = new int[Height, Width];

        int[,,,] CoordinateInMap = new int[Height, Width, 2, MapPlacing.instance.EnemyPblc]; //맵과 몬스터 스폰 좌표의 정보가 저장됨, 4차원 배열

        k = 0;
        for (i = 0; i < Height; i++)
        {
            for (j = 0; j < Width; j++)
            {
                Map[i, j] = int.Parse(sMap[k]);
                k++;
                if (Map[i, j] <= MapPlacing.instance.MaxEnemyRoom)
                {
                    int[,] SpawnCoordinate = CreateSpawnCoordiante(RoomSize, MapPlacing.instance.EnemyPblc);
                    for (int rk = 0; rk < 2; rk++)
                    {
                        for (l = 0; l < MapPlacing.instance.EnemyPblc; l++)
                        {
                            CoordinateInMap[i, j, rk, l] = SpawnCoordinate[rk, l];
                        }
                    }
                }
            }
        }
        pCoordinateInMap = CoordinateInMap;
        // 방 출력 테스트
        /*
        string result = "";
        for (k = 0; k < 2; k++)
        {
            for (l = 0; l < MapPlacing.instance.EnemyPblc; l++)
            {
                result = result + CoordinateInMap[0, 0, k, l] + " ";
            }
            result = result + "\n";
        }
        Debug.Log(result);
        */
    }

    //적 생성 좌표 반환하는 함수 / 반환값 :  SpawnCoordinate[0 or 1(0이 y축, 1이 x축), (적 양)]
    public static int[,] CreateSpawnCoordiante(int RoomSize, int amount)
    {
        int i;
        int[,] SpawnCoordinate = new int[2, amount];
        System.Random Rnum = new System.Random();
        for (i = 0; i < amount; i++)
        {
            SpawnCoordinate[0, i] = Rnum.Next(RoomSize);
            SpawnCoordinate[1, i] = Rnum.Next(RoomSize);
        }
        return SpawnCoordinate;
    }
}
