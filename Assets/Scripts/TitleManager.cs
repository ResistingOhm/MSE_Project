using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
     public GameObject settingsPanel;

    public void OnClickStartButton()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void OnClickExitButton()
    {
        Application.Quit();
#if UNITY_EDITOR //에디터 안에서 실행될떄만 종료
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    public void OnSettingButtonClicked(){
          settingsPanel.SetActive(true);
    }
}
