using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrackKnightSpawn : MonoBehaviour
{
    private Camera mainCamera;
    private bool isKnightMoveable = false;

    private SpriteRenderer sprite;
    private CapsuleCollider2D capsuleCollider;
    private Tower myTower;                        // 타워의 정보를 저장
    private knight[] knight = new knight[3];

    public knight this[int index]
    {
        get { return knight[index]; }
        set { knight[index] = value; }
    }

    private void Awake()
    {
        mainCamera = Camera.main;
        sprite = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        myTower = transform.parent.GetComponent<Tower>();   // 부모옵젝인 타워의 정보를 변수에 저장
    }
    

    private void Start()
    {
        knight[0] = ObjectPool.GetObject();
        knight[0].transform.position = myTower.transform.position + new Vector3(0.7f, -0.7f, 0);

        //knight[1] = ObjectPool.GetObject();
        //knight[1].transform.position = myTower.transform.position + new Vector3(-0.7f, -0.7f, 0);

        //knight[2] = ObjectPool.GetObject();
        //knight[2].transform.position = myTower.transform.position + new Vector3(0.0f, -0.7f, 0);
    }
    private void Update()
    {
        if (isKnightMoveable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, transform.forward, Mathf.Infinity);

                if (hit.collider == capsuleCollider)
                {
                    mousePosition.z = 0f;
                    knight[0].MoveToDestination(mousePosition);
                }

                OffKnightMove();
            }

        }
    }

    public void DestroyKnight()
    {
        ObjectPool.ReturnObject(knight[0]);
        //ObjectPool.ReturnObject(knight[1]);
        //ObjectPool.ReturnObject(knight[2]);
    }

    public void OnKnightMove()
    {
        sprite.enabled = true;
        capsuleCollider.enabled = true;
        isKnightMoveable = true;
        //Debug.Log("OnKnightMove!!");
    }

    public void OffKnightMove()
    {
        sprite.enabled = false;
        capsuleCollider.enabled = false;
        isKnightMoveable = false;
        //Debug.Log("OffKnightMove@@");
    }



}

