using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Deathpanel : MonoBehaviour
{
    [SerializeField] Button restartBtn;
    [SerializeField] Button settingsBtn;
    [SerializeField] Button homeBtn;
    private void Awake()
    {
        restartBtn.onClick.AddListener(OnRestartClick);
        settingsBtn.onClick.AddListener(OnSettingsClicked);
        homeBtn.onClick.AddListener(OnHomeClicked);
    }

    private void OnDestroy()
    {
        restartBtn.onClick.RemoveAllListeners();
        settingsBtn.onClick.RemoveAllListeners();
        homeBtn.onClick.RemoveAllListeners();
    }

    private void OnRestartClick()
    {
        SceneManager.LoadScene("Level_01");
        Time.timeScale = 1;
    }

    private void OnSettingsClicked()
    {

    }

    private void OnHomeClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
