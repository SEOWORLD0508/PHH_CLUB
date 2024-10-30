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
    public float[] values; 
    public ItemType itemType;
    public Transform prefab;
    public bool useAble;
    public int specialIndex; // normal = 0,  if has special effect or etc... will be used
    public float price;

    //TODO : trlst kr --> eng
    /*
     * 무기용
    0 = pre-delay
    1 = after-delay
    2~ = index (weapon = damage & range) (potion = amount) (and more)
    2.damage
    3.range
    4.angle
    */

    //
    /*
     * 소모용 아이템
    0 = hp 회복 수치(%)(현재체력비례)

    */
    //
}
