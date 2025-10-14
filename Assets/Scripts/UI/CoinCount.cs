using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCount : MonoBehaviour
{
    public int coinCount;
    public Text coinText;
    void Update()
    {
        coinText.text = coinCount.ToString();
    }
}
