using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AttackPattern
{
    public string Name = "Default";
    public float[] val = new float[2];
    /*0번 데미지, 1번 공격 이후 딜레이.(패턴 지속시간은 딜레이시간에 더해서 값넣어도 될듯... 에니메이터로 구현하면 코드 많이 줄일수 있음)
     일반적은 패턴 1개(기본공격)만 있으면 되고 보스는 패턴 여러개 사용할예정 */

}
public class EnemyController : MonoBehaviour
{


    float nextAttackDelay; 
    float currentAttackT;
    public float attackRange;
    public LayerMask wallLayer;
    public float sightRange;


    public AttackPattern[] attackPatterns = new AttackPattern[1]; // 인스펙터상에서 값 넣고, 코드로 값은 안바꿀듯

    bool playerInSight;
    bool canAttack;
    Transform player;



    // Start is called before the first frame update
    void Start()
    {
         player = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit2d = Physics2D.Raycast(transform.position, (player.transform.position - transform.position).normalized, sightRange, wallLayer);

        playerInSight  = hit2d.collider == null ? true : false;
    
        if (currentAttackT > 0) currentAttackT -= Time.deltaTime;

        canAttack = playerInSight; 
        //Debug.Log("will add more conditions");

        if(canAttack && currentAttackT <= 0)
        {
            Attack();
        }

    }

    public virtual void Attack() 
    {
        int i = Random.Range(0, attackPatterns.Length);
        Debug.Log("Attacked... Pattern -> " + i);
        nextAttackDelay = attackPatterns[i].val[1];
        currentAttackT = nextAttackDelay;
    }



    // movement parameter ->  [(x,y), (x,y), 1]  1 for plyer, 2 for obstacle
}
