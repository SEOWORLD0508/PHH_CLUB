using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName ="Item", menuName ="Item")]
public class Item : ScriptableObject
{

    public string ItemName;
    [TextArea]// 이렇게 하면 줄바꿈도 인식되는 문자열이 됨
    public string ItemDescription;
    public Sprite sprite;
    public float[] values; // 0번 데미지, 1번 선딜, 2번 후딜 고정. 3번은 사거리/ 장전속도로 할듯 ㅇㅇ
    public ItemType itemType;
    public Transform prefab;

    
}
