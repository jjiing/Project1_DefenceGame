using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : Tower
{
    private void Awake()                       
    {
        sprite = GetComponent<SpriteRenderer>();
        Init();
    }
    public override void Init()                             // 1�ܰ� Ÿ�� ����
    {
        TowerName = "�ü� Ÿ��";
        TowerLevel = 1;
        Damage = 20;
        AttackSpeed = 0.8f;
        AttackRange = "�߰Ÿ�";
        ConstructionCost = 50;
        UpgradeCost = ConstructionCost * 160 / 100;         // ���׷��̵��� �Ǽ���� 60%
        SellingCost = ConstructionCost * 70 / 100;          // �Ǹź���� �Ǽ� ����� 70%
        TotalSpentMoney = ConstructionCost;                 // �� ��� �ݾ��� �Ǽ����
    }

    public override int Upgrade()
    {
        int tempUpgradeCost = UpgradeCost;

        if (TowerLevel < MaxTowerLevel)
        {
            TotalSpentMoney += UpgradeCost;                 // �� ��� �ݾ׿� ���ۺ���� �߰�����
            sprite.sprite = towerLevelImage[TowerLevel - 1];// Ÿ�� �ʱⷹ���� 1, �ε����� 0���� ����
            
            TowerLevel++;
            Damage += Damage * 50 / 100;                    // ������ ����
            UpgradeCost += UpgradeCost * 60 / 100;          // ���׷��̵��� �Ǽ���� 60%����
            SellingCost = TotalSpentMoney * 70 / 100;       // �Ǹűݾ� == �� ������� 70%
            transform.GetChild(0).GetComponent<TowerWeapon>().SetTowerUpgradeInfo();

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
        return TowerType.Archer;
    }



}
