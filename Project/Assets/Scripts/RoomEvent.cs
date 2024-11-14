using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEvent : MonoBehaviour
{
    public static RoomEvent instance; //인스턴스 생성
    private void Awake()
    {
        if (RoomEvent.instance == null)
        {
            RoomEvent.instance = this;
        }
    }
    static int Width = 6;//MapPlacing.instance.PblcWidth; //맵의 가로
    static int Height = 4;//MapPlacing.instance.PblcHeight; //맵의 세로

    [SerializeField]
    public Transform enemies;

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
    }

    void Update()
    {
        k = 0;
        int l;
        int doorDis = 3;
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
                        if (MapPlacing.instance.RoomInfo[i, j].isCleared == false && MapPlacing.instance.RoomInfo[i, j].EnemyAmount == MapPlacing.instance.EnemyPblc && MapMobSpawned[i, j] == false && MapPlacing.instance.RoomInfo[i, j].isEntered == true)
                        {
                            //적 생성 코드
                            for (l = 0; l < MapPlacing.instance.EnemyPblc; l++)
                            {
                                Transform enemy = Instantiate(enemies, MapPlacing.instance.rooms[k].position + new Vector3(Random.Range(-8, 8), Random.Range(-8, 8), 0), Quaternion.identity);
                                enemy.eulerAngles = new Vector3(-90, 0, 0);
                            }
                            MapMobSpawned[i, j] = true;
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
    public void MinusEnemy(int y, int x)
    {
        MapPlacing.instance.RoomInfo[y, x].EnemyAmount--;
        Debug.Log("Enemy died " + MapPlacing.instance.RoomInfo[y, x].EnemyAmount);
    }

    //방 클리어 선언
    public static void RoomClear(int y, int x)
    {
        MapPlacing.instance.RoomInfo[y, x].isCleared = true;
        GameManager.Instance.Gold += (float)Random.Range(500, 550) * GameManager.Instance.goldDegree;
        GameManager.Instance.UpdateGoldUI();
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

    public int[] FindPlayerRoom()
    {
        int i, j;
        int[] result = new int[2];
        for (i = 0; i < Height; i++)
        {
            for (j = 0; j < Width; j++)
            {
                if (MapPlacing.instance.RoomInfo[i, j].isEntered == true)
                {
                    result[0] = i;
                    result[1] = j;
                    return result;
                }
            }
        }
        int[] noPlayer = new int[1];
        noPlayer[0] = -1;
        return noPlayer;
    }
}
