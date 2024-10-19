using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : Creature
{
    [Header("Basic Status")]
    public float maxHp; // 현재 체력 -> Creature / 최대 체력
    public double statIncrease = 1.02; // 레벨 당 체력, 공격력 증가량 102%
    public float stamina,maxStamina; // 현재 스테미나 / 최대 스테미나
    public float Vamp; // 뱀파이어 진행도
    // public float weaponDamage; // 무기 공격력 Creature 에서 관리
    // public float damage; // 기본 공격력 Creature 에서 관리
    public float statime; // 스텟 회복 시간
    public float delaytime1,delaytime2; // 선딜 딜레이 / 후딜 딜레이
    public bool attack = true; // 공격 가능 여부

    [SerializeField]
    Judgment Judgment;

    [SerializeField]
    Image healthBar, staminaBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.fillAmount = 0.5f;
        staminaBar.fillAmount = 0.5f;
        Vamp = 0;
        maxHp = 100;
        maxStamina = 100;
        damage = 20;
        health = maxHp;
        stamina = maxStamina;
        Debug.Log(maxHp + "/" + maxStamina + "/" + health + "/" + stamina);
        UpdateStatus(); //스텟 재정의 함수 // 인벤토리 무기 교체시 호출 부탁 드려요~
        StartCoroutine(timerCoroutine());
        
    }

    void UpdateStatus()
    {
        healthBar.fillAmount = (float)health / maxHp;
        staminaBar.fillAmount = (float)stamina / maxStamina;
        Item item = FindObjectOfType<Inventory>().weapons[0].item; // 플레이어 아이템 받아옴
        weaponDamage = item.values[2]; // 무기 데미지
        attackRange = item.values[3]; // 무기 사거리  
        maxHp = 100;
        damage = 20; // 기본 데미지
        maxStamina = 100 + Vamp * 1.5f;
        for (int i=0; i<Vamp; i++) {
            maxHp = maxHp * (float)statIncrease; // 최대 체력 약 724
            damage = damage * (float)statIncrease; // 최대 공격력 약 144

        }
        
        damage += weaponDamage;  // 무기 데미지 + 기본 데미지 * 타락치?
    }

    // Update is called once per frame
    void Update()
    {
        
        if (health > maxHp)
        {
            health = maxHp;
        }
        if(Input.GetMouseButtonDown(0) && attack) // 공격키 
        {
            StartCoroutine(Attack());
        }
    }
    IEnumerator Attack()
    {
        attack = true;
        yield return new WaitForSeconds(delaytime1); // 선딜
        Debug.Log("Attack");
        Judgment.Attack(this);



        yield return new WaitForSeconds(delaytime2); //후딜
        attack = false;
    }

    IEnumerator timerCoroutine() 
    {
        while(true)
        {
            stamina += 50f;
            if (stamina > maxStamina)
            {
                stamina = maxStamina;
            }

            yield return new WaitForSeconds(1f);
        }
       
    }
}
