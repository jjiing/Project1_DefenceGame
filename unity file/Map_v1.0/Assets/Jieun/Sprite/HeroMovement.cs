using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    public float moveSpeed=1f;
    Vector3 heroDestination;
    Vector2 dirHeroDestination;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float inputMousePointerX = Input.GetAxisRaw("Horizontal");  //A,D 또는 좌우 방향키 입력받음
        //float inputMousePointerY = Input.GetAxisRaw("Vertical");    //W,S 또는 위아래 방향키 입력받음
        //transform.Translate(new Vector2(inputMousePointerX, inputMousePointerY)*Time.deltaTime*moveSpeed);
    
        if(Input.GetMouseButton(1))
        {
            heroDestination = Camera.main.ScreenToWorldPoint(Input.mousePosition);    //world좌표로 입력, 마우스 위치 가져오기

        }
        dirHeroDestination = heroDestination - transform.position;

        //if(dirHeroDestination!=Vector2.zero) : 애니메이션 설정용

        transform.position = Vector2.MoveTowards(transform.position, heroDestination, Time.deltaTime * moveSpeed);  

    }
}
