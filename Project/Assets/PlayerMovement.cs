using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Basic Status")]
    public float CurrentHp,MaxHp; // 현재 체력 / 최대 체력
    public float CurrentStamina,MaxStamina; // 현재 스테미나 / 최대 스테미나
    public float BasicAttack,BasicDefense; // 기본 공격력 / 기본 방어력
    public float Attack,Defense; // 공격력 / 방어력
    public float AttackC; // 공격 쿨타임
    public float Vamp; // 뱀파이어 진행도
    [Header("Basic Movement")]
    public float maxSpeed; // 이동속도로 봐도 될듯 
    public float accel; // 가속량
  

    float currentSpeed;

    [Space]


    [Header("Dash")]
    public KeyCode DashKey;
    public float dashAmount;
    public float dashC; // 재사용시간
    private float currentDashT; // 재사용 시간 계산용 변수
    [SerializeField]
    Transform lineRenderer;


    [Space]

    private Vector2 moveDir;

    [SerializeField]
    Rigidbody2D rb;
   
    


    // Start is called before the first frame update
    void Start()
    {
        Vamp=0;
        CurrrentAttack = (vamp + 1)*10;
        CurrrentDefense = (vamp + 1)*10;
        CurrentHp = MaxHp;
        CurrentStamina = MaxStamina;
        Debug.Log(MaxHp + "/" + MaxStamina + "/" + CurrentHp + "/" + CurrentStamina);
        Debug.Log(CurrrentAttack + "/" + CurrrentDefense );
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
            if(currentDashT < dashC/2)
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


        if (Input.GetKeyDown(DashKey) && currentDashT <= 0)
        {
            lineRenderer.gameObject.SetActive(true);
            currentDashT = dashC;

            transform.Translate(moveDir * dashAmount);

        }
        


    }
}
