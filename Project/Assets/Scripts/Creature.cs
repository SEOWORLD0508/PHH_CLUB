using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class Creature : MonoBehaviour
{
    public float health;
    public float damage; // 무기 데미지 + 기본 데미지 
    public float attackRange;
    public float weaponDamage;
    public float weaponAngle; // 무기 공격 각도
    public string entity_name;

    public Item item;
    public Vector3 dir;
    public Vector3 lastPos;


    public virtual void Update()
    {
        dir = ((transform.position - lastPos).magnitude != 0) ? transform.position - lastPos : dir;
        lastPos = transform.position;
        Debug.DrawRay(transform.position, dir, Color.red);
    }
    public void Die()
    {
        Destroy(this.gameObject);
        int[] pRoom = RoomEvent.instance.FindPlayerRoom();
        if (pRoom[0] != -1)
        {
            RoomEvent.instance.MinusEnemy(pRoom[0], pRoom[1]);
        }
    }
}
