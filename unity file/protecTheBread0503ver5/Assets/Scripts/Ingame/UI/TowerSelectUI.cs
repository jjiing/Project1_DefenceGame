using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TowerSelectUI : MonoBehaviour
{
    [SerializeField]
    private GameObject[] builtTower;            // 타워 프리팹 배열

    [SerializeField]
    private GameObject[] checkButton;           // 타워 선택버튼 배열
    private bool[] isCheck;                     // 타워 선택시 한번 더 확인

    private Ground ground;                      // 선택한 땅 정보

    public static bool isActiveTowerSelectUI;   // UI가 사용에 관한 Flag


    private void Awake()
    {
        isCheck = new bool[4] { false, false, false, false };
        isActiveTowerSelectUI = false;
    }

    public void OnClickBuildBarrack()       // Button1 클릭 시 실행 (배럭)
    {
        SelectTower((int)TowerType.Barrack);
    }
    public void OnClickBuildArcher()        // Button2 클릭 시 실행 (아처)
    {
        SelectTower((int)TowerType.Archer);
    }
    public void OnClickBuildCatapult()      // Button3 클릭 시 실행 (투석기)
    {
        SelectTower((int)TowerType.Catapult);
    }
    public void OnClickBuildWizard()        // Button4 클릭 시 실행 (위자드)
    {
        SelectTower((int)TowerType.Wizard);
    }

    //-----------------------------------------------------------------------------
    // description    : 타워 선택시 한번 더 체크해줌 ((클릭)체크표시 띄어줌 -> (클릭)타워 짓기)
    //=============================================================================
    public void SelectTower(int index)
    {
        if (isCheck[index] == true)     // 체크상태
        {
            for (int i = 0; i < checkButton.Length; i++)
            {
                isCheck[i] = false;                         // 모두 다 false
            }

            checkButton[index].SetActive(false);            // 타워건설했으면 체크표시 해제
            ground.BuyTower(index);                         // 선택한 타워 건설
        }
        else                            // 체크가 안된상태
        {  
            for (int i = 0; i < checkButton.Length; i++)    
            {
                isCheck[i] = false;                         // 모두 false로 변경
                checkButton[i].SetActive(false);            // 모두 체크표시 해제
            }

            isCheck[index] = true;                          // 선택된 Index만 true               
            checkButton[index].SetActive(true);             // 선택된 Index만 체크표시
        }
    }

    //-----------------------------------------------------------------------------
    // description : 타워선택UI On, Ground를 멤버변수에 저장해줌
    //=============================================================================
    public void OnTowerSelectUI(Ground ground)       
    {                             
        this.ground = ground;     // 선택한 땅의 정보를 클래스내 멤버변수로 저장

        isActiveTowerSelectUI = true;
        gameObject.SetActive(isActiveTowerSelectUI);

        //※위치※ 윗줄 gameObject가 Active되어야 밑의 코드 실행가능(아니면 오류나옴)
        for (int i = 0; i < checkButton.Length; i++)    // UI On했으면 체크표시 다 끄도록함
        {
            isCheck[i] = false;                         // 모두 false로 변경
            checkButton[i].SetActive(false);            // 모두 체크표시 해제
        }
    }

    //-----------------------------------------------------------------------------
    // description    : 타워선택UI Off
    //=============================================================================
    public void OffTowerSelectUI()
    {
        isActiveTowerSelectUI = false;
        gameObject.SetActive(isActiveTowerSelectUI);
    }


}













// 썻다가 버린 코드들..
/*
-----------------------------------------------------------------------------
 DESCRIPTS    : 타워를 선택한 땅에 설치
=============================================================================
private void BuildTower(int towerIndex)
{
    GameObject tempTower;

    if (towerIndex == (int)TowerType.Wizard)           // 위자드 타워일때만 위치값 따로 보정하여 설치
    {
        tempTower = Instantiate(builtTower[towerIndex], groundPosition + new Vector3(0, 0.39f, 0), Quaternion.identity); // 타워를 ground 중앙에 위치하기 위한 y값 조정
    }
    else                                // 나머지 타워들은 위치값 똑같이 보정하여 설치
    {
        tempTower = Instantiate(builtTower[towerIndex], groundPosition + offsetTowerPosition, Quaternion.identity);      // 타워를 ground 중앙에 위치하기 위한 y값 조정
    }

    // 설치한 타워의 sortingOrder의 수정을 위함
    tempTower.GetComponent<SpriteRenderer>().sortingOrder = ground.gameObject.GetComponent<SpriteRenderer>().sortingOrder;

    ground.GetComponent<SpriteRenderer>().color = Color.white;  // 타워를 설치하면 땅을 원래 색으로 바꿈
    this.ground.isTowerBuild = true;                            // 땅에 타워가 건설되었다(true)
    OffTowerSelectUI();
}
*/