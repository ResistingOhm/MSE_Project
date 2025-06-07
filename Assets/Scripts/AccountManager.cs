using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class AccountManager : MonoBehaviour
{
    public TMP_InputField InputFieldID;         
    public TMP_InputField InputFieldPW;         
    public TMP_InputField InputFieldCheckPW;        
    public TMP_InputField InputFieldName;       

    public void OnClickSubmitButton()
    {
        
        if (InputFieldPW.text != InputFieldCheckPW.text)
        {
            Debug.LogWarning("비밀번호가 일치하지 않습니다.");
            return;
        }


        /*
        if (PlayerPrefs.HasKey(InputFieldID.text + "_Password"))
        {
            Debug.LogWarning("이미 존재하는 아이디입니다.");
            return;
        }

        PlayerPrefs.SetString(InputFieldID.text + "_Password", InputFieldPW.text);
        PlayerPrefs.SetString(InputFieldID.text + "_Name", InputFieldName.text);
        PlayerPrefs.Save();

        Debug.Log("회원가입 성공!");
        SceneManager.LoadScene("Login");
        */

        NetworkManager.apiManager.Register(InputFieldName.text, InputFieldID.text, InputFieldPW.text);
    }

    public void OnBackButtonClicked()
    {
        SceneManager.LoadScene("Login");
    }
}
