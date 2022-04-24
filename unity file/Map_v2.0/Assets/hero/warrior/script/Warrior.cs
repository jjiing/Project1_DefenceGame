using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    //public GameManager gameManager;
    int level = 1;
    //HP
    //값 범위정해서 랜덤으로 하게 하기********
    protected int warriorHPnow = 50;       //현재체력
    protected int warriorHPmax = 100;       //최대체력
    protected int warriorHPup = 5;         //힐량

    //Atk
    protected int warriorAtk = 40;
    protected int warriorUltAtk = 1;       //지속딜?




    //스킬쿨타임
    protected float coolTime2 = 40f;        //자힐
    protected float coolTime3 = 15f;        //병사소환
    protected float coolTime4 = 45f;         //궁극기 - 광역딜



    float coolTimeRevive=5f;
    bool isCoolRevive = true;



    void Start()
    {
        
    }

    
    void Update()
    {
        if (warriorHPnow <= 0)
            WarriorDie();   //굳이 DIE를 함수로 만들 필요가 있나? .. 그냥 DESTROY해주면 되지않나 부활때문에 함수로?..

        //레벨업 > 수정 필요
        if (Input.GetKeyUp(KeyCode.P))  //지금 조건 그냥 임의 설정 > 수정
        {
            //if (level < 3 && gameManager.exp >= 100) //게임매니져 참조안됨 >수정
                WarriorLevelUp();
        }

    }
    public int HP
    {
        get { return warriorHPnow; }
        protected set
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
        protected set { warriorAtk = value;}
    }

    void WarriorLevelUp()
    {
        //gameManager.exp -= 100;   //게임매니져 참조안됨 > 수정
        level += 1;
        warriorHPmax = (int)((double)warriorHPmax*1.1);
        warriorAtk = (int)((double)warriorAtk * 1.1);

    }

    void WarriorDie()
    {
        //서서히 사라지는 이펙트?
        //죽어도 빵(라이프)차감이 전혀 없나?
    }


}
