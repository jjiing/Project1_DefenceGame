using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrackTower : Tower
{
    private BarrackKnightSpawn barrackKnightSpawn;
    private knight[] knights =  new knight[3];

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        barrackKnightSpawn = transform.GetComponentInChildren<BarrackKnightSpawn>();
        for (int i = 0; i < 3; i++) 
            knights[i] = barrackKnightSpawn[i];

        Init();
    }


    public override void Init()                         // 1�ܰ� Ÿ�� ����
    {
        TowerName = "���� Ÿ��";
        TowerLevel = 1;
        Damage = 10;
        AttackSpeed = 1f;                               // ����Ÿ���� 10�ʿ� 1�� ������ ()
        AttackRange = "�ٰŸ�";
        ConstructionCost = 70;
        UpgradeCost = ConstructionCost * 160 / 100;     // ���׷��̵��� �Ǽ���� 60%����
        SellingCost = ConstructionCost * 70 / 100;      // �Ǹź���� �Ǽ� ����� 70%
        TotalSpentMoney = ConstructionCost;             // �� ��� �ݾ��� �Ǽ����
        Hp = 100;

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
            Hp += Hp * 30 / 100;                            // Hp 30%����(����Ÿ������)
            UpgradeCost += UpgradeCost * 60 / 100;          // ���׷��̵��� �Ǽ���� 60%����
            SellingCost = TotalSpentMoney * 70 / 100;       // �Ǹűݾ� == �� ������� 70%
            transform.GetChild(0).GetComponent<BarrackKnightSpawn>().SetTowerUpgradeInfo();

            return tempUpgradeCost;
        }
        else
        {
            Debug.Log("�ش� Ÿ���� MAX LEVEL�Դϴ�.");
            return 0;
        }
    }

    public void DestroyKnight()
    {
        barrackKnightSpawn.DestroyKnight();
    }

    public override TowerType GetTowerType()
    {
        return TowerType.Barrack;
    }


}
