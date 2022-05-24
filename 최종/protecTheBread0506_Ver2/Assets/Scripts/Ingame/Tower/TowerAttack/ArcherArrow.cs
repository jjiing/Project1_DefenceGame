using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherArrow : Projectile
{
    private GameObject tempEffect;
    private void Update()
    {
        if (Target != null)         // target이 존재하면
        {
            MoveDirection = (Target.position - transform.position).normalized;
            LookAt2D(MoveDirection);
        }
        else                        // target이 없다면
        {
            Destroy(gameObject);
        }

        transform.position += MoveDirection * MoveSpeed * Time.deltaTime;
    }
    protected void LookAt2D(Vector3 dir)                // 화살이 적의 방향을 향하도록 해줌
    {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Monster")) return;   // 적이 아닌 대상과 부딪히면 
        if (collision.transform != Target) return;      // 현재 Target이 적이 아닐 때

        tempEffect = Instantiate(effect, transform.position - new Vector3(0, -0.1f, 0), transform.rotation);
        Destroy(tempEffect, 1f);

        Enemy tempEnemy = collision.GetComponent<Enemy>();
        tempEnemy.co_hit = tempEnemy.Hit(Damage);
        tempEnemy.StartCoroutine(tempEnemy.co_hit);

        Destroy(gameObject);
    }

}



//[SerializeField]
//private float moveSpeed;                            // 투사체 속도
//private Vector3 moveDirection = Vector3.zero;       // 투사체 발사방향
//private int damage;                                 // 투사체 공격력

//private Transform target;                           // 날라가는 목표몬스터

//public void SetUp(Transform target, int damage)
//{
//    this.target = target;
//    this.damage = damage;
//}

//private void Update()
//{
//    if (target != null)         // target이 존재하면
//    {
//        moveDirection = (target.position - transform.position).normalized;
//    }
//    else                        // target이 없다면
//    {
//        Destroy(gameObject);
//    }
//    transform.position += moveDirection * moveSpeed * Time.deltaTime;
//}


//private void OnTriggerEnter2D(Collider2D collision)
//{
//    if (!collision.CompareTag("Monster")) return;   // 적이 아닌 대상과 부딪히면 
//    if (collision.transform != target) return;      // 현재 Target이 적이 아닐 때

//    collision.GetComponent<Enemy>().hp -= damage;
//    Destroy(gameObject);
//}