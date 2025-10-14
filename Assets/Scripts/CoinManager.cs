using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public int coinCount = 0;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private GameObject panelWin;
    private void Update()
    {
        coinText.text = coinCount.ToString();
        if (coinCount == 28)
        {
            panelWin.SetActive(true);
        }
    }

}
