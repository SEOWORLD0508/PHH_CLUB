using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


[System.Serializable]
public class AttackPattern
{
    public string Name = "Default";
    public float[] val = new float[3];
  
    /*0번 데미지, 1번 공격 이전 딜레이.2번 공격 이후 딜레이(패턴 지속시간은 딜레이시간에 더해서 값넣어도 될듯... 애니메이터로 구현하면 코드 많이 줄일수 있음)
     * 
     일반적은 패턴 1개(기본공격)만 있으면 되고 보스는 패턴 여러개 사용할예정 */

}
public class EnemyController : Creature
{
    float currentAttackT;
    public LayerMask collideLayer;
    public float sightRange;
    public float speed;
    E_PathFinding pathF;
    public Animator animator;
    //public Animator animator;


    public AttackPattern[] attackPatterns = new AttackPattern[1]; // 인스펙터상에서 값 넣고, 코드로 값은 안바꿀듯

    bool playerInSight;
    bool canAttack;
    Transform player;

    [SerializeField]
    Vector2[] obs;

    [SerializeField]
    NavMeshAgent navmesh;
    [SerializeField]
    bool useUnityPathfinding;

    [SerializeField]
    Transform Warning;

    bool attacking;


    [SerializeField]
    Judgment Judgment;





    // Start is called before the first frame update
    void Start()
    {
        entity_name = "Monster";
        damage = 0;
        weaponDamage = item.values[2];
        damage += weaponDamage;
        attackRange = item.values[3];
        weaponAngle = item.values[4];
        player = FindObjectOfType<PlayerMovement>().transform;
        pathF = GetComponent<E_PathFinding>();
        navmesh.updateRotation = false;
        navmesh.updateUpAxis = false;
        navmesh.speed = speed;
        navmesh.stoppingDistance = attackRange / 2;
        dir = player.transform.position - transform.position;
        attacking = false;
        /*
        animator = GetComponent<Animator>();
        animator.SetBool("isAttack", false);
        animator.SetBool("isHit", false);
        animator.SetBool("isDie", false);
        animator.SetBool("isWalk", false);
        animator.SetBool("isIdle", true);
        */
    }

    // Update is called once per frame
    public override void Update()
    {
        //dir = navmesh.path.corners[0];
        //Debug.DrawRay(transform.position, dir.normalized, Color.red, 10.0f);
        RaycastHit2D hit2d = Physics2D.Raycast(transform.position, (player.transform.position - transform.position).normalized, sightRange, collideLayer);


        playerInSight = hit2d.transform && hit2d.transform.gameObject.layer != 7 ? true : false;

        if (currentAttackT > 0) currentAttackT -= Time.deltaTime;

        canAttack = playerInSight ? true : false;

        //Debug.Log("will add more conditions");

        if (canAttack && currentAttackT <= 0 && Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            Attack();
            
        }
        else
        {
            dir = (player.transform.position - transform.position).normalized;
        }


        if (playerInSight && !attacking)
        {

            if (!useUnityPathfinding)
                transform.Translate(pathF.Main(obs, player.position, transform.position, speed) * speed * Time.deltaTime);
            else
            {
                navmesh.SetDestination(player.position);
                animator.SetBool("isIdle", false);
                animator.SetBool("isWalk", true);
            }
        }
        else
        {
            navmesh.SetDestination(transform.position);
            animator.SetBool("isWalk", false);
            animator.SetBool("isIdle", !attacking);

        }

        
        

    }




    public virtual void Attack()
    {

        int i = Random.Range(0, attackPatterns.Length);
        //Debug.Log("Attacked... Pattern -> " + i);

        currentAttackT = attackPatterns[i].val[1] + attackPatterns[i].val[2];


        StartCoroutine(DCoroutine(attackPatterns[i].val[1], attackPatterns[i].val[2]));


   
    }



    public IEnumerator DCoroutine(float _t1, float _t2)
    {

 
        attacking = true;
        Warning.gameObject.SetActive(true);
        Warning.gameObject.transform.position = transform.position;
        navmesh.speed = 0;


        animator.SetBool("isWalk", false);
        animator.SetBool("isIdle", false);
        animator.SetBool("isAttack", true);

        yield return new WaitForSeconds(_t1); // 선딜
        Judgment.Attack(this);


        Warning.gameObject.SetActive(false);
        navmesh.speed = speed;


        yield return new WaitForSeconds(_t2); // 후딜
        //navmesh.speed = speed;

        animator.SetBool("isAttack", false);
        animator.SetBool("isIdle", true);
        attacking = false;

    }
    
}
