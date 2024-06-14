using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Item", menuName ="Item")]
public class Item : ScriptableObject
{

    public string ItemName;
    public float[] values; // 0번 데미지, 1번 선딜, 2번 후딜 고정. 3번은 사거리/ 장전속도로 할듯 ㅇㅇ
    public ItemType itemType;


    
}
