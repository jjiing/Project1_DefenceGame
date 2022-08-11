using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    public float heroSpeed = 1.5f;    //���� �̵��ӵ�

    private Animator animator;
    

    private CircleCollider2D heroCollider;
    
                              
    public Vector3 cursorInput, cursorFix;
    private Camera mainCamera;

    private Ray ray;
    private RaycastHit hit;

    public bool isHeroMove;
    public bool isHeroCursorMove;

    public Vector3 heroMoveDir;



    void Awake()
    {
        heroCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        CursorPoint();
        HeroMoveToCursor();
        WarriorWalkAni(); 
    }
    void CursorPoint()      //���콺 ����Ʈ �Է�
    {
        if (Input.GetMouseButtonDown(1))
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.CompareTag("Clickable"))
                {
                    isHeroMove = true;
                    isHeroCursorMove = true;
                    heroCollider.enabled = false;
                    SetCursorPos();         
                }
            }

        }
    }
    void SetCursorPos()   //������ ���� �Լ�
    {
        cursorInput = Camera.main.ScreenToWorldPoint(Input.mousePosition); //��� �ٲ�� ���콺��
        cursorFix = new Vector3(cursorInput.x, cursorInput.y, 0);
        heroMoveDir = (cursorFix - transform.position).normalized;
    }
    public void HeroMoveToCursor()        //��Ŭ���ڸ��� �̵�
    {
        if (isHeroMove && isHeroCursorMove)
        {

            heroMoveDir = (cursorFix - transform.position).normalized;
            
            transform.position += (heroMoveDir * heroSpeed * Time.deltaTime);

            float distance = Vector2.Distance(cursorFix , transform.position);
            if (distance <= 0.01f)
            {
                heroCollider.enabled = true;
                isHeroMove = false;
                isHeroCursorMove = false;
            }
        }
       
    }

    public void WarriorWalkAni()       
    {
        if (isHeroMove == true)
            animator.SetBool("isMove", true);

        else
            animator.SetBool("isMove", false);
  
            //����-������ ���Ⱚ(����ȭ������)
        animator.SetFloat("inputX", heroMoveDir.x);
        animator.SetFloat("inputY", heroMoveDir.y);
        
    }


}
