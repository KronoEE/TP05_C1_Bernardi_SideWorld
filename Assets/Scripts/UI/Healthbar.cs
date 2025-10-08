using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Image fillHealthBar;
    private PlayerController player;
    private float maxHealth;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        maxHealth = player.health;
    }


    void Update()
    {
        fillHealthBar.fillAmount = player.health / maxHealth;
    }
}
