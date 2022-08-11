using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MonsterGround : MonoBehaviour
{
    [SerializeField]
    private TowerSelectUI       towerSelectUI;          // Ÿ������UI
    [SerializeField]
    private TowerUpgradeSellUI  towerUpgradeSellUI;     // Ÿ�������Ǹ�UI

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Ground.prevSelectGround != null)        // �������õ� Ground�� �ִٸ�
            {
                Ground.prevSelectGround.GetComponent<SpriteRenderer>().color = Color.white;

                if (Ground.prevSelectGround.isTowerBuild)
                {
                    if (Ground.prevSelectGround.BuiltTower.GetTowerType() == TowerType.Barrack)
                    {
                        Ground.prevSelectGround.towerAttackRange.enabled = false;
                    }
                }
            }


            towerSelectUI.OffTowerSelectUI();           // Ÿ������ UI���� 
            towerUpgradeSellUI.OffTowerUpSell_UI();     // Ÿ�������Ǹ� UI����
        }
    }

}
