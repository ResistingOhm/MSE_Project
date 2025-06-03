using UnityEngine;
using UnityEngine.UI;
using System;

public class PopupCheckConfirmEventArgs : EventArgs
{
    public UnityEngine.Events.UnityAction events;
    public string eventName;
}

public class PopupWindow : MonoBehaviour {

    public static PopupWindow instance;
    public static event EventHandler<PopupCheckConfirmEventArgs> ConfirmEvent;

    public enum MsgType { notice, error, warning, exit,Joke };

    public GameObject popupView;
    
    public Text m_MessageType;
    public Text m_Msg;
    public Button Btn_Exit;
    public Button Btn_OK;
    public Button Btn_Cancle;
    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }   
	// Use this for initialization
	void Start ()
    {
        popupView.SetActive(false);

        if(instance == null)
            instance = this;

        Btn_Exit.onClick.AddListener(CloseView);
        Btn_Cancle.onClick.AddListener(CloseView);
        ConfirmEvent += OnConfirmOnClick;

        if (transform.parent != null && transform.parent.GetComponent<Canvas>() != null)
            transform.SetSiblingIndex(transform.parent.childCount);
        else
            Debug.LogError("Popup Window 패키지가 Canvas 자식으로 설정되어있지 않습니다. Canvas 하위 자식으로 설정하세요.");
            
    }

    public void CloseView()
    {
        popupView.SetActive(false);
        Btn_OK.gameObject.SetActive(false);
        Btn_Cancle.gameObject.SetActive(false);
        Btn_OK.onClick.RemoveAllListeners();
    }

    public void PopupCheckWindowOpen(UnityEngine.Events.UnityAction action, string actionName, MsgType msgtype, string msg)
    {
        Btn_OK.gameObject.SetActive(true);
        Btn_Cancle.gameObject.SetActive(true);
        Btn_OK.onClick.AddListener(delegate { ConfirmEvent(this, new PopupCheckConfirmEventArgs() { events = action, eventName = actionName }); });
        PopupWindowOpen(msgtype, msg);
    }

    public void OnConfirmOnClick(object sender, PopupCheckConfirmEventArgs e)
    {
        e.events();
        Btn_OK.onClick.RemoveAllListeners();
        CloseView();
    }


	public void PopupWindowOpen(MsgType msgtype, string msg)
    {
        popupView.SetActive(true);

        if (msgtype == MsgType.error)
        {
            m_MessageType.text = "Error !";
            m_Msg.text = msg;
            m_MessageType.color = Color.red;
        }
        else if (msgtype == MsgType.warning)
        {
            m_MessageType.text = "Warning !";
            m_Msg.text = msg;
            m_MessageType.color = Color.yellow;
        }
        else if (msgtype == MsgType.exit)
        {
            m_MessageType.text = "EXIT";
            m_Msg.text = msg;
            m_MessageType.color = Color.white;
        }
        else if (msgtype == MsgType.Joke)
        {
            m_MessageType.text = "Joke!";
            m_Msg.text = msg;
            m_MessageType.color = Color.cyan;
        }
        else
        {
            m_MessageType.text = "Notice";
            m_Msg.text = msg;
            m_MessageType.color = Color.white;
        }
    }

}
