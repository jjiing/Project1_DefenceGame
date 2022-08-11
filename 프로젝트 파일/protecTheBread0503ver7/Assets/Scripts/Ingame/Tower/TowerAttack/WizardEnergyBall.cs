using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardEnergyBall : Projectile
{
    private GameObject tempEffect;

    private void Update()
    {
        if (Target != null)         // target�� �����ϸ�
        {
            MoveDirection = (Target.position - transform.position).normalized;
        }
        else                        // target�� ���ٸ�
        {
            Destroy(gameObject);
        }

        transform.position += MoveDirection * MoveSpeed * Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Monster")) return;   // ���� �ƴ� ���� �ε����� 
        if (collision.transform != Target) return;      // ���� Target�� ���� �ƴ� ��
        
        tempEffect = Instantiate(effect, transform.position - new Vector3(0, -0.1f, 0), transform.rotation);
        Destroy(tempEffect,1f);

        Enemy tempEnemy = collision.GetComponent<Enemy>();
        tempEnemy.co_hit = tempEnemy.WizardHit(Damage);
        tempEnemy.StartCoroutine(tempEnemy.co_hit);

        Destroy(gameObject);
    }


}
