using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Item", menuName ="Item")]
public class Item : ScriptableObject
{

    public string ItemName;
    public float[] values; // 0�� ������, 1�� ����, 2�� �ĵ� ����. 3���� ��Ÿ�/ �����ӵ��� �ҵ� ����
    public ItemType itemType;


    
}
