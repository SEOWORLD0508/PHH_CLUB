using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[System.Serializable]
public class AttackPattern
{
    public string Name = "Default";
    public float[] val = new float[2];
    public bool moveTowardTarget = false;
    /*0번 데미지, 1번 공격 이후 딜레이.(패턴 지속시간은 딜레이시간에 더해서 값넣어도 될듯... 애니메이터로 구현하면 코드 많이 줄일수 있음)
     * 애니메이터에서 event 사용해서 함수 호출시키고, 그때 공격 거리 안에 있는지 다시 확인해서 ㄱㄱ
     일반적은 패턴 1개(기본공격)만 있으면 되고 보스는 패턴 여러개 사용할예정 */

}
public class EnemyController : MonoBehaviour
{

    public float health;
    float nextAttackDelay; 
    float currentAttackT;
    public float attackRange;
    public LayerMask collideLayer;
    public float sightRange;
    public float speed;
    E_PathFinding pathF;


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

    bool dashing;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        pathF = GetComponent<E_PathFinding>();
        navmesh.updateRotation = false;
        navmesh.updateUpAxis = false;
        navmesh.speed = speed;
        navmesh.stoppingDistance = attackRange / 2;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit2d = Physics2D.Raycast(transform.position, (player.transform.position - transform.position).normalized, sightRange, collideLayer);
    


        playerInSight  = hit2d.transform.gameObject.layer != 7 ? true : false;
    
        if (currentAttackT > 0) currentAttackT -= Time.deltaTime;

        canAttack = playerInSight ? true : false;
        
        //Debug.Log("will add more conditions");

        if(canAttack && currentAttackT <= 0 && Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            Attack();
        }


        if (playerInSight && !dashing)
        {

            if (!useUnityPathfinding)
                transform.Translate(pathF.Main(obs, player.position, transform.position, speed) * speed * Time.deltaTime);
            else
            {
                //print("!");
                navmesh.SetDestination(player.position);
            }
        }

    }



    public virtual void Attack() 
    {
        int i = Random.Range(0, attackPatterns.Length);
        Debug.Log("Attacked... Pattern -> " + i);
        nextAttackDelay = attackPatterns[i].val[1];
        currentAttackT = nextAttackDelay;
        if (attackPatterns[i].moveTowardTarget)
        {
            StartCoroutine(DCoroutine(currentAttackT));
        }
    }

    public IEnumerator DCoroutine(float T)
    {
        dashing = true;
        navmesh.speed = Vector3.Distance(transform.position, player.position) / T;
        navmesh.SetDestination(player.position);
        yield return new WaitForSeconds(T);
        navmesh.speed = speed;
        dashing = false;
    }



    // movement parameter ->  [(x,y), (x,y), 1]  1 for plyer, 2 for obstacle
}
