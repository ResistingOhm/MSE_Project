using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsPanel;
    public Slider volumeSlider;
    public Button logoutButton;
    public Button quitButton;
    public Button closeButton;

    private void Start()
    {
        settingsPanel.SetActive(false);

        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        AudioListener.volume = volumeSlider.value;
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

        logoutButton.onClick.AddListener(OnLogout);
        quitButton.onClick.AddListener(OnQuit);
        closeButton.onClick.AddListener(CloseSettings);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            settingsPanel.SetActive(!settingsPanel.activeSelf);
        }
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("Volume", value); // 볼륨 저장
        PlayerPrefs.Save();
    }

    public void OnLogout()
    {
        SceneManager.LoadScene("Login");
    }

    public void OnQuit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
