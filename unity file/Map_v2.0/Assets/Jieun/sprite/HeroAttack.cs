using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttack : Warrior
{
    public HeroMovement heroMoveScript;
    
    
    float currentDist = 0;      //i번째 몬스터와 현재거리
    float TargetDist = -1f;     //타겟몬스터와의 거리
    int TargetIndex = -1;       //타겟팅 할 인덱스

 
    GameObject targetMonster;
    List<GameObject> monstersAround = new List<GameObject>();   //몬스터 리스트
    
    float distance;

    private Animator animator;

    bool isCool = true;

    bool isAttack = false;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //기본공격
        FindTargetMonster();
        GoToTargetandAttack();
        WarriorAttackAni();

        if (Input.GetMouseButtonDown(1))
        {
            isAttack = false;
            StopAllCoroutines();
            
            heroMoveScript.heroSpeed = 1.5f;    //속도 초기화
        }
         


        //스킬
        WarriorSkill1();
        if(isCool)
        {
            WarriorSkill2();
            StartCoroutine(CoolTimeCo(coolTime2));
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (isCool)
            {
                WarriorSkill3();
                StartCoroutine(CoolTimeCo(coolTime3));
            }

        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isCool)
            {
                WarriorSkill4();
                StartCoroutine(CoolTimeCo(coolTime4));
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) //몬스터 영역안에 들어오면 리스트에 넣기 //땅은 레이어로 충돌 무시설정 해놓음
    {
        if (collision.CompareTag("Monster"))
        {
            Debug.Log("충돌");
            monstersAround.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)  //몬스터가 영역에서 나가면 리스트에서 제거
    {
        if (collision.CompareTag("Monster"))
        {
            monstersAround.Remove(collision.gameObject);
        }
    }


    void FindTargetMonster()
    {
        if (monstersAround.Count != 0 )
        {
            for (int i = 0; i < monstersAround.Count; i++)
            {
                currentDist = Vector2.Distance(transform.position, monstersAround[i].transform.position);
                if (TargetDist == -1)               //첫 몬스터를 타겟으로 정하고 타겟 거리도 저장
                {
                    TargetDist = currentDist;
                    TargetIndex = i;
                }
                else
                {
                    if (TargetDist >= currentDist)  //타겟거리 비교해서 더 가까운놈을 인덱스로 저장
                    {
                        TargetIndex = i;
                        TargetDist = currentDist;
                    }
                }
                targetMonster = monstersAround[TargetIndex];
            }
            TargetDist = -1f;  //초기화   
        }
    }
    void GoToTargetandAttack()
    {

        if (monstersAround.Count != 0 && !heroMoveScript.isHeroCursorMove)
        {
            heroMoveScript.isHeroMove = true;


            heroMoveScript.heroMoveDir = (targetMonster.transform.position - transform.position).normalized;
            transform.position += heroMoveScript.heroMoveDir * heroMoveScript.heroSpeed * Time.deltaTime;

            distance = Vector2.Distance(transform.position, targetMonster.transform.position); //타겟몬스터와의거리(움직이는 동안)
            if (distance <= 0.5f * heroMoveScript.heroSpeed)
            {
                heroMoveScript.isHeroMove = false;
                Debug.Log("가까이와따");
                heroMoveScript.heroSpeed = 0;
                if (isAttack == false)
                    StartCoroutine(HeroAttackCo());
            }

        }
    }
    IEnumerator HeroAttackCo()
    {

        animator.SetTrigger("Attack");
        heroMoveScript.isHeroMove = false;
        Debug.Log("공격시작");

        while (targetMonster != null && distance <= 0.5f)
        {
            isAttack = true;
            Debug.Log("공격 1회");
            //animator.SetTrigger("Attack");
            targetMonster.GetComponent<Monster>().monterHP -= warriorAtk;
            Debug.Log("몬스터 체력" + targetMonster.GetComponent<Monster>().monterHP);
            
            if (targetMonster.GetComponent<Monster>().monterHP <= 0)
            {
                monstersAround.Remove(targetMonster);
                Debug.Log(targetMonster.name + "죽음");
                isAttack = false;
                heroMoveScript.isHeroMove = false;
            }

            if (monstersAround.Count<=0)
                break;

         
            yield return new WaitForSeconds(1f);
            
        }
        isAttack = false;
        heroMoveScript.heroSpeed = 1.5f;    //속도 초기화
    }

    void WarriorAttackAni()
    {
        if (isAttack == true)
        {
            animator.SetBool("isAttack", true);
            

            if (targetMonster != null)
            {
                heroMoveScript.heroMoveDir = (targetMonster.transform.position - transform.position).normalized;

                //영웅-목적지 방향값(정규화시켜줌)
                animator.SetFloat("inputX", heroMoveScript.heroMoveDir.x);
                animator.SetFloat("inputY", heroMoveScript.heroMoveDir.y);
            }
        }
        else
            animator.SetBool("isAttack", false);        

    }




   



    IEnumerator CoolTimeCo(float cooltime)
    {
        isCool = false;
        yield return new WaitForSeconds(cooltime);
        isCool = true;
    }
    void WarriorSkill1()
    {
        //주위 병사들 공격력, 체력 높이기 (쿨 없음. 감지하면 사용)
    }
    void WarriorSkill2()
    {
        //(쿨 돌때마다 자동사용)
        double warriorHealPer = warriorHPup * 0.01;
        int plusHp = (int)(warriorHPmax*warriorHealPer);
        warriorHPnow += plusHp;
        if (warriorHPnow > warriorHPmax)
            warriorHPnow = warriorHPmax;
       
    }
    void WarriorSkill3()
    {
        //주위 병사들 소환
    }
    void WarriorSkill4()
    {
        //궁극기 - 광역딜
    }


}

