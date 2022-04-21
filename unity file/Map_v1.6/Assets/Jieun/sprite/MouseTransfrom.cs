using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTransfrom : MonoBehaviour
{
    public HeroMovement heroMoveScript;
    public Vector3 cursorInput, cursorFix;

    private void Start()
    {
        //heroMoveScript.heroDestPos = transform.position;      //목적지 벡터값 초기화(안해주면 시작과 동시에 첫 위치로 움직임)
    }

    void Update()
    {
        SetHeroDestPos();
    }

    void SetHeroDestPos()   //목적지 설정 함수
    {
        cursorInput = Camera.main.ScreenToWorldPoint(Input.mousePosition); //계속 바뀌는 마우스값

        //world좌표로 입력, 마우스 커서 위치
        
        //if (Input.GetMouseButtonDown(1))
        //heroMoveScript.heroDestPos = new Vector3(cursorInput.x, cursorInput.y, 0);
        //목적지 위치에 커서 위치 넣기
        if (Input.GetMouseButtonDown(1))
        {
            cursorFix = new Vector3(cursorInput.x, cursorInput.y, 0);
        }
        transform.position = cursorFix;


    }


    private void OnTriggerStay2D(Collider2D collision)      //태그, 트리거 설정으로 주황색일때만 움직이게(isHeroMove=true;)
    {
        if (collision.tag == "Clickable")
        {
            if (Input.GetMouseButton(1))
            {
                heroMoveScript.isHeroMove = true;
                heroMoveScript.isHeroBasicMove=true;
            }
        }
    }
}

