using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    //HP
    //값 범위정해서 랜덤으로 하게 하기********
    protected int warriorHPnow = 20;       //현재체력
    protected int warriorHPmax = 100;       //최대체력
    protected int warriorHPup = 5;         //힐량

    //Atk
    protected int warriorAtk = 40;



    //스킬쿨타임
    protected float coolTime2 = 10f;        //자힐
    protected float coolTime3 = 15f;        //병사소환
    protected float coolTime4 = 4f;         //궁극기 - 광역딜

    float coolTimeRevive=5f;


    bool isCoolRevive = true;



    void Start()
    {
        
    }

    
    void Update()
    {
        if (warriorHPnow <= 0)
            warriorDie();   //굳이 DIE를 함수로 만들 필요가 있나? .. 그냥 DESTROY해주면 되지않나 부활때문에 함수로?..


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
    void warriorDie()
    {
        
            Destroy(gameObject);
            //죽으면 부활하는거 구현
            //StartCoroutine(ReviveCoolTimeCo(coolTimeRevive));
            //Instantiate(gameObject);
            //transform.position = new Vector3(-9, 16, 0);
            ////가운데로 이동(걷는 모션)
        
       
    }
    IEnumerator ReviveCoolTimeCo(float cooltime)
    {
        isCoolRevive = false;
        yield return new WaitForSeconds(cooltime);
        isCoolRevive = true;
    }

}
