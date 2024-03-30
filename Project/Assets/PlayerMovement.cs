using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Basic Movement")]
    public float maxSpeed; // 이동속도로 봐도 될듯 
    public float accel; // 가속량
  

    float currentSpeed;

    [Space]


    [Header("Dash")]
    public float dashAmount;
    public float dashC; // 재사용시간
    private float currentDashT; // 재사용 시간 계산용 변수


    [Space]

    private Vector2 moveDir;

    [SerializeField]
    Rigidbody2D rb;
   
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float X = Input.GetAxisRaw("Horizontal");
        float Y = Input.GetAxisRaw("Vertical");


        print(X);
        moveDir = new Vector2(X, Y);


        float targetS = maxSpeed;

        if (Mathf.Abs(X) + Mathf.Abs(Y) > 1)
            targetS = targetS / 4;



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
        

    }
}
