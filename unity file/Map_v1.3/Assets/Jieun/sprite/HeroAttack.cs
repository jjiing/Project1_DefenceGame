using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttack : MonoBehaviour
{
    public HeroMovement heroMoveScript;
    public MouseTransfrom cursor;

    float currentDist = 0;      //i번째 몬스터와 현재거리
    public float disToTarget = 0;      //타겟몬스터까지의 거리(움직이는동안)
    float TargetDist = -1f;     //타겟몬스터와의 거리
    int TargetIndex = -1;       //타겟팅 할 인덱스


    GameObject targetMonster;
    List<GameObject> monstersAround = new List<GameObject>();   //몬스터 리스트
    bool isAttack = false;
    //bool isAttackOnce = false;  //애니메이션용

    private Animator animator;

    private void OnTriggerEnter2D(Collider2D collision) //몬스터 영역안에 들어오면 리스트에 넣기 //땅은 레이어로 충돌 무시설정 해놓음
    {
        if (collision.CompareTag("Monster"))
        {
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
        if (monstersAround.Count != 0 && heroMoveScript.isHeroMove == false)
        {
            if(targetMonster != null)
                GoToTargetandAttack();
        }
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
        heroMoveScript.heroDestPos = targetMonster.transform.position;//목적지를 범위 안 가장 가까운 몬스터로 바꿈
        heroMoveScript.isHeroMove = true;   //움직이는 함수 작동
        disToTarget = Vector2.Distance(transform.position, targetMonster.transform.position); //타겟몬스터와의거리(움직이는 동안)
        if (disToTarget <= 0.5f)    //타겟몬스터에 도착
        {
            //float heroSpeedSave = heroMoveScript.heroSpeed;
            //heroMoveScript.heroSpeed = 0;
            heroMoveScript.isHeroMove = false;  //멈춤
            if(isAttack == false)
                StartCoroutine(AttackCo());
            heroMoveScript.heroDestPos = new Vector3(cursor.cursorPos.x, cursor.cursorPos.y, 0); //목적지 원래대로 바꿔주기
            //heroMoveScript.heroSpeed = heroSpeedSave;
        }
    }
    IEnumerator AttackCo()
    {
        isAttack = true;

        while (targetMonster != null)
        {
            Debug.Log("공격 1회");
            //isAttackOnce = true;
            //WarriorAttackAni();
            targetMonster.GetComponent<Monster>().monterHP -= 40;
            //isAttackOnce = false;


            if (targetMonster.GetComponent<Monster>().monterHP <= 0)
                Debug.Log(targetMonster.name + "죽음");

            yield return new WaitForSeconds(1);
        }
        isAttack = false;
    }

    void WarriorAttackAni()
    {       
        if(isAttack == true)
            animator.SetBool("isAttack", true);
        else
            animator.SetBool("isAttack", false);

        Vector3 dir = heroMoveScript.dirHeroMst;
        //영웅-목적지 방향값(정규화시켜줌)
        animator.SetFloat("inputX", dir.x);
        animator.SetFloat("inputY", dir.y);
    }
}


