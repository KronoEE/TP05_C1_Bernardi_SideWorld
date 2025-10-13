using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private int coinsToAdd = 1;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int playerLayer = LayerMask.NameToLayer("Player");

        if (collision.gameObject.layer == playerLayer)
        {
            audioManager.PlaySFX(audioManager.coinsSfx);
            PlayerController playerScript = collision.gameObject.GetComponent<PlayerController>();
            playerScript.coins += playerScript.coins + coinsToAdd;
            Destroy(gameObject);
        }
    }
}
