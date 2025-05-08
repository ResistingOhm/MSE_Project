using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
     public GameObject creditPanel;

    public void OnClickStartButton()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void OnClickLeaderBoardButton()
    {
        SceneManager.LoadScene("LeaderBoard");
    }
    public void OnClickBackdButton()
    {
        SceneManager.LoadScene("Title");
    }
    public void OnClickLevelOneButton()
    {
        SceneManager.LoadScene("MapOne");
    }public void OnClickLevelTwoButton()
    {
        SceneManager.LoadScene("MapTwo");
    }
    void Start()
    {
    creditPanel.SetActive(false); // 씬 시작할 때 꺼줌
    }
    public void OnCreditClicked(){
          creditPanel.SetActive(true);
    }
    public void CloseCredit(){
        creditPanel.SetActive(false);
    }
    public void OnClickExitButton()
    {
        Application.Quit();
#if UNITY_EDITOR //에디터 안에서 실행될떄만 종료
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    
}
