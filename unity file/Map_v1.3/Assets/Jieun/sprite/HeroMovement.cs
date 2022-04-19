using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    float heroSpeed = 1.5f;    //영웅이동 속도
    public Vector3 heroDestPos;      //목적지 위치, 목적지 위치 벡터값

    private Animator animator;
    public bool isHeroMove;

     void Awake()
    {
        animator=GetComponent<Animator>();
    }
     void Update()
    {
        HeroMoveToDest();
    }

    public void HeroMoveToDest()        //이동
    {
        if (isHeroMove)
        {
          transform.position = Vector3.MoveTowards(transform.position, heroDestPos, Time.deltaTime * heroSpeed);
            animator.SetBool("isMove", true);
            HeroMoveAni();
        }
        if(Vector3.Distance(transform.position, heroDestPos)<=0.1f)
        {
            isHeroMove=false;
            animator.SetBool("isMove", false);
        }
    }
    public void HeroMoveAni()
    {
        Vector3 dir = (heroDestPos - transform.position).normalized; 
        //영웅-목적지 방향값(정규화시켜줌)
        Debug.Log(dir.x + " " + dir.y);

        if (dir.x > 0.5f)
            dir.x = 1;
        if (dir.x < -0.5f)
            dir.x = -1;
        if (dir.y > 0.5f)
            dir.y = 1;
        if (dir.y < -0.5f)
            dir.y = -1;
        Debug.Log(dir.x + " " + dir.y);

        dir.x = (int)dir.x;
        dir.y = (int)dir.y;

       

        //if (dir.x <= 1&& dir.x>=-1 && dir.y<0)
        //{
        animator.SetFloat("inputX", dir.x);
        animator.SetFloat("inputY", dir.y);
        //}
        /*else if (dir.x <= 1 && dir.x >= -1 && dir.y > 0)
        {
            animator.SetFloat("inputX", 0);
            animator.SetFloat("inputY", 1);
        }
        else if (dir.y <= 1 && dir.y >= -1 && dir.x > 0)
        {
            animator.SetFloat("inputX", -1);
            animator.SetFloat("inputY", 0);
        }
        else if (dir.y <= 1 && dir.y >= -1 && dir.x < 0)
        {
            animator.SetFloat("inputX", 0);
            animator.SetFloat("inputY", -1);
        }*/
    }


}
