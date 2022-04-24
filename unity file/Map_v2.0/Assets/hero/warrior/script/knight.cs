using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knight : MonoBehaviour
{
    private Animator animator;
    GameObject targetMonster;
    List<GameObject> monstersAround = new List<GameObject>();   //몬스터 리스트

    float targetDist;   //타겟 몬스터와의 거리

    float knightSpeed = 1f;
    bool isAttack = false;
    //bool isKnightMove = false;
    Vector3 targetKnightDirection;

    //atk
    public int knightAtk = 10;
    //hp
    public int knightHPnow = 50;
    public int knightHPmax = 100;
    //버프시 증가량
    public float buffPercent= 0.2f; 


    private void Start()
    {
        animator = GetComponent<Animator>();
        Invoke("DestroyKnight", 10f);
    }

    void Update()
    {
        //기본공격
        FindTargetMonster();
        GoToTargetandAttack();
        WarriorAttackAni();


        if (knightHPnow <= 0)
            DestroyKnight();

    }

    public int KnightHP
    {
        get { return knightHPnow; }

        set
        {
           if (value < 0)
                value = 0;

            knightHPnow = value;
        }
    }
    public int KnightAtk
    {
        get { return knightAtk; }

        set
        {
            if (value > knightHPmax)
                value = knightHPmax;
            else if (value < 0)
                value = 0;

            knightAtk = value;
        }
    }
    private void DestroyKnight()
    {
        ObjectPool.ReturnObject(this);
    }
    


    private void OnTriggerEnter2D(Collider2D collision) //몬스터 영역안에 들어오면 리스트에 넣기 
    {
        if (collision.CompareTag("Monster"))
        {
            monstersAround.Add(collision.gameObject);
            Debug.Log("충돌감지");
            Debug.Log(monstersAround.Count);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)  //몬스터가 영역에서 나가면 리스트에서 제거
    {
        if (collision.CompareTag("Monster"))
        {
            monstersAround.Remove(collision.gameObject);
            Debug.Log("충돌사라짐");
            Debug.Log(monstersAround.Count);

        }
    }

    void FindTargetMonster()
    {
        if (monstersAround.Count != 0)
        {
            float TargetDistCheck = -1f;            //타겟몬스터와의 거리 - 거리 감지용
            int TargetIndex = -1;                   //타겟팅 할 인덱스

            for (int i = 0; i < monstersAround.Count; i++)
            {
                float currentDist = Vector2.Distance(transform.position, monstersAround[i].transform.position);     //i번째 몬스터와의 거리

                if (TargetDistCheck == -1)               //첫 몬스터를 타겟으로 정하고 타겟 거리도 저장
                {
                    TargetDistCheck = currentDist;
                    TargetIndex = i;
                }
                else
                {
                    if (TargetDistCheck >= currentDist)  //타겟거리 비교해서 더 가까운놈을 인덱스로 저장
                    {
                        TargetIndex = i;
                        TargetDistCheck = currentDist;
                    }
                }
                targetMonster = monstersAround[TargetIndex];
            }
            TargetDistCheck = -1f;  //초기화   
        }
    }
    void GoToTargetandAttack()
    {
        if (monstersAround.Count != 0)
        {
            //isKnightMove = true;

            targetKnightDirection = (targetMonster.transform.position - transform.position).normalized;
            transform.position += targetKnightDirection * knightSpeed * Time.deltaTime;

            targetDist = Vector2.Distance(transform.position, targetMonster.transform.position); //타겟몬스터와의거리(움직이는 동안)
            if (targetDist <= 0.5f * knightSpeed)
            {
                //isKnightMove = false;
                knightSpeed = 0;
                if (isAttack == false)
                    StartCoroutine(KnightAttackCo());
            }

        }
    }

    IEnumerator KnightAttackCo()
    {
        //animator.SetTrigger("Attack");
        Debug.Log("공격시작");

        while (targetMonster != null && targetDist <= 0.5f)
        {
            isAttack = true;
            Debug.Log("공격 1회");
            //animator.SetTrigger("Attack");
            targetMonster.GetComponent<Monster>().monterHP -= knightAtk;
            Debug.Log("몬스터 체력" + targetMonster.GetComponent<Monster>().monterHP);

            if (targetMonster.GetComponent<Monster>().monterHP <= 0)
            {
                monstersAround.Remove(targetMonster);
                Debug.Log(targetMonster.name + "죽음");
                isAttack = false;
                //isKnightMove = false;
            }
            yield return new WaitForSeconds(1f);
        }
        isAttack = false;
        knightSpeed = 1.5f;    //속도 초기화
    }

    IEnumerator DestoryCo()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }


    void WarriorAttackAni()
    {
        if (isAttack == true)
        {
            animator.SetBool("isAttack", true);
            if (targetMonster != null)
            {
                Vector3 Dir = (targetMonster.transform.position - transform.position).normalized;
                //영웅-목적지 방향값(정규화시켜줌)
                animator.SetFloat("inputX", Dir.x);
            }
        }
        else
            animator.SetBool("isAttack", false);
    }
}



