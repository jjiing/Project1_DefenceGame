using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KnightRecognitionRange : MonoBehaviour
{
    public List<Enemy> monsterList;

    private void Awake()
    {
        monsterList = new List<Enemy>(); 
    }


    private void OnTriggerEnter2D(Collider2D collision) //���� �����ȿ� ������ ����Ʈ�� �ֱ� 
    {
        if (collision.CompareTag("Monster"))
        {
            monsterList.Add(collision.GetComponent<Enemy>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)  //���Ͱ� �������� ������ ����Ʈ���� ����
    {
        if (collision.CompareTag("Monster"))
        {
            Enemy collisionEnemy = collision.GetComponent<Enemy>();

            if (transform.parent.GetComponent<knight>().TargetMonster == collisionEnemy)
            {
                transform.parent.GetComponent<knight>().TargetMonster = null;
            }

            monsterList.Remove(collisionEnemy);
        }
    }


}
