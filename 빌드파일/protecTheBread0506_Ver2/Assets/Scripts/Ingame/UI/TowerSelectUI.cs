using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TowerSelectUI : MonoBehaviour
{
    [SerializeField]
    private GameObject[]    builtTower;                 // Ÿ�� ������ �迭

    [SerializeField]
    private GameObject[]    checkButton;                // Ÿ�� ���ù�ư �迭 (UI)
    private bool[]          isCheck;                    // Ÿ�� ���ý� �ѹ� �� Ȯ��

    private Ground          ground;                     // ������ �� ����

    [SerializeField]
    private GameObject      towerInfo;                  // Ÿ������â ��� Ÿ�������ؽ�Ʈ
    [SerializeField]
    private Text[]          towerInfoText;              // Ÿ������(UI) [0-Ÿ���̸�, 1-Ÿ������, 2-���ݷ�, 3-����, 4-���ݻ�Ÿ�, 5-���]

    public static bool      isActiveTowerSelectUI;      // UI ��뿡 ���� Flag


    private void Awake()
    {
        isCheck = new bool[4] { false, false, false, false };
        isActiveTowerSelectUI = false;
    }

    public void OnClickBuildBarrack()       // Button1 Ŭ�� �� ���� (�跰)
    {
        SelectTower((int)TowerType.Barrack);
    }
    public void OnClickBuildArcher()        // Button2 Ŭ�� �� ���� (��ó)
    {
        SelectTower((int)TowerType.Archer);
    }
    public void OnClickBuildCatapult()      // Button3 Ŭ�� �� ���� (������)
    {
        SelectTower((int)TowerType.Catapult);
    }
    public void OnClickBuildWizard()        // Button4 Ŭ�� �� ���� (���ڵ�)
    {
        SelectTower((int)TowerType.Wizard);
    }

    //-----------------------------------------------------------------------------
    // description    : Ÿ�� ���ý� �ѹ� �� üũ���� ((Ŭ��)üũǥ�� ����� -> (Ŭ��)Ÿ�� ����)
    //=============================================================================
    public void SelectTower(int index)
    {
        if (isCheck[index] == true)     // üũ����
        {
            for (int i = 0; i < checkButton.Length; i++)
            {
                isCheck[i] = false;                         // ��� �� false
            }

            towerInfo.SetActive(false);                     // Ÿ������â OFF
            checkButton[index].SetActive(false);            // Ÿ���Ǽ������� üũǥ�� ����
            ground.BuyTower(index);                         // ������ Ÿ�� �Ǽ�
        }
        else                            // üũ�� �ȵȻ���
        {  
            for (int i = 0; i < checkButton.Length; i++)    
            {
                isCheck[i] = false;                         // ��� false�� ����
                checkButton[i].SetActive(false);            // ��� üũǥ�� ����
            }

            isCheck[index] = true;                          // ���õ� Index�� true               
            checkButton[index].SetActive(true);             // ���õ� Index�� üũǥ��

            TowerInfoText(index);                           // Ÿ������â�� �ؽ�Ʈ �Է�
            towerInfo.SetActive(true);                      // Ÿ������â ON
            
        }
    }

    //-----------------------------------------------------------------------------
    // description : Ÿ������UI On, Ground�� ��������� ��������
    //=============================================================================
    public void OnTowerSelectUI(Ground ground)       
    {                             
        this.ground = ground;     // ������ ���� ������ Ŭ������ ��������� ����

        isActiveTowerSelectUI = true;
        gameObject.SetActive(isActiveTowerSelectUI);

        //����ġ�� ���� gameObject�� Active�Ǿ�� ���� �ڵ� ���డ��(�ƴϸ� ��������)
        for (int i = 0; i < checkButton.Length; i++)    // UI On������ üũǥ�� �� ��������
        {
            isCheck[i] = false;                         // ��� false�� ����
            checkButton[i].SetActive(false);            // ��� üũǥ�� ����
        }

        towerInfo.SetActive(false);
    }

    //-----------------------------------------------------------------------------
    // description    : Ÿ������UI Off
    //=============================================================================
    public void OffTowerSelectUI()
    {
        isActiveTowerSelectUI = false;
        gameObject.SetActive(isActiveTowerSelectUI);
    }

    //[0-Ÿ���̸�, 1-Ÿ������, 2-���ݷ�, 3-����, 4-���ݻ�Ÿ�, 5-���]
    private void TowerInfoText(int towerType)
    {
        if (towerType == (int)TowerType.Barrack)
        {
            towerInfoText[0].text = "���� Ÿ��";
            towerInfoText[1].text = "���� 3���� ��ȯ�ϴ� Ÿ��";
            towerInfoText[2].text = "10";
            towerInfoText[3].text = "����";
            towerInfoText[4].text = "�ٰŸ�";
            towerInfoText[5].text = "70";
        }
        else if(towerType == (int)TowerType.Archer)
        {
            towerInfoText[0].text = "��ó Ÿ��";
            towerInfoText[1].text = "���� �ӵ��� �����ϴ� Ÿ��";
            towerInfoText[2].text = "20";
            towerInfoText[3].text = "����";
            towerInfoText[4].text = "�߰Ÿ�";
            towerInfoText[5].text = "50";
        }
        else if(towerType == (int)TowerType.Catapult)
        {
            towerInfoText[0].text = "������";
            towerInfoText[1].text = "���������� �ϴ� Ÿ��";
            towerInfoText[2].text = "40";
            towerInfoText[3].text = "�ſ� ����";
            towerInfoText[4].text = "��Ÿ�";
            towerInfoText[5].text = "120";
        }
        else if(towerType == (int)TowerType.Wizard)
        {
            towerInfoText[0].text = "������ Ÿ��";
            towerInfoText[1].text = "���� �������ϴ� Ÿ��";
            towerInfoText[2].text = "25";
            towerInfoText[3].text = "����";
            towerInfoText[4].text = "�߰Ÿ�";
            towerInfoText[5].text = "60";
        }
    }

}













// ���ٰ� ���� �ڵ��..
/*
-----------------------------------------------------------------------------
 DESCRIPTS    : Ÿ���� ������ ���� ��ġ
=============================================================================
private void BuildTower(int towerIndex)
{
    GameObject tempTower;

    if (towerIndex == (int)TowerType.Wizard)           // ���ڵ� Ÿ���϶��� ��ġ�� ���� �����Ͽ� ��ġ
    {
        tempTower = Instantiate(builtTower[towerIndex], groundPosition + new Vector3(0, 0.39f, 0), Quaternion.identity); // Ÿ���� ground �߾ӿ� ��ġ�ϱ� ���� y�� ����
    }
    else                                // ������ Ÿ������ ��ġ�� �Ȱ��� �����Ͽ� ��ġ
    {
        tempTower = Instantiate(builtTower[towerIndex], groundPosition + offsetTowerPosition, Quaternion.identity);      // Ÿ���� ground �߾ӿ� ��ġ�ϱ� ���� y�� ����
    }

    // ��ġ�� Ÿ���� sortingOrder�� ������ ����
    tempTower.GetComponent<SpriteRenderer>().sortingOrder = ground.gameObject.GetComponent<SpriteRenderer>().sortingOrder;

    ground.GetComponent<SpriteRenderer>().color = Color.white;  // Ÿ���� ��ġ�ϸ� ���� ���� ������ �ٲ�
    this.ground.isTowerBuild = true;                            // ���� Ÿ���� �Ǽ��Ǿ���(true)
    OffTowerSelectUI();
}
*/