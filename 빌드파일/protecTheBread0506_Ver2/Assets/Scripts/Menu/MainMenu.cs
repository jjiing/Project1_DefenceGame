using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingMenu;
    public GameObject storyMenu;
    public void OnClickStartGame()
    {
        Debug.Log("���� ����");
        storyMenu.SetActive(true);
    }
    public void OnClickSkip()
    {
        Debug.Log("��ŵ");
       
        AudioManager.instance.GoToStageMenu();
    }
    public void OnClickOpenSettingMenu()
    {
        Debug.Log("����");
        settingMenu.SetActive(true);

    }
    public void OnClickCloseSettingMenu()
    {
        Debug.Log("�ݱ�");
        settingMenu.SetActive(false);
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