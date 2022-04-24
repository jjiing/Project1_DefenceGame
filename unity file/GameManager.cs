using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 씬이 넘어가도 데이터 저장되도록 

    private int gold;
    private int diamond;

    private int star;       // 스테이지 클리어 별?
    private int life;       // 인게임 라이프(빵)

    public int Gold
    {
        get{ return gold; }
        set{ gold = value; }
    }
    public int Diamond
    {
        get { return diamond; }
        set {diamond = value;}
    }
    public int Star
    {
        get { return star; }
        set {star = value;}
    }
    public int Life
    {
        get { return life; }
        set {life = value;}
    }

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);  // 씬이 넘어가도 object 삭제 X
    }

}
