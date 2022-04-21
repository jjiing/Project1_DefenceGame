using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttack : MonoBehaviour
{
    public HeroMovement heroMoveScript;
    float currentDist = 0;      //i번째 몬스터와 현재거리
    float TargetDist = -1f;     //타겟몬스터와의 거리
    int TargetIndex = -1;       //타겟팅 할 인덱스

    GameObject targetMonster;
    List<GameObject> monstersAround = new List<GameObject>();   //몬스터 리스트

    bool isAttack = false;
    float distance;


    private Animator animator;

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



    private void Start()
    {
        animator = GetComponent<Animator>();
    }



    void Update()
    {
        FindTargetMonster();
        GoToTargetandAttack();
        WarriorAttackAni();
    }





    void FindTargetMonster()
    {
        if (monstersAround.Count != 0)
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
        if (monstersAround.Count != 0 && heroMoveScript.isHeroBasicMove==false)
        {
            Debug.Log("if문 시작");
            if (targetMonster != null)
            {
                distance = Vector2.Distance(transform.position, targetMonster.transform.position); //타겟몬스터와의거리(움직이는 동안)

                Vector3 direction = (targetMonster.transform.position - transform.position).normalized;  
                transform.position += direction * heroMoveScript.heroSpeed * Time.deltaTime;
                heroMoveScript.isHeroMove = true;


                if (distance <= 0.5f * heroMoveScript.heroSpeed)
                {
                    Debug.Log("가까이와따");
                    heroMoveScript.heroSpeed = 0;       
                    
                    
                    if (isAttack == false)
                        StartCoroutine(HeroAttackCo());
                }
                
            }
        }
    }
        IEnumerator HeroAttackCo()
        {
        
            isAttack = true;
            heroMoveScript.isHeroMove = false; //얘 없으면 걷기 모션이 안꺼짐
            Debug.Log("공격시작");

            while (targetMonster != null && distance <= 0.5f )
            {
                
                Debug.Log("공격 1회");            
                targetMonster.GetComponent<Monster>().monterHP -= 40;
                if (targetMonster.GetComponent<Monster>().monterHP <= 0)
                {
                    Debug.Log(targetMonster.name + "죽음");
                    isAttack = false;
                    heroMoveScript.isHeroMove = false;
                }
                yield return new WaitForSeconds(1);
            }
            isAttack = false;
        
        heroMoveScript.heroSpeed = 1.5f;    //속도 초기화
           }

        void WarriorAttackAni()
        {
            if (isAttack == true)
                animator.SetBool("isAttack", true);
            else
                animator.SetBool("isAttack", false);

            if (targetMonster != null)
            {
                Vector3 dir = (targetMonster.transform.position - transform.position).normalized;
                    //영웅-목적지 방향값(정규화시켜줌)
                animator.SetFloat("inputX", dir.x);
                animator.SetFloat("inputY", dir.y);
            }
        }
    
}

