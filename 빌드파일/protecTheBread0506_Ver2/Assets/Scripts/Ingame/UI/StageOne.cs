using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageOne : MonoBehaviour
{
    public GameObject settingMenu;
    public GameObject completeMenu;
    public GameObject loseMenu;


    public void OnClickOpenSettingMenu()
    {
        Debug.Log("����");
        settingMenu.SetActive(true);
        if(settingMenu.activeSelf == true)
        {
            Debug.Log("����");
            Time.timeScale = 0f;
        }

    }
    public void OnClickCloseSettingMenu()
    {
        Debug.Log("�ݱ�");
        settingMenu.SetActive(false);
        if (settingMenu.activeSelf == false)
        {
            Debug.Log("���");
            Time.timeScale = 1f;
        }
    }

    public void OnClickGameReset()
    {
        Debug.Log("�ٽ��ϱ�");
        Time.timeScale = 1f;
        settingMenu.SetActive(false);
        SceneManager.LoadScene("Stage1");

    }

    public void OnClickGameReturn()
    {
        Debug.Log("�������� ����");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Stage Menu");

    }

    public void OnClickCloseComplete()
    {
        completeMenu.SetActive(false);
        Time.timeScale = 0f;
    }
   
    public void OnClickCloseLose()
    {
        loseMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    public void OnClickQuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
