using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardTower : Tower
{
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        Init();
    }
    public override void Init()                         // 1�ܰ� Ÿ�� ����
    {
        TowerName = "������ Ÿ��";
        TowerLevel = 1;
        Damage = 25;
        AttackSpeed = 1.5f;
        AttackRange = "�߰Ÿ�";
        ConstructionCost = 60;
        UpgradeCost = ConstructionCost * 160 / 100;     // ���׷��̵��� �Ǽ���� 60%����
        SellingCost = ConstructionCost * 70 / 100;      // �Ǹź���� �Ǽ� ����� 70%
        TotalSpentMoney = ConstructionCost;             // �� ��� �ݾ��� �Ǽ����
    }

    public override int Upgrade()
    {
        int tempUpgradeCost = UpgradeCost;
        
        if (TowerLevel < MaxTowerLevel)
        {
            TotalSpentMoney += UpgradeCost;                 // �� ��� �ݾ׿� ���ۺ���� �߰�����
            sprite.sprite = towerLevelImage[TowerLevel - 1];// Ÿ�� �ʱⷹ���� 1, �ε����� 0���� ����

            TowerLevel++;                                   // Ÿ�� ������
            Damage += Damage * 50 / 100;                    // ������ ����
            UpgradeCost += UpgradeCost * 60 / 100;          // ���׷��̵��� �Ǽ���� 60%����
            SellingCost = TotalSpentMoney * 70 / 100;       // �Ǹűݾ� == �� ������� 70%
            transform.GetChild(0).GetComponent<TowerWeapon>().SetTowerUpgradeInfo();
            Debug.Log("������Ÿ�� ���� level : " + TowerLevel);

            return tempUpgradeCost;
        }
        else
        {
            Debug.Log("�ش� Ÿ���� MAX LEVEL�Դϴ�.");
            return 0;
        }
    }

    public override TowerType GetTowerType()
    {
        return TowerType.Wizard;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.transform.position.y > transform.position.y)
    //        collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder - 1;
    //    else
    //        collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 1;
    //}
}
