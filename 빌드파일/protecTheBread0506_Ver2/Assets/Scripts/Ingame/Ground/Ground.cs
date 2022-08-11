using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Ground : MonoBehaviour
{
    [SerializeField]
    private TowerSelectUI       towerSelectUI;          // Ÿ������UI
    [SerializeField]
    private TowerUpgradeSellUI  towerUpgradeSellUI;     // Ÿ�������Ǹ�UI
    [SerializeField]
    private TowerInfoUI         towerInfoUI;            // Ÿ������UI
    [SerializeField]
    private GameObject          noMoneyText;

    [SerializeField]
    private GameObject[]        towerPrefab;            // Ÿ�� ������ �迭
    [SerializeField]
    private InGameManager       inGameManager;          // ���ӸŴ���(���, ���̾�, �� ���� ���)

    private int[]               constructionCost;       // �ǹ� �Ǽ����
    private GameObject          builtTower;             // ���� ���� ��ġ�� Ÿ��
    public SpriteRenderer       towerAttackRange;       // ���� ���� ��ġ�� Ÿ���� ���ݻ�Ÿ�ǥ��
    public bool                 isTowerBuild;           // Ÿ���� ���綥�� ���������� Flag

    private Camera              mainCamera;
    private SpriteRenderer      sprite;                 // ���� ���� Sprite
    
    public static Ground        prevSelectGround;       // ���� ���� ��

    public Tower BuiltTower
    {
        get { return builtTower.GetComponent<Tower>(); }
    }

    private void Awake()
    {
        constructionCost = new int[4] { 70, 50, 120, 60 };      // [�跰,�ü�,������,����]
        inGameManager = FindObjectOfType<InGameManager>();
        sprite = GetComponent<SpriteRenderer>(); 
        mainCamera = Camera.main;

        isTowerBuild = false;
    }

    private void OnMouseDown()
    {
      
            if (!EventSystem.current.IsPointerOverGameObject()) // UI�� ���� ������ UI�̺�Ʈ�� �߻�
            {
                // (�ٸ� �� ���������� ���� ���� ���� ���� ������� �ǵ���)
                if (prevSelectGround != null)
                {                 // ���� ���õ� ���� �ִٸ� (ó�������� ���� �ƴ϶��)
                    prevSelectGround.GetComponent<SpriteRenderer>().color = Color.white; // ���� ������ ����.

                    if (prevSelectGround.isTowerBuild)
                    {
                        if(prevSelectGround.BuiltTower.GetTowerType() == TowerType.Barrack)
                        {
                            prevSelectGround.towerAttackRange.enabled = false;
                        }
                    }

                }
                prevSelectGround = this.gameObject.GetComponent<Ground>();             // ���� ���� ������ ����

                OnClickGroundUI();
            }
        
    }
    private void OnMouseEnter()
    {
        if (sprite.color == new Color(1f, 0.4f, 0.4f, 0.8f))    // "Ÿ��"�� Ŭ���� �����϶�
        {
            towerInfoUI.OnMouseEnterGround();
            towerAttackRange.enabled = true;
        }
    }

    private void OnMouseExit()          
    {
        if (builtTower != null && BuiltTower.GetTowerType() != TowerType.Barrack)   // ������ Ÿ���� �跰�� �ƴҶ�
        {
            towerInfoUI.OffTowerInfoUI();                       // Ÿ������UI OFF
            towerAttackRange.enabled = false;                   // ���ݻ�Ÿ�ǥ�� ����
        }
        else if (builtTower != null && BuiltTower.GetTowerType() == TowerType.Barrack)
        {
            towerInfoUI.OffTowerInfoUI();                       // Ÿ������UI OFF
        }
    }

    void OnClickGroundUI()
    {
        if (isTowerBuild == false)  // Ÿ���� ������ -> Ÿ������UI
        {
            sprite.color = new Color(0.3f, 1, 0.3f, 0.8f);                          // ���õ� �� �ʷϻ����� ǥ��
            towerUpgradeSellUI.OffTowerUpSell_UI();

            // ���� �߽����� Ÿ������UI ��ġ��������
            towerSelectUI.GetComponent<RectTransform>().position = mainCamera.WorldToScreenPoint(transform.position + new Vector3(0, 0.3f, 0));
            
            towerSelectUI.OnTowerSelectUI(gameObject.GetComponent<Ground>());       // UI ON, �� ������ �Ű������� ��
        }
        else                        // Ÿ���� ������ -> Ÿ�������Ǹ�UI �� Ÿ������UI
        {
            sprite.color = new Color(1f, 0.4f, 0.4f, 0.8f);                         // ���õ� �� ���������� ǥ��
            towerSelectUI.OffTowerSelectUI();
            
            towerAttackRange.enabled = true;                                        // Ÿ�� ���ݹ��� ǥ��
            
            // Ÿ������UI ���
            towerInfoUI.OnTowerInfoUI(builtTower.GetComponent<Tower>());            // Ÿ������UI ON
            // ���� �߽����� Ÿ������UI ��ġ�������� (Ÿ������ �����ʿ�)
            towerUpgradeSellUI.GetComponent<RectTransform>().position = mainCamera.WorldToScreenPoint(transform.position + new Vector3(0, 0.4f, 0));
            towerUpgradeSellUI.OnTowerUpSell_UI(gameObject.GetComponent<Ground>()); // UI ON, �� ������ �Ű������� ��
        }
            
    }

    public void BuyTower(int towerindex)
    {
        //sprite.color = Color.white;                         // Ÿ���� ��ġ�ϸ� ���� ���� ������ �ٲ�
        //towerSelectUI.OffTowerSelectUI();                   // UI ��Ȱ��ȭ

        Vector3 offsetTowerPosition = new Vector3(0, 0.6f, 0);

        if (inGameManager.Gold - constructionCost[towerindex] >= 0) // ���� ���� ���
        {
            isTowerBuild = true;                                // ���� Ÿ���� �Ǽ��Ǿ���(= true)
            SfxManager.instance.BuildTower();

            if (towerindex == (int)TowerType.Wizard)            // ���ڵ� Ÿ���϶��� ��ġ�� ���� �����Ͽ� ��ġ
            {
                builtTower = Instantiate(towerPrefab[towerindex], transform.position + new Vector3(0, 0.39f, 0), Quaternion.identity); // Ÿ���� ground �߾ӿ� ��ġ�ϱ� ���� y�� ����
                towerAttackRange = builtTower.transform.GetChild(0).GetComponent<SpriteRenderer>();     // Ÿ���� ���ݹ��� ǥ��
            }
            else                                                // ������ Ÿ������ ��ġ�� �Ȱ��� �����Ͽ� ��ġ
            {
                builtTower = Instantiate(towerPrefab[towerindex], transform.position + offsetTowerPosition, Quaternion.identity);      // Ÿ���� ground �߾ӿ� ��ġ�ϱ� ���� y�� ����
                towerAttackRange = builtTower.transform.GetChild(0).GetComponent<SpriteRenderer>();     // Ÿ���� ���ݹ��� ǥ��
            }

            inGameManager.Gold -= builtTower.GetComponent<Tower>().ConstructionCost;  // �Ǽ������ ����
                                                                                      // ������ Ÿ���� ����Ÿ���� �������� �ʰ� ����
            builtTower.GetComponent<SpriteRenderer>().sortingOrder = sprite.sortingOrder;
        }
        else
        {
            noMoneyText.SetActive(true);
            noMoneyText.GetComponent<RectTransform>().position = mainCamera.WorldToScreenPoint(transform.position + new Vector3(0, 0.3f, 0));
            noMoneyText.GetComponent<NoMoneyText>().OnNoMoneyText();
        }

        UndoGround();
    }

    public void SellTower()
    {
        isTowerBuild = false;                                   // ���� Ÿ���� ����(= false)

        SfxManager.instance.DestroyTower();
        inGameManager.Gold += builtTower.GetComponent<Tower>().SellingCost;       // �Ǹź���� ������

        if (BuiltTower is BarrackTower)
        {
            BarrackTower barrackTower = BuiltTower as BarrackTower;
            barrackTower.DestroyKnight();
        }

        UndoGround();
        Destroy(builtTower);                                    // �Ⱦ����ϱ� �ı�
    }

    public void UpgradeTower()
    {

        if (inGameManager.Gold - BuiltTower.UpgradeCost >= 0)   // ���� ������ ����!
        {
            inGameManager.Gold -= BuiltTower.Upgrade();         // Ÿ���� ���ݷ�, ���ۺ�� �� ����
        }
        else
        {
            noMoneyText.SetActive(true);
            noMoneyText.GetComponent<RectTransform>().position = mainCamera.WorldToScreenPoint(transform.position + new Vector3(0, 0.3f, 0));
            noMoneyText.GetComponent<NoMoneyText>().OnNoMoneyText();
            
        }

        UndoGround();
    }

    public void UndoGround()
    {
        sprite.color = Color.white;                         // ���� ���� ������� �ٲ�
        towerSelectUI.OffTowerSelectUI();                   // UI ��Ȱ��ȭ
        towerUpgradeSellUI.OffTowerUpSell_UI();             // Ÿ�������Ǹ�UI OFF
    }
}






// ���ٰ� ���� �ڵ��.. UI�� ����ȭ�� �����ڸ����� ©���°� �����Ҷ� ���� �ڵ�
/*
���� TowerSelect UI ������ 
if ((Input.mousePosition.x <= Screen.width / 2) && (Input.mousePosition.y >= Screen.height / 2))
{
    //Debug.Log("2��и�");
    towerSelectUI_rectTransform.pivot = Vector2.up;

}
else if ((Input.mousePosition.x <= Screen.width / 2) && (Input.mousePosition.y < Screen.height / 2))
{
    //Debug.Log("3��и�");
    towerSelectUI_rectTransform.pivot = Vector2.zero;
}
else if ((Input.mousePosition.x > Screen.width / 2) && (Input.mousePosition.y >= Screen.height / 2))
{
    //Debug.Log("1��и�");
    towerSelectUI_rectTransform.pivot = Vector2.one;
}
else
{
    //Debug.Log("4��и�");
    towerSelectUI_rectTransform.pivot = Vector2.right;
}
*/