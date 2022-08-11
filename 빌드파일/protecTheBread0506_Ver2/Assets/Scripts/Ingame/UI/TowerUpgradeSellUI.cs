using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum FuncNum { Upgrade = 0, Sell, Move } // Move(����Ÿ������)

public class TowerUpgradeSellUI : MonoBehaviour
{
    [SerializeField]
    private GameObject[]    checkButton;            // Ÿ�� ���ù�ư �迭
    private bool[]          isCheck;                // Ÿ�� ���ý� �ѹ� �� Ȯ��

    [SerializeField]
    private GameObject      towerInfo;              // Ÿ������â ��� Ÿ�������ؽ�Ʈ
    [SerializeField]
    private Text[]          towerInfoText;          // Ÿ������(UI) [0-Ÿ���̸�, 1-Ÿ������, 2-���ݷ�, 3-����, 4-���ݻ�Ÿ�, 5-���]

    [SerializeField]
    private GameObject      knightMoveUI;
    private Ground          ground;                 // ������ �� ����
    private Tower           builtTower;

    public static bool      isActiveTowerUpSellUI;  // UI�� ��뿡 ���� Flag

    private void Awake()
    {
        isCheck = new bool[3] {false,false,false};
        isActiveTowerUpSellUI = false;
    }

    public void OnClickUpgrade()        // ���׷��̵� ��ư Ŭ���� ����
    {
        SelectFunction(FuncNum.Upgrade);
    }
    public void OnClickSell()           // �Ǹ� ��ư Ŭ���� ����
    {
        SelectFunction(FuncNum.Sell);
    }
    public void OnClickMove()           // Move��ư Ŭ���� ����
    {
        SelectFunction(FuncNum.Move);
    }

    //-----------------------------------------------------------------------------
    // description  : ���� or �Ǹ� ���ý� �ѹ� �� üũ�ϰ� ��ɼ���
    //=============================================================================
    public void SelectFunction(FuncNum funcNum)
    {
        int selectNum = (int)funcNum;

        if (isCheck[selectNum] == true)     // üũ���� (�ι�° üũ)
        {
            for (int i = 0; i < checkButton.Length; i++)    // üũ���� ��� false�� ����
            {
                isCheck[i] = false;                         
            }

            checkButton[selectNum].SetActive(false);        // üũǥ�� ����

            if (selectNum == (int)FuncNum.Sell)
            {
                ground.SellTower();                         // ������ Ÿ�� �Ǹ�
            }
            else if (selectNum == (int)FuncNum.Upgrade)
            {
                ground.UpgradeTower();                      // ������ Ÿ�� ���׷��̵�
            }
            else if (selectNum == (int)FuncNum.Move)
            {
                builtTower.GetComponentInChildren<BarrackKnightSpawn>().OnKnightMove();
                ground.UndoGround();
            }

        }
        else                                // üũ�� �ȵȻ��� (ù��° üũ)
        {
            for (int i = 0; i < checkButton.Length; i++)
            {
                isCheck[i] = false;                         // ��� false�� ����
                checkButton[i].SetActive(false);            // ��� üũǥ�� ����
            }

            isCheck[selectNum] = true;                      // ���õ� Index�� true               
            checkButton[selectNum].SetActive(true);         // ���õ� Index�� üũǥ��

            if (funcNum == FuncNum.Upgrade)
            {
                towerInfo.SetActive(true);
                TowerInfoText((int)builtTower.GetTowerType());
            }
            else
            {
                towerInfo.SetActive(false);
            }
        }
    }

    //-----------------------------------------------------------------------------
    // description  : Ÿ�������Ǹ�UI ON
    //=============================================================================
    public void OnTowerUpSell_UI(Ground ground)
    {
        this.ground = ground;
        this.builtTower = ground.BuiltTower;

        isActiveTowerUpSellUI = true;
        gameObject.SetActive(true);

        // ����ġ�� ���� gameObject�� Active�Ǿ�� ���� �ڵ� ���డ��(�ƴϸ� ��������)
        if (builtTower.GetTowerType() == TowerType.Barrack)
        {
            knightMoveUI.SetActive(true);               // �跰�� ��츸 ��ưUI Ȱ��ȭ
        }
        else
        {
            knightMoveUI.SetActive(false);              // �跰����Ÿ���� ��ưUI ��Ȱ��ȭ
        }

        // ����ġ�� ���� gameObject�� Active�Ǿ�� ���� �ڵ� ���డ��(�ƴϸ� ��������)
        for (int i = 0; i < checkButton.Length; i++)    // UI On������ üũǥ�� �� ��������
        {
            isCheck[i] = false;                         // ��� false�� ����
            checkButton[i].SetActive(false);            // ��� üũǥ�� ����
        }
    }

    //-----------------------------------------------------------------------------
    // description  : Ÿ�������Ǹ�UI Off
    //=============================================================================
    public void OffTowerUpSell_UI()
    {
        isActiveTowerUpSellUI = false;
        gameObject.SetActive(false);
        towerInfo.SetActive(false);
    }

    //[0-Ÿ���̸�, 1-Ÿ������, 2-���ݷ�, 3-����, 4-���ݻ�Ÿ�, 5-���]
    private void TowerInfoText(int towerType)
    {
        if (towerType == (int)TowerType.Barrack)
        {
            towerInfoText[0].text = builtTower.TowerName;
            towerInfoText[1].text = "LV."+builtTower.TowerLevel.ToString();
            towerInfoText[2].text = (builtTower.Damage * 150 / 100).ToString();
            towerInfoText[3].text = "����";
            towerInfoText[4].text = "�ٰŸ�";
            towerInfoText[5].text = builtTower.UpgradeCost.ToString();
        }
        else if (towerType == (int)TowerType.Archer)
        {
            towerInfoText[0].text = builtTower.TowerName;
            towerInfoText[1].text = "LV." + builtTower.TowerLevel.ToString();
            towerInfoText[2].text = (builtTower.Damage * 150 / 100).ToString();
            towerInfoText[3].text = "����";
            towerInfoText[4].text = "�߰Ÿ�";
            towerInfoText[5].text = builtTower.UpgradeCost.ToString();
        }
        else if (towerType == (int)TowerType.Catapult)
        {
            towerInfoText[0].text = builtTower.TowerName;
            towerInfoText[1].text = "LV." + builtTower.TowerLevel.ToString();
            towerInfoText[2].text = (builtTower.Damage * 150 / 100).ToString();
            towerInfoText[3].text = "�ſ� ����";
            towerInfoText[4].text = "��Ÿ�";
            towerInfoText[5].text = builtTower.UpgradeCost.ToString();
        }
        else if (towerType == (int)TowerType.Wizard)
        {
            towerInfoText[0].text = builtTower.TowerName;
            towerInfoText[1].text = "LV." + builtTower.TowerLevel.ToString();
            towerInfoText[2].text = (builtTower.Damage * 150 / 100).ToString();
            towerInfoText[3].text = "����";
            towerInfoText[4].text = "�߰Ÿ�";
            towerInfoText[5].text = builtTower.UpgradeCost.ToString();
        }
    }
}
