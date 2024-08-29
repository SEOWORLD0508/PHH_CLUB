using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName ="Item", menuName ="Item")]
public class Item : ScriptableObject
{

    public string ItemName;
    [TextArea]// �̷��� �ϸ� �ٹٲ޵� �νĵǴ� ���ڿ��� ��
    public string ItemDescription;
    public Sprite sprite;
    public float[] values; // 0�� ������, 1�� ����, 2�� �ĵ� ����. 3���� ��Ÿ�/ �����ӵ��� �ҵ� ����
    public ItemType itemType;
    public Transform prefab;

    //TODO : trlst kr --> eng
}
