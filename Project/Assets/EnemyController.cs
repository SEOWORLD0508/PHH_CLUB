using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{


    public float attackDelay;
    float currentAttackT;
    public float attackRange;
    public LayerMask wallLayer;
    public float sightRange;

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
        Debug.Log("will add more conditions");

        if(canAttack && currentAttackT <= 0)
        {
            currentAttackT = attackDelay;
            Debug.Log("Attack");
        }

    }

    public virtual void Attack() 
    {

    }
}
