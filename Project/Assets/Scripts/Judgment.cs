
////플레이어랑 몬스터 데미지 판정 구현
//// 공격자 클래스랑 피해자 클래스를 파라미터로 받아서 공격자와 피해자 사이의 거리를 계산하고, 공격자 클래스의 사정거리값을 기준으로 맞았는지 안맞았는지 판정한다. 
using System;
using System.Collections.Generic;
using UnityEngine;



public class Judgment : MonoBehaviour
{
    public static Judgment instance; //인스턴스 생성
    private void Awake()
    {
        if (Judgment.instance == null)
        {
            Judgment.instance = this;
        }
    }

    public PlayerStatus Player_;
    void Start()
    {
        Player_ = FindObjectOfType<PlayerStatus>();
    }
    public List<Creature> get_enemy_list(Creature attacker)
    {
        var Creature_list = FindObjectsOfType<Creature>();
        var enemy_list = new List<Creature>();
        String name = attacker.entity_name;
        foreach (Creature creature in Creature_list)
        {
            if (creature.entity_name != name)
            {
                enemy_list.Add(creature);
            }
        }
        return enemy_list;
    }
    public bool is_in_attackRange(Creature attacker, Creature target)
    {
        float weaponAngle = attacker.item.values[4];
        float minimum = Mathf.Cos(Mathf.Deg2Rad * weaponAngle);
        var pos1 = target.transform.position;
        var pos2 = attacker.transform.position;
        var direction_vec = pos1 - pos2; // 대상과 공격자 사이의 방향벡터
        var facing_vec = attacker.dir; // 바라보는 방향 
        // z축은 고려할 필요 없어서 0으로 초기화
        direction_vec.z = 0;
        facing_vec.z = 0;
        var cos_sim = (Vector2.Dot(direction_vec, facing_vec)) / (direction_vec.magnitude * facing_vec.magnitude);
        bool standing = (facing_vec.x == 0 && facing_vec.y == 0 && facing_vec.z == 0);
        if (cos_sim <= 1 && cos_sim >= minimum || standing)
        {

            // 가만히 서서 때리면 걍 다 맞게 해둠


            Vector3 targetDir = (target.transform.position - attacker.transform.position).normalized; //공격할때 타겟을 향한 단위벡터
            Debug.DrawRay(attacker.transform.position, targetDir, Color.blue, 10.0f);
            Debug.DrawRay(attacker.transform.position, attacker.dir, Color.red, 10.0f);
            float dot = Vector3.Dot(attacker.dir, targetDir);

            dot = Mathf.Clamp(dot, -1, 1);


            //내적을 이용한 각 계산하기
            // thetha = cos^-1( a dot b / |a||b|)

            //dot가 -1 ~ 1 면 acos -> 각도로 변환 -> 내가 지정해둔 각도랑 비교해서
            //dot가 그거 밖이면 false


            float theta = Mathf.Acos(dot) * Mathf.Rad2Deg;

            //Debug.Log("타겟과 AI의 각도 : " + theta);
            //if (theta <= weaponAngle) return true;
            //else return false;




            if (theta <= weaponAngle || standing)
            {
                /*이부분 에러 납니다
                animator.SetBool("isIdle", false);
                animator.SetBool("isHit", true);
   
                animator.SetBool("isIdle", true);
                animator.SetBool("isHit", false);
                */
                print("AttackInRange");
                return true;
            }
            else
            {
                print("AttackOutRange ,min = " + weaponAngle + " , val = " + theta);

                return false;
            }
            ; //if문 안에만 반환문 있으면 에러나요
        }
        return false;
    }
    public void Attack(Creature attacker)
    {
        var enemy_list = get_enemy_list(attacker);
        float attack_range = attacker.attackRange;
        foreach (Creature creature in enemy_list)
        {

            var pos1 = creature.transform.position;
            var pos2 = attacker.transform.position;
            float distance = Vector2.Distance(pos1, pos2);
            bool isInSight = is_in_attackRange(attacker, creature);
            if (distance <= attack_range && isInSight)
            {
                print("health down");
                creature.health -= attacker.damage; // 데미지 닳는 부분

                if (creature.entity_name != "Player")
                {

                    if (Player_.heal_by_enemy_attack)
                    {
                        Player_.health += Player_.maxHp * 3 / 100;
                    }
                }
                if (creature.health <= 0)
                {
                    if (creature.entity_name == "Player")
                    {
                        GameManager.Instance.GameOver();
                    }
                    else
                    {
                        creature.Die();
                        if (Player_.heal_by_enemy_kill)
                        {
                            Player_.health += Player_.maxHp * 15 / 100;
                        }
                    }
                }
            }

        }

    }

}
