
////플레이어랑 몬스터 데미지 판정 구현
//// 공격자 클래스랑 피해자 클래스를 파라미터로 받아서 공격자와 피해자 사이의 거리를 계산하고, 공격자 클래스의 사정거리값을 기준으로 맞았는지 안맞았는지 판정한다. 
using System;
using System.Collections.Generic;
using UnityEngine;



public class Judgment : MonoBehaviour
{
    public List<Creature> get_enemy_list(Creature attacker)
    {
        var Creature_list = FindObjectsOfType<Creature>();
        var enemy_list = new List<Creature>();
        String name = attacker.entity_name;
        foreach(Creature creature in Creature_list)
        {
            if(creature.entity_name != name)
            {
                enemy_list.Add(creature);
            }
        }
        return enemy_list;
    }
    public bool is_in_attackRange(Creature attacker,Creature target)
    {
        float weaponAngle = attacker.item.values[4];
        float minimum = Mathf.Cos(Mathf.Deg2Rad * weaponAngle);
        //var pos1 = target.transform.position;
        //var pos2 = attacker.transform.position;
        //var direction_vec = pos2 - pos1; // 대상과 공격자 사이의 방향벡터
        //var facing_vec = attacker.transform.forward; // 바라보는 방향 ( 구현 안함)
        //var cos_sim = (Vector2.Dot(direction_vec,facing_vec)) / (direction_vec.magnitude * facing_vec.magnitude);


        Vector3 targetDir = (target.transform.position - attacker.transform.position).normalized;
        Debug.DrawRay(attacker.transform.position, targetDir, Color.blue, 10.0f);
        float dot = Vector3.Dot(attacker.dir.normalized, targetDir);

        //내적을 이용한 각 계산하기
        // thetha = cos^-1( a dot b / |a||b|)
        float theta = Mathf.Acos(dot) * Mathf.Rad2Deg;

        //Debug.Log("타겟과 AI의 각도 : " + theta);
        //if (theta <= weaponAngle) return true;
        //else return false;

        print(attacker);

        if (theta <= weaponAngle) {
            print("AttackInRange"); 
            return true;
        }
        else
        {
            print("AttackOutRange ,min = " + weaponAngle +" , val = " + theta);
            
            return false;
        }
    }
    public void Attack(Creature attacker)
    {
        var enemy_list = get_enemy_list(attacker);
        float attack_range = attacker.attackRange;
        foreach(Creature creature in enemy_list)
        {
            print(creature);
            var pos1 = creature.transform.position;
            var pos2 = attacker.transform.position;
            float distance = Vector2.Distance(pos1, pos2);
            bool isInSight = is_in_attackRange(attacker, creature);
            if(distance <= attack_range && isInSight) {
                creature.health -= 10; // 데미지 닳는 부분 (구현 안함)
            }
        }
    }
    

  
    
}
