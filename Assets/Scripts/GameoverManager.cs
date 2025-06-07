using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GameoverManager : MonoBehaviour
{

    public GameObject gameoverPanel;
    public Text scoreText;
    public Button mainButton;
    public Button replayButton;

    private void Start(){
        gameoverPanel.SetActive(false);
        
        mainButton.onClick.AddListener(OnMainMenu);
        replayButton.onClick.AddListener(OnRetry);
    
    }
    public void ShowGameOver(){
        if (gameoverPanel != null){
            gameoverPanel.SetActive(true);
        }
        
        Time.timeScale = 0f;
        scoreText.text = " 100 "; //server 
    }
    public void OnRetry(){

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMainMenu(){

        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }
}
