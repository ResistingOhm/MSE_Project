using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnClickBackdButton()
    {
        SceneManager.LoadScene("Title");
    }
    public void OnClickLevelOneButton()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void OnClickLevelTwoButton()
    {
        SceneManager.LoadScene("GameScene");
    }
}
