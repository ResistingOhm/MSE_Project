using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameClearUI : MonoBehaviour
{
    public GameObject gameClearPanel;
    public Button titleButton;

    private void Start(){
        gameClearPanel.SetActive(false);
        
        titleButton.onClick.AddListener(GoToTitleScene); 
    }
    public void GoToTitleScene(){

        Time.timeScale = 1f;
        SoundManager.soundManager.SetTitleBGM();
        SceneManager.LoadScene("Title");
    }
}
