using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerMovement : MonoBehaviour
{

    public Animator animator;

    [Header("Movement")]
    float currentSpeed;
    public float maxSpeed;
    public float accel;
    [Space]

    [Header("Dash")]
    public bool dashable = true;
    public KeyCode DashKey;
    public float dashAmount;
    public float dashC; // 재사용시간
    private float currentDashT; // 재사용 시간 계산용 변수
    public bool invincible;
    public float invincibleTime;
    [SerializeField]
    private float reverseOffset;
    [SerializeField]
    Transform lineRenderer;
    

    [SerializeField]
    LayerMask wallLayer;

    [Space]

    public Vector2 moveDir;

    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    PlayerStatus status;

   
    public Transform popUpPos;
    


    // Start is called before the first frame update
    void Start()
    {
        invincible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        float X = Input.GetAxisRaw("Horizontal");
        float Y = Input.GetAxisRaw("Vertical");



        moveDir = new Vector2(X, Y);


        float targetS = maxSpeed;

        if (Mathf.Abs(X) + Mathf.Abs(Y) > 1)
            targetS = targetS / Mathf.Sqrt(2);

        if (currentDashT > 0)
        {
            currentDashT -= Time.deltaTime;
            if (currentDashT < dashC / 2)
                lineRenderer.gameObject.SetActive(false);
        }
        if(currentDashT < dashC - invincibleTime)
        {
            invincible = false;

            animator.SetBool("Dash", false);
        }

        status.immune = invincible;


        if (moveDir != Vector2.zero)
        {
            animator.SetBool("Walk", true);
            if (currentSpeed < targetS)
                currentSpeed += accel * Time.deltaTime;
            else currentSpeed = targetS;
            if (moveDir.x > 0) status.rotateMesh(true); else if (moveDir.x < 0) status.rotateMesh(false);
        }
        else
        {
            currentSpeed = 0;
            animator.SetBool("Walk", false);
        }

        //rb.AddForce(moveDir * currentSpeed);
        if (!status.attack) currentSpeed *= 0.5f;
        transform.Translate(moveDir * currentSpeed * Time.deltaTime);


        if (Input.GetKeyDown(DashKey) && currentDashT <= 0 && dashable && status.stamina - 10 >= 0)
        {

            animator.SetBool("Dash", true);
            lineRenderer.gameObject.SetActive(true);
            currentDashT = dashC;
            invincible = true;
            status.stamina -= 10;
            transform.position = checkWall(dashAmount);

        }
    }
    Vector2 checkWall(float _moveAmount)
    {
        Vector2 cPos = new Vector2(transform.position.x, transform.position.y) - moveDir * reverseOffset;

        Vector2 targetP = cPos + moveDir * _moveAmount;
        Vector2 dir = (cPos - targetP).normalized;


        RaycastHit2D ray = Physics2D.Raycast(cPos, dir, Vector2.Distance(targetP, cPos), wallLayer);
        if (ray)
        {
            targetP = ray.point;
            
        }
        return targetP;

    }
}
