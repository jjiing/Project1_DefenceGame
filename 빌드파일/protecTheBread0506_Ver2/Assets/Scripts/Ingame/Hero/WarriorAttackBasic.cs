using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAttackBasic : MonoBehaviour
{
    public Warrior war;
    public HeroMovement heroMoveScript;
    private Animator animator;
    
    float targetDist;   //Ÿ�� ���Ϳ��� �Ÿ�
    
    GameObject targetMonster;
    List<GameObject> monstersAround = new List<GameObject>();   //���� ����Ʈ
    
    bool isAttack = false;
    int resultDamage;       //�������� ������ �߰��� ��


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //�⺻����
        FindTargetMonster();
        GoToTargetandAttack();
        WarriorAttackAni();
        

        if (Input.GetMouseButtonDown(1)|| targetDist > 0.5f)        //���� �� ���߱�
        {
            isAttack = false;
            StopAllCoroutines();
            heroMoveScript.heroSpeed = 1.5f;    //�ӵ� �ʱ�ȭ
        }
         

    }
   

    private void OnTriggerEnter2D(Collider2D collision) //���� �����ȿ� ������ ����Ʈ�� �ֱ� 
    {
        if (collision.CompareTag("Monster"))
            monstersAround.Add(collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)  //���Ͱ� �������� ������ ����Ʈ���� ����
    {
        if (collision.CompareTag("Monster"))
            monstersAround.Remove(collision.gameObject);
    }

    void FindTargetMonster()
    {
        if (monstersAround.Count != 0 )
        {
            float TargetDistCheck = -1f;            //Ÿ�ٸ��Ϳ��� �Ÿ� - �Ÿ� ������
            int TargetIndex = -1;                   //Ÿ���� �� �ε���

            for (int i = 0; i < monstersAround.Count; i++)
            {
                float currentDist = Vector2.Distance(transform.position, monstersAround[i].transform.position);     //i��° ���Ϳ��� �Ÿ�

                if (TargetDistCheck == -1)               //ù ���͸� Ÿ������ ���ϰ� Ÿ�� �Ÿ��� ����
                {
                    TargetDistCheck = currentDist;
                    TargetIndex = i;
                }
                else
                {
                    if (TargetDistCheck >= currentDist)  //Ÿ�ٰŸ� ���ؼ� �� �������� �ε����� ����
                    {
                        TargetIndex = i;
                        TargetDistCheck = currentDist;
                    }
                }
                targetMonster = monstersAround[TargetIndex];
            }
            TargetDistCheck = -1f;  //�ʱ�ȭ   
        }
    }
    void GoToTargetandAttack()
    {
        if (monstersAround.Count != 0 && !heroMoveScript.isHeroCursorMove)
        {
            heroMoveScript.isHeroMove = true;

            heroMoveScript.heroMoveDir = (targetMonster.transform.position - transform.position).normalized;
            transform.position += heroMoveScript.heroMoveDir * heroMoveScript.heroSpeed * Time.deltaTime;

            targetDist = Vector2.Distance(transform.position, targetMonster.transform.position); //Ÿ�ٸ��Ϳ��ǰŸ�(�����̴� ����)
            if (targetDist <= 0.5f * heroMoveScript.heroSpeed)
            {
                heroMoveScript.isHeroMove = false;
                heroMoveScript.heroSpeed = 0;
                if (isAttack == false)
                    StartCoroutine(HeroAttackCo());
            }
        }
    }
    
    IEnumerator HeroAttackCo()
    {
        animator.SetTrigger("Attack");

        while (targetMonster != null && targetDist <= 0.5f)
        {
            isAttack = true;

            resultDamage = Random.Range(GameManager.instance.Atk - 4, GameManager.instance.Atk + 5);
            targetMonster.GetComponent<Enemy>().nowHp -= resultDamage;    //Atk
            
            if (targetMonster.GetComponent<Enemy>().nowHp <= 0)
            {
                monstersAround.Remove(targetMonster);
                isAttack = false;
                heroMoveScript.isHeroMove = false;
            }

            yield return new WaitForSeconds(1f);
        }
        isAttack = false;
        heroMoveScript.heroSpeed = 1.5f;    //�ӵ� �ʱ�ȭ
    }

    void WarriorAttackAni()
    {
        if (isAttack == true)
        {
            animator.SetBool("isAttack", true);
            if (targetMonster != null)
            {
                heroMoveScript.heroMoveDir = (targetMonster.transform.position - transform.position).normalized;
                //����-������ ���Ⱚ(����ȭ������)
                animator.SetFloat("inputX", heroMoveScript.heroMoveDir.x);
                animator.SetFloat("inputY", heroMoveScript.heroMoveDir.y);
            }
        }
        else
            animator.SetBool("isAttack", false);        
    }



 


}

