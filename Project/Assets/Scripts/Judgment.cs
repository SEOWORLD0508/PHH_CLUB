
// 플레이어랑 몬스터 데미지 판정 구현
// 공격자 클래스랑 피해자 클래스를 파라미터로 받아서 공격자와 피해자 사이의 거리를 계산하고, 공격자 클래스의 사정거리값을 기준으로 맞았는지 안맞았는지 판정한다. 
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public Vector3 CirclePoint(float angle,Creature victim){
    angle += victim.rotation.eulerAngles.y;
    return new Vector3(Mathf.sin)
}
public class AttackedEvent : MonoBehaviour
{
    bool attacker(Creature attacker, Creature victim)
    {
        bool hit;
        if (hit)
        {
            Debug.Log("Hit!");
        }
        else
        {
            Debug.Log("Avoid!");
        }
        return hit;
    }
}

