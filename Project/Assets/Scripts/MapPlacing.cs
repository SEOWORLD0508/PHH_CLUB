//모듈 선언
using NavMeshPlus.Components;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//맵 크기
struct MapSize
{
    public int width;
    public int height;
}

//방 정보
public struct RoomStr
{
    public bool isEntered; //플레이어가 있는지 없는지
    public int RoomKind;  //방 종류
    public bool isCleared; //깼는지 안깼는지
    public int EnemyAmount; //몬스터 수
    public string DoorDirection; //방문 방향, Right, Left, Both
}

//맵의 방 수 비율
public struct RoomPer
{
    public double Room1;
    public double Room2;
    public double Room3;
}

//방 종류 정수로 표현
public struct RoomNumInfo
{
    public int aisle; //복도
    public int check; //체크 포인트
    public int boss; //보스방
    public int maxEnemyRoom; //적이 있는 방은 0번 부터 시작, 0~maxEnemyRoom번 방에서만 적이 스폰됨
}

public class MapPlacing : MonoBehaviour
{
    public static MapPlacing instance; //인스턴스 생성
    private void Awake()
    {
        if (MapPlacing.instance == null)
        {
            MapPlacing.instance = this;
        }
    }

    //코드에서 유니티에 상호작용 할수 있게 함
    [SerializeField]
    public Transform[] roomPrefabs;
    public Transform Door;
    public string result = ""; //인스턴스 전달용
    public static int EnemyPerRoom = 5; //방당 스폰될 적 수
    public int EnemyPblc = EnemyPerRoom; //인스턴스 전달용
    public int PblcWidth = 6; //인스턴스 전달용
    public int PblcHeight = 4; //인스턴스 전달용
    public int MaxEnemyRoom = 2; //인스턴스 전달용
    public RoomNumInfo roomNumInfo;
    public RoomStr[,] RoomInfo;
    public int[,] PblcMap;
    [SerializeField]
    public List<Transform> rooms;
    public bool bossRoomOpened = false;
    public Transform StoreKeeper;

    [SerializeField]
    float GridSize = 19.5f;

    [SerializeField]
    NavMeshSurface nav;

    private void Start()
    {
        /*방 종류
        0~2 : 랜덤으로 배치되는 방(상자방, 몬스터 방 etc..)
        3 : 체크 포인트
        4 : 복도(플레이어가 이동하는 곳)
        */

        EnemyPerRoom = 0;
        int i, j, k;
        int cnt = 0;
        MapSize mapSize;
        mapSize.width = PblcWidth; //홀수여야 함 원래거반 
        mapSize.height = PblcHeight;
        int Width = mapSize.width / 2;
        int Height = mapSize.height;

        roomNumInfo.boss = 5;
        roomNumInfo.aisle = 4;
        roomNumInfo.check = 3;
        roomNumInfo.maxEnemyRoom = MaxEnemyRoom;

        RoomPer roomPer;
        roomPer.Room1 = 0.3;
        roomPer.Room2 = 0.4;
        roomPer.Room3 = 0.3;
        int[] RanArr = CreateMapRandArr(Width, Height, roomPer);
        int[,] hfMap1 = CreateMap(Width, Height, RanArr, roomNumInfo);
        int[] RanArr2 = CreateMapRandArr(Width, Height, roomPer);
        int[,] hfMap2 = CreateMap(Width, Height, RanArr2, roomNumInfo);
        int[,] Map = new int[PblcHeight, PblcWidth];

        for (i = 0; i < Height; i++)
        {
            for (j = 0; j < Width; j++)
            {
                Debug.Log(j);
                Map[i, j] = hfMap1[i, j];
            }
        }

        for (i = 0; i < Height; i++)
        {
            for (j = Width; j < Width * 2; j++)
            {
                Map[i, j] = hfMap2[i, j - Width];
            }
        }

        Map[Height - 1, 0] = roomNumInfo.check;
        Map[Height - 3, Width] = roomNumInfo.boss;
        Map[Height - 3, Width - 1] = roomNumInfo.boss;
        RoomInfo = CreateMapStr(PblcWidth, Height, roomNumInfo, Map);
        PblcMap = Map;
        //출력 / 유니티에 반영
        cnt = 0;
        for (i = 0; i < Height; i++)
        {
            for (j = 0; j < PblcWidth; j++)
            {
                result = result + Map[i, j] + " ";
                rooms.Add(Instantiate(roomPrefabs[Map[i, j]], new Vector2(j * GridSize, -1 * i * GridSize), Quaternion.identity));
                if (RoomInfo[i, j].RoomKind != roomNumInfo.aisle && RoomInfo[i, j].RoomKind != roomNumInfo.check && RoomInfo[i, j].RoomKind != roomNumInfo.boss)
                {
                    //Debug.Log(RoomInfo[i, j].DoorDirection);
                    if (RoomInfo[i, j].DoorDirection == "Right")
                    {
                        Transform door = Instantiate(Door, rooms[cnt].GetChild(1).Find("TestWall (2)").position, Quaternion.identity);
                        door.transform.parent = rooms[cnt].transform;
                    }
                    else if (RoomInfo[i, j].DoorDirection == "Left")
                    {
                        Transform door = Instantiate(Door, rooms[cnt].GetChild(1).Find("TestWall (3)").position, Quaternion.identity);
                        door.transform.parent = rooms[cnt].transform;
                    }
                    else
                    {
                        Transform door = Instantiate(Door, rooms[cnt].GetChild(1).Find("TestWall (2)").position, Quaternion.identity);
                        Transform door2 = Instantiate(Door, rooms[cnt].GetChild(1).Find("TestWall (3)").position + new Vector3(0.25f, 0, 0), Quaternion.identity);
                        door.transform.parent = rooms[cnt].transform;
                        door2.transform.parent = rooms[cnt].transform;
                    }
                }
                //체크 포인트 룸 프리팹 완성되면 
                if (Map[i, j] == roomNumInfo.check)
                {
                    Instantiate(StoreKeeper, rooms[cnt].position, Quaternion.identity);
                }
                
                if (Map[i, j] == roomNumInfo.aisle)
                {
                    if (i < Height - 2)
                    {
                        if (i == 0)
                        {
                            Destroy(rooms[cnt].GetChild(1).Find("TestWall (1)").gameObject);
                            Destroy(rooms[cnt].GetChild(1).Find("TestWall (2)").gameObject);
                            Destroy(rooms[cnt].GetChild(1).Find("TestWall (3)").gameObject);
                        }
                        else
                        {
                            Destroy(rooms[cnt].GetChild(1).Find("TestWall").gameObject);
                            Destroy(rooms[cnt].GetChild(1).Find("TestWall (1)").gameObject);
                            Destroy(rooms[cnt].GetChild(1).Find("TestWall (2)").gameObject);
                            Destroy(rooms[cnt].GetChild(1).Find("TestWall (3)").gameObject);
                        }

                    }
                    else if (i == Height - 2)
                    {
                        if (j == 0)
                        {
                            Destroy(rooms[cnt].GetChild(1).Find("TestWall").gameObject);
                            Destroy(rooms[cnt].GetChild(1).Find("TestWall (1)").gameObject);
                            Destroy(rooms[cnt].GetChild(1).Find("TestWall (2)").gameObject);
                        }
                        else if (j == PblcWidth - 1)
                        {
                            Destroy(rooms[cnt].GetChild(1).Find("TestWall").gameObject);
                            Destroy(rooms[cnt].GetChild(1).Find("TestWall (1)").gameObject);
                            Destroy(rooms[cnt].GetChild(1).Find("TestWall (3)").gameObject);
                        }
                        else
                        {
                            Destroy(rooms[cnt].GetChild(1).Find("TestWall").gameObject);
                            Destroy(rooms[cnt].GetChild(1).Find("TestWall (1)").gameObject);
                            Destroy(rooms[cnt].GetChild(1).Find("TestWall (2)").gameObject);
                            Destroy(rooms[cnt].GetChild(1).Find("TestWall (3)").gameObject);
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            Destroy(rooms[cnt].GetChild(1).Find("TestWall").gameObject);
                            Destroy(rooms[cnt].GetChild(1).Find("TestWall (1)").gameObject);
                        }
                        else if (j == PblcWidth - 1)
                        {
                            Destroy(rooms[cnt].GetChild(1).Find("TestWall").gameObject);
                            Destroy(rooms[cnt].GetChild(1).Find("TestWall (3)").gameObject);
                        }
                        else
                        {
                            Destroy(rooms[cnt].GetChild(1).Find("TestWall").gameObject);
                            Destroy(rooms[cnt].GetChild(1).Find("TestWall (2)").gameObject);
                            Destroy(rooms[cnt].GetChild(1).Find("TestWall (3)").gameObject);
                        }
                    }
                }
                cnt++;
            }
            //result = result + "\n";
        }
        //Debug.Log(result);
        nav.BuildNavMesh();
    }

    private void Update()
    {
        if(RoomEvent.instance.bossRoomAble == true && bossRoomOpened == false)
        {
            //보스방 오픈 코드
            bossRoomOpened = true;
            int cntt = 0;
            int i, j;
            for(i = 0; i < PblcHeight - 3; i++)
            {
                for(j = 0; j < PblcWidth / 2; j++)
                {
                    cntt++;
                }
            }
            Transform door = Instantiate(Door, new Vector3(rooms[cntt].GetChild(1).Find("TestWall (3)").position.x + 0.25f, rooms[cntt].GetChild(1).Find("TestWall (1)").position.y, 0), Quaternion.identity);
            door.transform.parent = rooms[cntt].transform;
            //rooms[cnt].GetChild(1).Find("TestWall (1)").position.y rooms[cntt].GetChild(1).Find("TestWall (1)").position
        }
    }
    
    //방 정보 배열 생성 함수
    public static RoomStr[,] CreateMapStr(int Width, int Height, RoomNumInfo roomNumInfo, int[,] MapArr)
    {
        RoomStr[,] roomStr = new RoomStr[Height, Width];
        int i, j;
        for (i = 0; i < Height; i++)
        {
            for (j = 0; j < Width; j++)
            {
                roomStr[i, j].isEntered = false;
                roomStr[i, j].RoomKind = MapArr[i, j];
                roomStr[i, j].isCleared = false;
                if (MapArr[i, j] != roomNumInfo.aisle && MapArr[i, j] != roomNumInfo.check)
                {
                    if (j < (Width / 2) - 1)
                    {
                        roomStr[i, j].DoorDirection = "Right";
                    }
                    else if (j > (Width / 2) || j == (Width / 2) - 1)
                    {
                        roomStr[i, j].DoorDirection = "Left";
                    }
                    else if (j == (Width / 2)/* || j == (Width / 2)*/)
                    {
                        roomStr[i, j].DoorDirection = "Both";
                    }
                }
                if (roomStr[i, j].RoomKind <= roomNumInfo.maxEnemyRoom)
                {
                    roomStr[i, j].EnemyAmount = EnemyPerRoom;
                }
                else
                {
                    roomStr[i, j].EnemyAmount = 0;
                }
            }
        }
        return roomStr;
    }

    //이차원 배열 맵 생성 함수
    public static int[,] CreateMap(int Width, int Height, int[] RanArr, RoomNumInfo roomNumInfo)
    {
        if (isGoodWidth(Width) == false)
        {
            int[,] ErrorResult = new int[1, 1];
            ErrorResult[0, 0] = -1;
            return ErrorResult;
        }
        int i, j;
        int[,] MapArr = CreateMapBaseArr(Width, Height, roomNumInfo);
        //복도 생성
        Debug.Log(RanArr.Length);
        int k = 0;
        for (i = 0; i < Height; i++)
        {
            for (j = 0; j < Width; j++)
            {
                if (MapArr[i, j] != roomNumInfo.aisle)
                {
                    if (k == RanArr.Length)
                    {
                        break;
                    }
                    MapArr[i, j] = RanArr[k];
                    k++;
                }
            }
        } //방 비율대로 랜덤 생성/배치
        //MapArr[Height - 1, 0] = roomNumInfo.check; //체크 포인트
        return MapArr;
    }

    //비율대로 랜덤 방 생성하는 함수
    public static int[] CreateMapRandArr(int width, int height, RoomPer percent)
    {
        if (isGoodWidth(width) == false)
        {
            int[] ErrorResult = new int[1];
            ErrorResult[0] = -1;
            return ErrorResult;
        }
        string result = "";
        int i;
        if (percent.Room1 + percent.Room2 + percent.Room3 != 1)
        {
            int[] ErrorResult = new int[1];
            ErrorResult[0] = -1;
            return ErrorResult;
        }
        int len = ((height / 2) + 1) * (width - 2);
        int[] MapFirst = new int[len];
        for (i = 0; i < len; i++)
        {
            if (i <= len * percent.Room1)
            {
                MapFirst[i] = 0;
            }
            else if (i <= len * (percent.Room1 + percent.Room2))
            {
                MapFirst[i] = 1;
            }
            else if (i <= len * (percent.Room1 + percent.Room2 + percent.Room3))
            {
                MapFirst[i] = 2;
            }
        }
        MapFirst = ShuffleArray(MapFirst);
        return MapFirst;
    }

    //방구조를 일차원배열에서 이차원배열로 변환하는 함수
    public static int[,] FirstToSec(int[] ArrNum, int width, int height)
    {
        int i, j;
        int k = 0;
        int[,] ResultMapArr = new int[height, width];
        for (i = 0; i < width; i++)
        {
            for (j = 0; j < height; j++)
            {
                if (k == ArrNum.Length)
                {
                    break;
                }
                ResultMapArr[j, i] = ArrNum[k];
                k++;
            }
        }
        return ResultMapArr;
    }

    //방구조를 이차원배열에서 일차원배열로 변환하는 함수
    public static int[] SecToFirst(int[,] SecArr, int width, int height)
    {
        int i, j;
        int k = 0;
        int[] result = new int[width * height];
        for (i = 0; i < width; i++)
        {
            for (j = 0; j < height; j++)
            {
                result[k] = SecArr[j, i];
                k++;
            }
        }
        return result;
    }

    //배열 내부 요소를 랜덤으로 섞는 함수
    public static int[] ShuffleArray(int[] array)
    {
        int random1, random2, k;
        System.Random num = new System.Random();
        for (int i = 0; i < array.Length; ++i)
        {
            random1 = num.Next(array.Length);
            random2 = num.Next(array.Length);
            k = array[random1];
            array[random1] = array[random2];
            array[random2] = k;
        }
        return array;
    }

    //맵 복도 배치하는 함수
    public static int[,] CreateMapBaseArr(int width, int height, RoomNumInfo roomNumInfo)
    {
        if (isGoodWidth(width) == false)
        {
            int[,] ErrorResult = new int[1, 1];
            ErrorResult[0, 0] = -1;
            return ErrorResult;
        }
        int[,] MapArr = new int[height, width];
        int i, j;
        for (i = 0; i < width; i++)
        {
            MapArr[height - 2, i] = roomNumInfo.aisle;
            MapArr[height - 1, i] = roomNumInfo.aisle;
        }
        for (i = 1; i < width; i = i + 2)
        {
            for (j = 0; j < height - 2; j++)
            {
                MapArr[j, i] = roomNumInfo.aisle;
            }
        }
        //MapArr[height - 3, width - 1] = roomNumInfo.boss;
        return MapArr;
    }

    //맵 가로가 홀수인지 확인하는 함수
    public static bool isGoodWidth(int width)
    {
        if (width % 2 == 1)
        {
            return true;
        }
        return false;
    }
}
