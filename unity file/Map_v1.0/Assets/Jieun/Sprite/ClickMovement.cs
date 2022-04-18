using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMovement : MonoBehaviour
{
    private Camera camera;
    private Animator animator;

    private bool isMove;            //캐릭터가 움직이고 있는지 판정할 변수
    private Vector3 destination;    //목적지 변수

    private void Awake()
    {
        camera = Camera.main;
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1))     //마우스 우클릭 위치를 목적지로 설정
        {
            RaycastHit hit;
            if(Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                SetDestination(hit.point);
            }
        }
        Move();
    }
    private void SetDestination(Vector3 dest)   //목적지 설정 함수
    {
        destination = dest;
        isMove = true;
        animator.SetBool("isMove",true);        //애니메이터 안에있는 (걷는)애니메이션 동작
    }
    private void Move()     //isMove=T일때 목적지 방향으로 이동시키기
    {
        if(isMove)
        {
            var dir = destination- transform.position;
            transform.position += dir.normalized*Time.deltaTime * 5f ;
            //dir.normalized : 클릭한 마우스 위치와 캐릭터 위치 사이의 거리가 멀수록 이동속도가 빨라질 수 있으니
            //방향 값을 normarlized 시켜줘 1로 만들어줌
            //Time.deltaTime : 게임의 프레임 속도에 영향을 받지 않도록 해줌
            // 5f : 이동속도값
        }

        if(Vector3.Distance(transform.position,destination)<=0.1f)
        {
            isMove = false;
            animator.SetBool("isMove", false);
        }
    }
}
