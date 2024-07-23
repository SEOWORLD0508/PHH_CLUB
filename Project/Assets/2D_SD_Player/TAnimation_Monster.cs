using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TAnimation_Monster : MonoBehaviour
{
    Animator anim;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (anim.GetBool("isAttack") == false)
            {
                anim.SetBool("isAttack", true);
                anim.SetBool("isIdle", false);
            }

        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (anim.GetBool("isHit") == false)
            {
                anim.SetBool("isHit", true);
                anim.SetBool("isIdle", false);
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (anim.GetBool("isDie") == false)
            {
                anim.SetBool("isDie", true);
                anim.SetBool("isIdle", false);
            }
        }
        else if (Input.GetKey(KeyCode.F))
        {
            if (anim.GetBool("isWalk") == false)
            {
                anim.SetBool("isWalk", true);
                anim.SetBool("isIdle", false);
            }
        }
        else
        {
            if (anim.GetBool("isIdle") == false)
            {
                anim.SetBool("isHit", false);
                anim.SetBool("isIdle", true);
                anim.SetBool("isAttack", false);
                anim.SetBool("isDie", false);
                anim.SetBool("isWalk", false);
            }
        }
    }
}
