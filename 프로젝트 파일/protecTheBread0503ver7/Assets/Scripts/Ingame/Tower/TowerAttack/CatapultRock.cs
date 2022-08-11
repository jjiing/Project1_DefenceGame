using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultRock : Projectile
{
    private Collider2D collider2d;

    private GameObject tempEffect;
    private bool isEffectOccur;

    private void Update()
    {
        if (Target != null)             // target�� �����ϸ�
        {
            MoveDirection = (Target.position - transform.position).normalized;
            if (Vector3.Distance(Target.position, transform.position) <= 0.2f)
            {
                collider2d.enabled = true;
                if (isEffectOccur == false)     // ���� �ε����� ����Ʈ 1���� ����ǵ���
                {
                    isEffectOccur = true;
                    tempEffect = Instantiate(effect, transform.position - new Vector3(0, -0.1f, 0), transform.rotation);
                    Destroy(tempEffect, 1f);
                }
            }
        }
        else                            // target�� ���ٸ�
        {
            Destroy(gameObject);
        }

        transform.position += MoveDirection * MoveSpeed * Time.deltaTime;
    }

    public override void SetUp(Transform target, int damage)
    {
        collider2d = GetComponent<Collider2D>();
        
        Target = target;
        Damage = damage;
        isEffectOccur = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Monster")) return;   // ���� �ƴ� ���� �ε����� 
        //if (collision.transform != Target) return;    // ���� Target�� ���� �ƴ� ��

        Enemy tempEnemy = collision.GetComponent<Enemy>();
        tempEnemy.co_hit = tempEnemy.Hit(Damage);

        tempEnemy.StartCoroutine(tempEnemy.co_hit);

        Destroy(gameObject);
    }
}
