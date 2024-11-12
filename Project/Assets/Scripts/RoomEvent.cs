using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomEvent : MonoBehaviour
{
    void Start()
    {
        
    [SerializeField]
    public static TextMeshProUGUI popUpText;
    static int Width = 6;//MapPlacing.instance.PblcWidth; //맵의 가로
    static int Height = 4;//MapPlacing.instance.PblcHeight; //맵의 세로
    int i, j, k;
    bool[,] MapMobSpawned = new bool[Height, Width];
    GameObject player;
    public bool bossRoomAble = false;
    void Start()
    {
        player = GameObject.Find("Player");
        for (i = 0; i < Height; i++)
        {
            for (j = 0; j < Width; j++)
            {
                MapMobSpawned[i, j] = false;
            }
        }
        //popUpText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        
    }

    //public delegate void KillEnemy(object sender, EventArgs e);
        k = 0;
        int l = 0;
        int doorDis = 3;
        double[,,,] CoordinateInMap;
        for (i = 0; i < Height; i++)
        {
            for (j = 0; j < Width; j++)
            {
                if (MapPlacing.instance.PblcMap[i, j] <= MapPlacing.instance.MaxEnemyRoom)
                {
                    if (MapPlacing.instance.RoomInfo[i, j].isCleared == false && MapPlacing.instance.RoomInfo[i, j].EnemyAmount == 0 && MapMobSpawned[i, j] == true)
                    {
                        RoomClear(i, j);
                        Debug.Log("Room [" + i + ", " + j + "] clear!");
                    }
                    else
                    {
                        if (MapPlacing.instance.RoomInfo[i, j].isCleared == false && MapPlacing.instance.RoomInfo[i, j].EnemyAmount == MapPlacing.instance.EnemyPblc)
                        {
                            //적 생성 코드
                            CoordinateInMap = EnemySpawn.instance.pCoordinateInMap;
                            //CoordinateInMap[i, j, 0, l], CoordinateInMap[i, j, 1, l]
                        }
                    }
                    if (MapPlacing.instance.RoomInfo[i, j].DoorDirection != "Both")
                    {
                        if (Vector2.Distance(MapPlacing.instance.rooms[k].GetChild(6).position, player.transform.position) < doorDis)
                        {
                            Debug.Log("room");
                            if (Input.GetKey(KeyCode.E))
                            {
                                if (MapPlacing.instance.RoomInfo[i, j].isEntered == true)
                                {
                                    GetOutRoom(i, j, MapPlacing.instance.RoomInfo[i, j].DoorDirection, k);
                                }
                                else
                                {
                                    GetInRoom(i, j, MapPlacing.instance.RoomInfo[i, j].DoorDirection, k);
                                }
                                Debug.Log(MapPlacing.instance.RoomInfo[i, j].isEntered);
                            }
                        }
                    }
                    else
                    {
                        if (Vector2.Distance(MapPlacing.instance.rooms[k].GetChild(6).position, player.transform.position) < doorDis)
                        {
                            Debug.Log("room");
                            if (Input.GetKey(KeyCode.E))
                            {
                                if (MapPlacing.instance.RoomInfo[i, j].isEntered == true)
                                {
                                    GetOutRoom(i, j, "Right", k);
                                }
                                else
                                {
                                    GetInRoom(i, j, "Right", k);
                                }
                            }
                        }
                        else if (Vector2.Distance(MapPlacing.instance.rooms[k].GetChild(7).position, player.transform.position) < doorDis)
                        {
                            Debug.Log("room");
                            if (Input.GetKey(KeyCode.E))
                            {
                                if (MapPlacing.instance.RoomInfo[i, j - 1].isEntered == true)
                                {
                                    MapPlacing.instance.RoomInfo[i, j - 1].isEntered = false;
                                    MapPlacing.instance.RoomInfo[i, j].isEntered = true;
                                    player.transform.position = MapPlacing.instance.rooms[k].GetChild(7).position + new Vector3(3, 0, 0);
                                }
                                else
                                {
                                    MapPlacing.instance.RoomInfo[i, j - 1].isEntered = true;
                                    MapPlacing.instance.RoomInfo[i, j].isEntered = false;
                                    player.transform.position = MapPlacing.instance.rooms[k].GetChild(7).position + new Vector3(-3, 0, 0);
                                }
                            }
                        }
                    }
                }
                k++;
            }
        }
    }

    //적 수 차감
    public static void MinusEnemy(int y, int x)
    {
        MapPlacing.instance.RoomInfo[y, x].EnemyAmount--;
    }

    //방 클리어 선언
    public static void RoomClear(int y, int x)
    {
        MapPlacing.instance.RoomInfo[y, x].isCleared = true;
    }

    //방 나가기
    public void GetOutRoom(int y, int x, string Direction, int k)
    {
        if (Direction == "Right")
        {
            player.transform.position = MapPlacing.instance.rooms[k].GetChild(6).position + new Vector3(3, 0, 0);
        }
        else if (Direction == "Left")
        {
            player.transform.position = MapPlacing.instance.rooms[k].GetChild(6).position + new Vector3(-3, 0, 0);
        }
        MapPlacing.instance.RoomInfo[i, j].isEntered = false;
    }

    //방 들어가기
    public void GetInRoom(int y, int x, string Direction, int k)
    {
        if (Direction == "Right")
        {
            player.transform.position = MapPlacing.instance.rooms[k].GetChild(6).position + new Vector3(-3, 0, 0);
        }
        else if (Direction == "Left")
        {
            player.transform.position = MapPlacing.instance.rooms[k].GetChild(6).position + new Vector3(3, 0, 0);
        }
        MapPlacing.instance.RoomInfo[i, j].isEntered = true;
    }
}
