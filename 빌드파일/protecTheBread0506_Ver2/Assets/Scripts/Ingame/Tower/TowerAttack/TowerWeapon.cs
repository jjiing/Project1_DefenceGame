using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponState { SearchTarget, AttackToTarget }

public class TowerWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject  projectile;                     // ����ü ������
    [SerializeField]
    private Transform   spawnPoint;                     // ����ü �߻���ġ

    private Tower       myTower;                        // Ÿ���� ������ ����
    private int         towerLevel;                     // Ÿ���� ���� 
    private int         damage;                         // Ÿ���� ������
    private float       attackSpeed;                    // Ÿ���� ���ݼӵ�
    
    private WeaponState weaponState;                    // Ÿ���� ����[0-��ǥ������, 1-��ǥ������]

    private List<Enemy> towerEnemyList;                 // Ÿ�� ��Ÿ� ���� ���� ����Ʈ
    private Transform   targetEnemy;                    // ���ݴ������ ������ Ÿ��

    private Animator    animator;                       // �ִϸ����� 
    private void Start()
    {
        myTower = transform.parent.GetComponent<Tower>();   // �θ������ Ÿ���� ������ ������ ����
        damage = myTower.Damage;                            // Ÿ�� ������ 
        attackSpeed = myTower.AttackSpeed;                  // Ÿ�� ���ݼӵ� 
        towerEnemyList = new List<Enemy>();                 // ����Ʈ �ʱ�ȭ

        if(myTower.GetTowerType() == TowerType.Catapult)
            animator = transform.parent.GetComponent<Animator>();   // �ִϸ����� 
        else if(myTower.GetTowerType() == TowerType.Archer)
            animator = transform.parent.GetChild(2).GetComponent<Animator>();
        else if(myTower.GetTowerType() == TowerType.Wizard)
            animator = transform.parent.GetChild(2).GetComponent<Animator>();
        

        weaponState = WeaponState.SearchTarget;             // Ÿ���� ���� [��ǥ������]
        StartCoroutine(weaponState.ToString());             // [��ǥ������]���� �ڷ�ƾ����
    }

    public void SetTowerUpgradeInfo()                       // Ÿ�� ���׷��̵�� �� �缳��
    {
        this.damage = myTower.Damage;                       // ���ݷ� ���� 
        this.attackSpeed = myTower.AttackSpeed;             // ���� ����
        this.towerLevel = myTower.TowerLevel;               // Ÿ�� ���� ����
        animator.SetInteger("Level", towerLevel);           // Ÿ�� ������ ���� �ִϸ��̼� ����
        Debug.Log(towerLevel);
    }

    public void ChangeState(WeaponState newState)           // [��ǥ������<->��ǥ������] �ڷ�ƾ����
    {
        StopCoroutine(weaponState.ToString());              // ���� �������� �ڷ�ƾ ����
        
        weaponState = newState;
        //Debug.Log("ChangeState! �ٲ� ���� : " + weaponState);
        StartCoroutine(weaponState.ToString());             // ����� ���·� �ڷ�ƾ ����
    }

    private IEnumerator SearchTarget()                      // [��ǥ������] �ڷ�ƾ
    {
        while(true)
        {
            float closeDistsqr = Mathf.Infinity;        

            for (int i =0;i < towerEnemyList.Count; i++)     // ��Ÿ��� ���� ����� ���� ��ǥ���ͷ� ����
            {
                float distance = Vector3.Distance(towerEnemyList[i].transform.position, transform.position);

                if (distance <= closeDistsqr)
                {
                    closeDistsqr = distance;
                    targetEnemy = towerEnemyList[i].transform;
                }
            }

            if(targetEnemy != null)                         // Ÿ���� �����Ǹ� [��ǥ������->��ǥ������] �ڷ�ƾ���κ���
            {
                ChangeState(WeaponState.AttackToTarget);     
            }

            yield return null;
        }
    }

    private IEnumerator AttackToTarget()
    {
        Attack();
        

        while (true)
        {
            if (targetEnemy == null)                        // ��ǥ���Ͱ� ���ٸ� [��ǥ������]���� ����
            {
                ChangeState(WeaponState.SearchTarget);
                //yield return null;
            }
            
            yield return new WaitForSeconds(attackSpeed);   // ���Ӹ�ŭ ��� �� ����

            if (targetEnemy != null)                        // Ÿ���� ���� ����ִٸ� ����
            {
                Attack();                                   
            }
        }
    }


    private void Attack()
    {
        if (targetEnemy.position.x < transform.position.x)  // ����ġ�� ���� �ִϸ��̼� ����
            animator.SetTrigger("LeftAttack");              
        else
            animator.SetTrigger("RightAttack");

        if (myTower.GetTowerType() == TowerType.Catapult)   // �������϶�
        {
            Invoke("SpawnProjectile", 0.12f);               // �������� 0.12�� �� ����ü �߻��ϵ��� ����        
            SfxManager.instance.CatapultAtk();
        }
        else if (myTower.GetTowerType() == TowerType.Wizard)
        {
            Invoke("SpawnProjectile", 0.17f);               // �������� 0.17�� �� ����ü �߻��ϵ��� ����
            SfxManager.instance.MagicAtk();
        }
        else if (myTower.GetTowerType() == TowerType.Archer)
        {
            Invoke("SpawnProjectile", 0.16f);
            SfxManager.instance.ArcherAtk();
        }

        
    }


    private void SpawnProjectile()                                  // ����ü ����
    {
        GameObject temp = Instantiate(projectile, spawnPoint.position, Quaternion.identity);
        temp.GetComponent<Projectile>().SetUp(targetEnemy, damage); // ����ü �� ����
    }

    private void OnTriggerEnter2D(Collider2D collision)             // ��Ÿ� ���� ���� ����Ʈ�� �߰�
    {
        if (collision.tag == "Monster")
        {
            towerEnemyList.Add(collision.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)              // ��Ÿ� �� or ����óġ�� ����Ʈ���� ����
    {
        if (collision.tag == "Monster")
        {
            if (targetEnemy == collision.transform)                 // ��ǥ ���Ͱ� ��Ÿ� �� or ����óġ�� NULL�� ����
            {
                targetEnemy = null;
            }
            towerEnemyList.Remove(collision.GetComponent<Enemy>());
        }
    }
}
