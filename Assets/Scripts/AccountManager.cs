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
            PopupWindow.instance.PopupWindowOpen(
                PopupWindow.MsgType.warning,"This Password does not correct. Please check password."
            );
            // Debug.LogWarning("check the password");
            return;
        }

        NetworkManager.apiManager.Register(InputFieldName.text, InputFieldID.text, InputFieldPW.text);
    }

    public void OnBackButtonClicked()
    {
        SceneManager.LoadScene("Login");
    }
}
