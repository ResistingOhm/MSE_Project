using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField InputFieldID;
    public TMP_InputField InputFieldPW; 

    public void OnSubmitButtonClicked(){
        string userID = InputFieldID.text;
        string userPW = InputFieldPW.text;

        /*
        if (!PlayerPrefs.HasKey(InputFieldID.text + "_Password"))
        {
            Debug.LogWarning("존재하지 않는 아이디입니다.");
            return;
        }

        string savedPassword = PlayerPrefs.GetString(InputFieldID.text +"_Password");
        if (InputFieldPW.text != savedPassword)
        {
            Debug.LogWarning("비밀번호가 틀립니다.");
            return;    
        }

        Debug.Log("로그인 성공!");
        SceneManager.LoadScene("MainScene"); 
        */
        NetworkManager.apiManager.LogIn(userID, userPW);
    }
    public void OnAccountButtonClicked(){
        SceneManager.LoadScene("Account");
    }
}
