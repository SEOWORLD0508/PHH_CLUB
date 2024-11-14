using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerMovement : MonoBehaviour
{


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
    


    // Start is called before the first frame update
    void Start()
    {
    
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



        if (moveDir != Vector2.zero)
        {
            if (currentSpeed < targetS)
                currentSpeed += accel * Time.deltaTime;
            else currentSpeed = targetS;
        }
        else
            currentSpeed = 0;

        //rb.AddForce(moveDir * currentSpeed);
       
        transform.Translate(moveDir * currentSpeed);


        if (Input.GetKeyDown(DashKey) && currentDashT <= 0 && dashable)
        {

            
            lineRenderer.gameObject.SetActive(true);
            currentDashT = dashC;
            //status.currentStamina -= 10;
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
