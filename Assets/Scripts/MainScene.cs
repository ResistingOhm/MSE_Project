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
        UserDataManager.udm.SelectedLevel(true);
        SceneManager.LoadScene("GameScene");
    }
    public void OnClickLevelTwoButton()
    {
        UserDataManager.udm.SelectedLevel(false);
        SceneManager.LoadScene("GameScene");
    }
}
