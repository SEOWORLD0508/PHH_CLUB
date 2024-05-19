using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Basic Status")]
    public float currentHp,maxHp; // 현재 체력 / 최대 체력
    public double statIncrease = 1.02; // 레벨 당 체력, 공격력 증가량 102%
    public float currentStamina,maxStamina; // 현재 스테미나 / 최대 스테미나
    //public float BasicAttack // 기본 공격력
    public float damage; // 공격력
    public float attackC; // 공격 쿨타임
    public float Vamp; // 뱀파이어 진행도
    public float weaponDamage; // 무기 공격력
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
        Vamp = 0;
        maxHP = 100;
        maxStamina = 100;
        damage = 20;
        currentHp = maxHp;
        currentStamina = maxStamina;
        Debug.Log(maxHp + "/" + maxStamina + "/" + currentHp + "/" + currentStamina);
        Debug.Log(damage + "/" + defense);
        UpdateStatus(); //스텟 재정의 함수
    }

    void UpdateStatus()
    {
        maxHP = 100;
        damage = 20;
        for (int i=0; i<Vamp; i++) {
            maxHP = maxHP * statIncrease; // 최대 체력 약 724
            damage = damage * statIncrease; // 최대 공격력 약 144
        }
        damage += weaponDamage;
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
