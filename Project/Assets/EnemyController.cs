using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AttackPattern
{
    public string Name = "Default";
    public float[] val = new float[2];
    /*0�� ������, 1�� ���� ���� ������.(���� ���ӽð��� �����̽ð��� ���ؼ� ���־ �ɵ�... ���ϸ����ͷ� �����ϸ� �ڵ� ���� ���ϼ� ����)
     �Ϲ����� ���� 1��(�⺻����)�� ������ �ǰ� ������ ���� ������ ����ҿ��� */

}
public class EnemyController : MonoBehaviour
{


    float nextAttackDelay; 
    float currentAttackT;
    public float attackRange;
    public LayerMask wallLayer;
    public float sightRange;


    public AttackPattern[] attackPatterns = new AttackPattern[1]; // �ν����ͻ󿡼� �� �ְ�, �ڵ�� ���� �ȹٲܵ�

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
