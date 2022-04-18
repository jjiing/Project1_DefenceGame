using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    float heroSpeed = 1.5f;    //영웅이동 속도
    Vector3 heroDest, heroDestPos;      //목적지 위치, 목적지 위치 벡터값

    private Animator animator;
    private bool isHeroMove;

     void Awake()
    {
        animator=GetComponent<Animator>();
    }
    private void Start()
    {
        heroDestPos = transform.position;      //목적지 벡터값 초기화(안해주면 시작과 동시에 첫 위치로 움직임)
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
            SetHeroDestPos();

        HeroMoveToDest();
    }

    void SetHeroDestPos()   //목적지 설정 함수
    {
        heroDest = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //world좌표로 입력, 마우스클릭 위치 가져오기(목적지)
        heroDestPos = new Vector3(heroDest.x, heroDest.y, 0);
        //벡터값에 담아주기
        isHeroMove = true;
        animator.SetBool("isMove", true);

    }
    void HeroMoveToDest()
    {
        if (isHeroMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, heroDestPos, Time.deltaTime * heroSpeed);
        }
        if(Vector3.Distance(transform.position, heroDestPos)<=0.1f)
        {
            isHeroMove=false;
            animator.SetBool("isMove", false);
        }
    }



}
