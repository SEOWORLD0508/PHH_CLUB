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
    /*0�� ������, 1�� ���� ���� ������.(���� ���ӽð��� �����̽ð��� ���ؼ� ���־ �ɵ�... ���ϸ����ͷ� �����ϸ� �ڵ� ���� ���ϼ� ����)
     * �ִϸ����Ϳ��� event ����ؼ� �Լ� ȣ���Ű��, �׶� ���� �Ÿ� �ȿ� �ִ��� �ٽ� Ȯ���ؼ� ����
     �Ϲ����� ���� 1��(�⺻����)�� ������ �ǰ� ������ ���� ������ ����ҿ��� */

}
public class EnemyController : MonoBehaviour
{


    float nextAttackDelay; 
    float currentAttackT;
    public float attackRange;
    public LayerMask collideLayer;
    public float sightRange;
    public float speed;
    E_PathFinding pathF;


    public AttackPattern[] attackPatterns = new AttackPattern[1]; // �ν����ͻ󿡼� �� �ְ�, �ڵ�� ���� �ȹٲܵ�

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

        canAttack = playerInSight; 
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
                navmesh.SetDestination(player.position);
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
