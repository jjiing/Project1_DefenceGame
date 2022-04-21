using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    public float heroSpeed = 1.5f;    //영웅이동 속도
    public Vector3 heroDestPos;      //목적지 위치, 목적지 위치 벡터값

    //HeroAttack heroAttack;
    private Animator animator;
    public bool isHeroMove;
    public Vector3 dirHeroMst; //방향
    //float disToDes;


     void Awake()
    {
        animator=GetComponent<Animator>();
    }
     void Update()
    {
        HeroMoveToDest();
        WarriorWalkAni();
    }

    public void HeroMoveToDest()        //이동
    {
        if (isHeroMove && transform.position != heroDestPos)
        { 
            transform.position = Vector3.MoveTowards(transform.position, heroDestPos, Time.deltaTime * heroSpeed);
            
           
            //if (heroAttack.disToTarget <= 0.7f)
                //isHeroMove = false;
        }
        if (transform.position == heroDestPos)
        {
            isHeroMove = false;
            //Debug.Log("is hero move 값 " + isHeroMove);
        }
    }
    public void WarriorWalkAni()
    {
        if (isHeroMove==true)
            animator.SetBool("isMove", true);
        else
            animator.SetBool("isMove", false);

        dirHeroMst = (heroDestPos - transform.position).normalized;
        //영웅-목적지 방향값(정규화시켜줌)
        animator.SetFloat("inputX", dirHeroMst.x);
        animator.SetFloat("inputY", dirHeroMst.y);    
    }


}
