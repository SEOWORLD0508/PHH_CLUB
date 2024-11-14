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
    public int specialIndex; // Weapon = 0 / 회복 = 1 / 침식도 = 2 / 

    
    //TODO : trlst kr --> eng
    /*
    0 = 가격
    1 = 안씀
    2.damage / 혹은 회복량 % / 혹은 침식도 +
    3.range
    4.angle
    */

   
    public void PassiveItemEffect()
    {
        PlayerStatus Player_ = FindObjectOfType<PlayerStatus>();
        PlayerMovement Player_Movement = FindObjectOfType<PlayerMovement>();
        switch (ItemName)
        {
            case("가학"):
                Player_.heal_by_enemy_attack = true;
                break;
            case ("금욕의 주머니"):
                GameManager.Instance.goldDegree = 0.75f;
                Player_.weaponDamageCoeff += 0.2f;
                // 인벤토리 크기 -> 무기 데미지 계수 20%p 증가로 바꿈 
                break;
            case ("멈출 수 없는 힘"):
                Player_.damage +=  6;
                break;
            case ("비열한 칼날"):
                // 시간안에 할 수 있음?
                break;
            case ("스캐빈저"):
                Player_.heal_by_enemy_kill = true;
                break;
            case ("신속한 보법"):
                Player_Movement.maxSpeed = Player_Movement.maxSpeed * 2;
                Player_Movement.dashable = false;
                break;
            case ("야수의 심장"):
                Player_.health_to_damage = true;
                break;
            case ("완강한 마음"):
                Player_.maxVamp = Player_.maxVamp * 1.5f;
                break;
            case ("웨폰 메뉴얼"):
                Player_.weaponDamageCoeff += 0.5f;
                break;
            case ("탐욕의 주머니"):
                GameManager.Instance.goldDegree = 1.5f;
                break;
            case ("윤회"):
                GameManager.Instance.rebirth = true;
                break;

        }
    }
}


