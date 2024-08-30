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
    public bool specialIndex; // normal = 0,  if has special effect or etc... will be used

    //TODO : trlst kr --> eng
    /*
    0 = pre-delay
    1 = after-delay
    2~ = index (weapon = damage & range) (potion = amount) (and more)

    */
}
