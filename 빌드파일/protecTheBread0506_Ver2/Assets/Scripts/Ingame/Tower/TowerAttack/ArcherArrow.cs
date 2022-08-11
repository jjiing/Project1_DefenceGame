using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherArrow : Projectile
{
    private GameObject tempEffect;
    private void Update()
    {
        if (Target != null)         // target�� �����ϸ�
        {
            MoveDirection = (Target.position - transform.position).normalized;
            LookAt2D(MoveDirection);
        }
        else                        // target�� ���ٸ�
        {
            Destroy(gameObject);
        }

        transform.position += MoveDirection * MoveSpeed * Time.deltaTime;
    }
    protected void LookAt2D(Vector3 dir)                // ȭ���� ���� ������ ���ϵ��� ����
    {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Monster")) return;   // ���� �ƴ� ���� �ε����� 
        if (collision.transform != Target) return;      // ���� Target�� ���� �ƴ� ��

        tempEffect = Instantiate(effect, transform.position - new Vector3(0, -0.1f, 0), transform.rotation);
        Destroy(tempEffect, 1f);

        Enemy tempEnemy = collision.GetComponent<Enemy>();
        tempEnemy.co_hit = tempEnemy.Hit(Damage);
        tempEnemy.StartCoroutine(tempEnemy.co_hit);

        Destroy(gameObject);
    }

}



//[SerializeField]
//private float moveSpeed;                            // ����ü �ӵ�
//private Vector3 moveDirection = Vector3.zero;       // ����ü �߻����
//private int damage;                                 // ����ü ���ݷ�

//private Transform target;                           // ���󰡴� ��ǥ����

//public void SetUp(Transform target, int damage)
//{
//    this.target = target;
//    this.damage = damage;
//}

//private void Update()
//{
//    if (target != null)         // target�� �����ϸ�
//    {
//        moveDirection = (target.position - transform.position).normalized;
//    }
//    else                        // target�� ���ٸ�
//    {
//        Destroy(gameObject);
//    }
//    transform.position += moveDirection * moveSpeed * Time.deltaTime;
//}


//private void OnTriggerEnter2D(Collider2D collision)
//{
//    if (!collision.CompareTag("Monster")) return;   // ���� �ƴ� ���� �ε����� 
//    if (collision.transform != target) return;      // ���� Target�� ���� �ƴ� ��

//    collision.GetComponent<Enemy>().hp -= damage;
//    Destroy(gameObject);
//}