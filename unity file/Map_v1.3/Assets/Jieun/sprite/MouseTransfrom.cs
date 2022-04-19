using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTransfrom : MonoBehaviour
{
    public HeroMovement hero; 
    float heroSpeed = 1.5f;    //영웅이동 속도
    Vector3 cursorPos;

    private void Start()
    {
        hero.heroDestPos = transform.position;      //목적지 벡터값 초기화(안해주면 시작과 동시에 첫 위치로 움직임)
    }

    void Update()
    {
        SetHeroDestPos();
    }

    void SetHeroDestPos()   //목적지 설정 함수
    {
        cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //world좌표로 입력, 마우스 커서 위치

        if (Input.GetMouseButtonDown(1))
            hero.heroDestPos = new Vector3(cursorPos.x, cursorPos.y, 0);
        //목적지 위치에 커서 위치 넣기

        transform.position = hero.heroDestPos;      
    }


    private void OnTriggerStay2D(Collider2D collision)      //태그, 트리거 설정으로 주황색일때만 움직이게(isHeroMove=true;)
    {
        if(collision.tag == "Clickable")
        {
            if (Input.GetMouseButton(1))
                 hero.isHeroMove = true; 
        }
    }
}
