using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrackKnightSpawn : MonoBehaviour
{
    private Camera              mainCamera;
    private bool                isKnightMoveable = false;

    private SpriteRenderer      sprite;
    private CapsuleCollider2D   capsuleCollider;

    private Tower               myTower;                    // Ÿ���� ������ ����
    private Animator            animator;                   // Ÿ���� �ִϸ��̼� ����
    private int                 towerLevel;
    public IEnumerator          coroutine;

    private knight[] knight = new knight[3];
    

    Vector3[] knightInitoffset = new Vector3[3] {new Vector3(-0.5f, -0.8f, 0), new Vector3(0f, -0.8f, 0), new Vector3(0.5f, -0.8f, 0) };  

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
        myTower = transform.parent.GetComponent<Tower>();   // �θ������ Ÿ���� ������ ������ ����
        animator = transform.parent.GetComponent<Animator>();
    }

    private void Start()
    {
        SpawnKnight();
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
                    knight[0].MoveToDestination(mousePosition + new Vector3(0, 0.25f, 0));
                    knight[1].MoveToDestination(mousePosition + new Vector3(-0.25f, -0.25f, 0));
                    knight[2].MoveToDestination(mousePosition + new Vector3(0.25f, -0.25f, 0));
                }

                OffKnightMove();
            }
        }

        
    }

    public void SpawnKnightStart(int index, float time)
    {
        StartCoroutine(CoSpawnKnight(index,time));
    }


    public IEnumerator CoSpawnKnight(int index, float time)     // ���簡 �׾��� �� ���
    {
        yield return new WaitForSeconds(time);
        animator.SetTrigger("BarrackOpen");
        knight[index] = ObjectPool.GetObject();
        knight[index].transform.position = myTower.transform.position + new Vector3(-0.25f, -0.2f, 0);
        knight[index].BarrackSpawnSetting(this, index, myTower.transform.position + knightInitoffset[index]);
        knight[index].isBarrackSpawnDead = false;

    }
    public void SetTowerUpgradeInfo()                       // Ÿ�� ���׷��̵�� �� �缳��
    {
        //this.damage = myTower.Damage;                     // ���ݷ� ���� 
        //this.attackSpeed = myTower.AttackSpeed;           // ���� ����
        this.towerLevel = myTower.TowerLevel;               // Ÿ�� ���� ����
        animator.SetInteger("Level", towerLevel);           // Ÿ�� ������ ���� �ִϸ��̼� ����
        Debug.Log(towerLevel);
    }
    public void SpawnKnight()   // Ÿ����ġ�� ���
    {
        animator.SetTrigger("BarrackOpen");
        for (int i = 0; i < 3; i++)
        {
            knight[i] = ObjectPool.GetObject();
            knight[i].transform.position = myTower.transform.position + new Vector3(-0.25f, -0.23f, 0);
            knight[i].BarrackSpawnSetting(this, i, myTower.transform.position + knightInitoffset[i]);
        }
    }

    public void DestroyKnight()
    {
        ObjectPool.ReturnObject(knight[0]);
        ObjectPool.ReturnObject(knight[1]);
        ObjectPool.ReturnObject(knight[2]);
    }

    public void OnKnightMove()
    {
        sprite.enabled = true;
        capsuleCollider.enabled = true;
        isKnightMoveable = true;
    }

    public void OffKnightMove()
    {
        sprite.enabled = false;
        capsuleCollider.enabled = false;
        isKnightMoveable = false;
    }



}

















//public void InvokeSpawnKnight(int index, float time)
//{
//    Invoke("SpawnKnight" + index, time);
//}

//public void SpawnKnight0()
//{
//    knight[0] = ObjectPool.GetObject();
//    knight[0].transform.position = myTower.transform.position + new Vector3(-0.2f, -0.2f, 0);
//    knight[0].BarrackSpawnSetting(this, 0, myTower.transform.position + new Vector3(-0.7f, -0.8f, 0));
//    knight[0].isBarrackSpawnDead = false;
//    Debug.Log("SpawnKnight1");
//}

//public void SpawnKnight1()
//{
//    knight[1] = ObjectPool.GetObject();
//    knight[1].transform.position = myTower.transform.position + new Vector3(-0.2f, -0.2f, 0);
//    knight[1].BarrackSpawnSetting(this, 1,myTower.transform.position + new Vector3(-0.2f, -0.8f, 0));
//    knight[1].isBarrackSpawnDead = false;
//    Debug.Log("SpawnKnight2");
//}

//public void SpawnKnight2()
//{
//    knight[2] = ObjectPool.GetObject();
//    knight[2].transform.position = myTower.transform.position + new Vector3(-0.2f, -0.2f, 0);
//    knight[2].BarrackSpawnSetting(this, 2,myTower.transform.position + new Vector3(0.3f, -0.8f, 0));
//    knight[2].isBarrackSpawnDead = false;
//    Debug.Log("SpawnKnight3");
//}