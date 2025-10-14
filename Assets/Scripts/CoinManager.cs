using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private GameObject panelWin;
    public int coinCount = 0;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GetComponent<AudioManager>();
    }
    private void Update()
    {
        coinText.text = coinCount.ToString();
        if (coinCount == 28)
        {
            audioManager.PlaySFX(audioManager.WinSfx);
            panelWin.SetActive(true);
        }
    }

}
