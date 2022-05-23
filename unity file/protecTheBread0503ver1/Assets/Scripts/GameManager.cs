using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    public static GameManager instance=null; // ���� �Ѿ�� ������ ����ǵ��� 
    public Text diamondText;
    public Text statDiaText;
    public Text attackText;
    public Text HPText;
    public Text speedText;
    public Text heroPicLevel;
    public Text heroInfoLevel;
    public Text starvalue;


    public int level = 1;
    //HP
    public int warriorHPnow;       //����ü��
    public int warriorHPmax = 100;       //�ִ�ü��
    public int warriorHPup;         //����

    //Atk
    public int warriorAtk ;
    public float warriorUltAtk;       //���ӵ�? > �ѹ� ���� 60�� & ������

    private string warriorSpeed = "보통";

    public int diamond;
    public int star;

    
    private int exp;
    private int expMax;

    


    public int Diamond
    {
        get { return diamond; }
        set { diamond = value; }
    }
    public int Star
    {
        get { return star; }
        set {star = value;}
    }
    public int Exp      
    {
        get { return exp; }
        set {exp = value;}
    }
    public int EXPMax
    {
        get { return expMax; }
        set {expMax = value;}
    }
    public int HP
    {
        get { return warriorHPnow; }
        set
        {
            if (value > warriorHPmax)
                value = warriorHPmax;
            else if (value < 0)
                value = 0;

            warriorHPnow = value;
        }
    }
    public int Atk
    {
        get { return warriorAtk; }
        set { warriorAtk = value; }
    }

    private void Awake()
    {
        warriorHPnow = 100;

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);  // ���� �Ѿ�� object ���� X

        warriorHPup = (int)((double)warriorHPmax * 0.2);
        
    }
    void Start()
    {
        exp = 0;
        expMax = 100;
        Diamond = 0;
        warriorAtk = 10;
        warriorUltAtk = 0.1f;


    }

    public void SetTexts()
    {
        if (diamondText == null)
            diamondText = GameObject.FindWithTag("DiamondUI").GetComponent<Text>();
        if (statDiaText == null)
            statDiaText = GameObject.FindWithTag("DiamondUIInfo").GetComponent<Text>();
        if (attackText == null)
            attackText = GameObject.FindWithTag("AttackUI").GetComponent<Text>();
        if (HPText == null)
            HPText = GameObject.FindWithTag("HPUI").GetComponent<Text>();
        if (speedText == null)
            speedText = GameObject.FindWithTag("SpeedUI").GetComponent<Text>();
        if (heroPicLevel == null)
            heroPicLevel = GameObject.FindWithTag("LevelUI").GetComponent<Text>();
        if (heroInfoLevel == null)
            heroInfoLevel = GameObject.FindWithTag("LevelUIInfo").GetComponent<Text>();
        if (starvalue == null)
            starvalue = GameObject.FindWithTag("StarUI").GetComponent<Text>();

    }
    private void LateUpdate()
    {
        //SetTexts();

        if(diamondText != null)
        {
            diamondText.text = Diamond.ToString();
            statDiaText.text = Diamond.ToString();
            attackText.text = warriorAtk.ToString();    
            HPText.text = warriorHPmax.ToString();        
            speedText.text = warriorSpeed;
            heroPicLevel.text = level.ToString();   
            heroInfoLevel.text = level.ToString();
            starvalue.text = Star.ToString();
        }
        
    }


}
