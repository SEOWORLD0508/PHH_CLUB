using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : Creature
{
    [Header("Basic Status")]
    
    public double statIncrease = 1.02; // 레벨 당 체력, 공격력 증가량 102%
    public float stamina,maxStamina; // 현재 스테미나 / 최대 스테미나
    public float Vamp,maxVamp; // 뱀파이어 진행도 / 최대 진행도
    // public float weaponDamage; // 무기 공격력 Creature 에서 관리
    // public float damage; // 기본 공격력 Creature 에서 관리
    public float statime; // 스텟 회복 시간
    public float delaytime1,delaytime2; // 선딜 딜레이 / 후딜 딜레이
    public bool attack = true; // 공격 가능 여부
    public bool heal_by_enemy_kill = false; // 적 처치시 체력 회복
    public bool heal_by_enemy_attack = false; // 적 공격시 체력 회복
    public bool immune = false; // 무적 판정
    public float weaponDamageCoeff = 1;

    public bool health_to_damage = false;
    [SerializeField]
    Inventory inventory;

    [SerializeField]
    Judgment Judgment;

    [SerializeField]
    Image staminaBar;

    [SerializeField]
    Image VampBar;

    [SerializeField]
    PlayerMovement playerMovement;
    [SerializeField]
    Item currentWeapon;

    // Start is called before the first frame update
    void Start()
    {
        entity_name = "Player";

        healthBar.fillAmount = 1.0f;
        staminaBar.fillAmount = 1.0f;
        VampBar.fillAmount = 1.0f;
        Vamp = 0;
        maxVamp = 100;
        maxHp = 100;
        maxStamina = 100;
        damage = 20;
        health = maxHp;
        stamina = maxStamina / 2;
        Debug.Log(maxHp + "/" + maxStamina + "/" + health + "/" + stamina);
        
        UpdateStatus(); //스텟 재정의 함수 // 인벤토리 무기 교체시 호출 부탁 드려요~
        StartCoroutine(timerCoroutine());

        
        
    }

    public void UpdateStatus()
    {
        
        RefreshImage();
        if(inventory.weapons.Count == 0)
        {
            item = null;
            weaponDamage = 0;
            attackRange = 5;
        }
        else
        {
            //Item item_ = inventory.weapons[0].item; // 플레이어 아이템 받아옴
           
            //item = item_; // item 은 creature 의 item 속성
            weaponDamage = item.values[2] * weaponDamageCoeff; // 무기 데미지
            attackRange = item.values[3]; // 무기 사거리
            delaytime1 = item.values[5];
            delaytime2 = item.values[6];
        }
          
        maxHp = 100;
        damage = 20; // 기본 데미지
        //maxStamina = 100 + Vamp * 1.5f;
        for (int i = 0; i < Vamp; i++)
        {  // 침식도 비례 데미지 갱신
            maxHp = maxHp * (float)statIncrease; // 최대 체력 약 724
            damage = damage * (float)statIncrease; // 최대 공격력 약 144

        }

        damage += weaponDamage;  // 무기 데미지 + 기본 데미지 * 타락치?

        if (health_to_damage) // 야수의 신장?
        {
            float ratio = health / maxHp;
            damage = damage * (1 + ratio);
        }

    }

    public override void RefreshImage()
    {

        base.RefreshImage();
        staminaBar.fillAmount = (float)stamina / maxStamina;
        VampBar.fillAmount = (float)Vamp / maxVamp;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        dir = playerMovement.moveDir;
        UpdateStatus();
        RefreshImage();

        if (health > maxHp)
        {
            health = maxHp;
        }
        if(Input.GetMouseButtonDown(0) && attack) // 공격키 
        {
            StartCoroutine(Attack());
        }
        if(Vamp >= maxVamp)
        {
            GameManager.Instance.GameOver(); // 최대 침식도 넘었을 떄 게임 오버
        }
    }
    IEnumerator Attack()
    {
        attack = false;
        playerMovement.animator.SetTrigger("Attack");
        yield return new WaitForSeconds(delaytime1); // 선딜
        Debug.Log("Attack");
        Judgment.Attack(this);
        playerMovement.animator.SetTrigger("Hit");


        yield return new WaitForSeconds(delaytime2); //후딜
        attack = true;
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
