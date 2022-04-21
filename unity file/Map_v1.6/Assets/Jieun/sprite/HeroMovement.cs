using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    public float heroSpeed = 1.5f;    //영웅이동 속도
    public Vector3 dirHeroMst; //방향

    public MouseTransfrom cursor;
    private Animator animator;

    public bool isHeroMove;
    public bool isHeroBasicMove;

    
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
        if (isHeroMove && isHeroBasicMove)
        {
            Vector3 direction = (cursor.cursorFix - transform.position).normalized;
            transform.position += (direction * heroSpeed * Time.deltaTime);
            float distance = Vector2.Distance(cursor.cursorFix , transform.position);
            if (distance <= 0.01f)
            {
                isHeroMove = false;
                isHeroBasicMove = false;
            }
        }
       
    }
   
    public void WarriorWalkAni()
    {
        if (isHeroMove == true)
            animator.SetBool("isMove", true);
        else 
            animator.SetBool("isMove", false);

       dirHeroMst = (cursor.cursorFix - transform.position).normalized;
        //영웅-목적지 방향값(정규화시켜줌)
        animator.SetFloat("inputX", dirHeroMst.x);
        animator.SetFloat("inputY", dirHeroMst.y);    
    }


}
