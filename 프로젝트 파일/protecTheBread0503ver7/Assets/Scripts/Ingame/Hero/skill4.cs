using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skill4 : MonoBehaviour
{
    List<GameObject> monstersAround = new List<GameObject>();   //���� ����Ʈ
    public bool isSkill4On;
    public GameObject skill4Effect;
    public Warrior war;
    public WarriorSkill warriorSkill;
    private void OnEnable()
    {
        Invoke("DestroyCircle", 3f);
        isSkill4On = true;
    }
    private void OnDisable()
    {
        for(int i=0;i<monstersAround.Count;i++)
            monstersAround.Remove(monstersAround[i]);
        isSkill4On=false;
    }


    void Update()
    {
        if (isSkill4On == true)
            StartCoroutine(Skill4Damage());
    }

    private void OnTriggerEnter2D(Collider2D collision) //���� �����ȿ� ������ ����Ʈ�� �ֱ� 
    {
        if (collision.CompareTag("Monster"))
        {
            monstersAround.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)  //���Ͱ� �������� ������ ����Ʈ���� ����
    {
        if (collision.CompareTag("Monster"))
        {
            monstersAround.Remove(collision.gameObject);

        }
    }

    private void DestroyCircle()
    {
        gameObject.SetActive(false);
        skill4Effect.SetActive(false);
        isSkill4On = false;
    }

    IEnumerator Skill4Damage()
    {
        while (monstersAround.Count > 0)
        {
            for (int i = 0; i < monstersAround.Count; i++)
            {
                monstersAround[i].GetComponent<Enemy>().nowHp -= GameManager.instance.warriorUltAtk;
                
            }
            yield return new WaitForSeconds(0.1f);
        }

    }


}
