using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TowerType { Barrack = 0, Archer, Catapult, Wizard }
abstract public class Tower : MonoBehaviour
{
    [SerializeField]
    protected Sprite[] towerLevelImage;     // ������ Ÿ�� �̹��� 2�� 
    protected SpriteRenderer sprite;        // Ÿ���� ��������Ʈ������ ����

    public const int MaxTowerLevel = 3;     // Ÿ�� �ִ� ����
    
    public Sprite towerTypeImage;           // �����ϴ�UI Ÿ���̹���
    private string towerName;               // UI�� ǥ�õ� Ÿ�� �̸�
    private int towerLevel;                 // Ÿ�� ���� (LV1 ~ LV3)
    private int damage;                     // Ÿ�� ���ݷ�
    private float attackSpeed;              // Ÿ�� ���ݼӵ�
    private string attackRange;             // Ÿ�� ��Ÿ� (���ڿ�)
    private int constructionCost;           // Ÿ�� �Ǽ����
    private int upgradeCost;                // Ÿ�� ���׷��̵���
    private int sellingCost;                // Ÿ�� �Ǹź�� ((�Ǽ��� + ���ۺ�)�� 70%)
    private int totalSpentMoney;            // Ÿ���� �� ����� �ݾ�(�Ǽ��� + ���ۺ�)
    private int hp;                         // ����Ÿ������! (����� hp)

    public string TowerName
    { get { return towerName; } set { towerName = value; }}
    public int TowerLevel
    { get { return towerLevel; } set { towerLevel = value;}}
    public int Damage
    { get { return damage; } set { damage = value;}}
    public float AttackSpeed
    { get { return attackSpeed; } set { attackSpeed = value;}}
    public string AttackRange
    { get { return attackRange; } set { attackRange = value; }}
    public int ConstructionCost
    { get { return constructionCost; } set { constructionCost = value;}}
    public int UpgradeCost
    { get { return upgradeCost; } set { upgradeCost = value;}}
    public int SellingCost
    { get { return sellingCost; } set { sellingCost = value;}}
    public int TotalSpentMoney
    { get { return totalSpentMoney; } set { totalSpentMoney = value;}}
    public int Hp
    { get { return hp; } set { hp = value; }}

    
    public abstract void Init();
    public abstract int Upgrade();
    public abstract TowerType GetTowerType();


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster") || collision.CompareTag("Player") || collision.CompareTag("Knight"))
        {
            if (collision.transform.position.y > transform.position.y)
                collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder - 1;
            else
                collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 1;
        }
    }


}
