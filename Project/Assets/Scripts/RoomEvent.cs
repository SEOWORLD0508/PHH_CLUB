using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomEvent : MonoBehaviour
{
    TMP_Text popUpText;
    static int Width = 6;//MapPlacing.instance.PblcWidth; //맵의 가로
    static int Height = 4;//MapPlacing.instance.PblcHeight; //맵의 세로
    int i, j, k;
    bool[,] MapMobSpawned = new bool[Height, Width];
    GameObject player;
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
                        }
                    }
                    if (MapPlacing.instance.RoomInfo[i, j].DoorDirection != "Both")
                    {
                        if (Vector2.Distance(MapPlacing.instance.rooms[k].GetChild(6).position, player.transform.position) < 1)
                        {
                            //if(popUpText!=null) OutText();
                            Debug.Log("ddd");
                            if (Input.GetKeyDown(KeyCode.E))
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
                        if (Vector2.Distance(MapPlacing.instance.rooms[k].GetChild(6).position, player.transform.position) < 1)
                        {

                        }
                        else if (Vector2.Distance(MapPlacing.instance.rooms[k].GetChild(7).position, player.transform.position) < 1)
                        {

                        }
                    }
                }
                k++;
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

    public void OutText()
    {
        popUpText.text = "Press <color=yellow>E</color> to get out";
        popUpText.transform.position = Camera.main.WorldToScreenPoint(player.transform.position + Vector3.down * 1);
        popUpText.gameObject.SetActive(true);
    }

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
        else
        {

        }
        MapPlacing.instance.RoomInfo[i, j].isEntered = false;
    }

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
        else
        {

        }
        MapPlacing.instance.RoomInfo[i, j].isEntered = true;
    }
}
