using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuSystem : MonoBehaviour
{
    [SerializeField] Button playBtn;
    [SerializeField] Button SettingsBtn;
    [SerializeField] Button quitBtn;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] Button SettingsBackBtn;
    private void Awake()
    {
        playBtn.onClick.AddListener(OnPlayClicked);
        quitBtn.onClick.AddListener(OnQuitClicked);
        SettingsBtn.onClick.AddListener(OnSettingsClicked);
        SettingsBackBtn.onClick.AddListener(OnSettingsBackButton);
    }
    private void OnDestroy()
    {
        playBtn.onClick.RemoveAllListeners();
        quitBtn.onClick.RemoveAllListeners();
        SettingsBtn.onClick.RemoveAllListeners();
        SettingsBackBtn.onClick.RemoveAllListeners();
    }
    void OnPlayClicked()
    {
        SceneManager.LoadScene("Level_01");
        Time.timeScale = 1;
    }

    void OnQuitClicked()
    {
        Application.Quit();
    }

    void OnSettingsClicked()
    {
        settingsPanel.SetActive(true);
    }

    void OnSettingsBackButton()
    {
        settingsPanel.SetActive(false);
    }
}
