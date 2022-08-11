using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TowerInfoUI : MonoBehaviour
{
    private Tower selectedTower;                // ������ Ÿ��

    [SerializeField]
    private GameObject[]    towerInfoUI;        // �����ϴ�UI[0- �跰Ÿ������, 1- �跰 �� ���Ÿ�� ������]
    [SerializeField]
    private Image           towerTypeImage;     // �����ϴ�UI Ÿ���̹���
    [SerializeField]
    private Text[]          towerInfoText;      // Ÿ�� ����[0-Ÿ���̸�, 1-���ݷ�,2-����,3-��Ÿ�,4-ü��(����Ÿ��)]

    public bool isActiveTowerInfoUI;

    private void Awake()
    {
        isActiveTowerInfoUI = false;
    }

    //-----------------------------------------------------------------------------
    // description : Ÿ�������� �����ͼ� �ؽ�Ʈ�� ���
    //=============================================================================
    public void GetTowerInfo()
    {
        towerTypeImage.sprite = selectedTower.towerTypeImage;
        
        towerInfoText[0].text = selectedTower.TowerName;
        towerInfoText[1].text = selectedTower.Damage.ToString();
        towerInfoText[2].text = selectedTower.AttackSpeed + "s";
        towerInfoText[3].text = selectedTower.AttackRange;
        towerInfoText[4].text = selectedTower.Hp.ToString();            // ü�� (����Ÿ������)
        towerInfoText[5].text = selectedTower.Damage.ToString();        // ���ݷ� (����Ÿ������)
        towerInfoText[6].text = selectedTower.AttackSpeed + "s";        // ���ݼӵ� (����Ÿ������)
    }

    //-----------------------------------------------------------------------------
    // description : Ÿ������UI On, Ground�� ��������� ��������
    //=============================================================================
    public void OnTowerInfoUI(Tower tower)
    {
        this.selectedTower = tower;     // ������ ���� ��ġ�� Ÿ���� ������ Ŭ������ ��������� ����
        GetTowerInfo();

        if (tower.GetTowerType() == TowerType.Barrack)
        { 
            towerInfoUI[0].SetActive(true);
            towerInfoUI[1].SetActive(false);
        }
        else
        {
            towerInfoUI[1].SetActive(true);
            towerInfoUI[0].SetActive(false);
        }

        isActiveTowerInfoUI = true;
        gameObject.SetActive(isActiveTowerInfoUI);
    }
    //-----------------------------------------------------------------------------
    // description : "���õ�" Ÿ���� ���콺 ���ٴ�� UI On
    //=============================================================================
    public void OnMouseEnterGround()
    {
        isActiveTowerInfoUI = true;
        gameObject.SetActive(isActiveTowerInfoUI);
    }
    //-----------------------------------------------------------------------------
    // description  : Ÿ������UI Off
    //=============================================================================
    public void OffTowerInfoUI()
    {
        isActiveTowerInfoUI = false;
        gameObject.SetActive(isActiveTowerInfoUI);
    }
}
