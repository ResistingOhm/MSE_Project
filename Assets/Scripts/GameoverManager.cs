using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GameoverManager : MonoBehaviour
{

    public GameObject gameoverPanel;
    public Button mainButton;
    public Button replayButton;

    private void Start(){
        gameoverPanel.SetActive(false);
        
        mainButton.onClick.AddListener(OnMainMenu);
        replayButton.onClick.AddListener(OnRetry);
    
    }
    public void OnRetry(){

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMainMenu(){

        Time.timeScale = 1f;
        SoundManager.soundManager.SetTitleBGM();
        SceneManager.LoadScene("MainScene");
    }
}
