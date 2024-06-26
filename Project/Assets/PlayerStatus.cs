using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("Basic Status")]
    public float currentHp,maxHp; // 현재 체력 / 최대 체력
    public double statIncrease = 1.02; // 레벨 당 체력, 공격력 증가량 102%
    public float currentStamina,maxStamina; // 현재 스테미나 / 최대 스테미나
    public float Vamp; // 뱀파이어 진행도
    public float weaponDamage; // 무기 공격력
    public float damage; // 기본 공격력
    public float statime; // 스텟 회복 시간
    public float delaytime1,delaytime2; // 선딜 딜레이 / 후딜 딜레이
    public bool attack = true; // 공격 가능 여부

    // Start is called before the first frame update
    void Start()
    {
        Vamp = 0;
        maxHp = 100;
        maxStamina = 100;
        damage = 20;
        currentHp = maxHp;
        currentStamina = maxStamina;
        Debug.Log(maxHp + "/" + maxStamina + "/" + currentHp + "/" + currentStamina);
        UpdateStatus(); //스텟 재정의 함수
    }

    void UpdateStatus()
    {
        maxHp = 100;
        damage = 20;
        maxStamina = 100 + Vamp * 1.5f;
        for (int i=0; i<Vamp; i++) {
            maxHp = maxHp * (float)statIncrease; // 최대 체력 약 724
            damage = damage * (float)statIncrease; // 최대 공격력 약 144
        }
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(timerCoroutine());
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
        if(Input.GetKeyDown(KeyCode.A) && attack)
        {
            StartCoroutine(Attack());
        }
    }
    IEnumerator Attack()
    {
        attack = false;
        yield return new WaitForSeconds(delaytime1);
        Debug.Log("Attack");




        yield return new WaitForSeconds(delaytime2);
        attack = true;
    }

    IEnumerator timerCoroutine() 
    {
        while(true)
        {
            currentStamina += 50f;
            if (currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }

            yield return new WaitForSeconds(1f);
        }
       
    }
}
