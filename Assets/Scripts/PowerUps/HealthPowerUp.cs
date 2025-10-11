using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
   [SerializeField] private int healthToAdd = 3;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int playerLayer = LayerMask.NameToLayer("Player");

        if (collision.gameObject.layer == playerLayer)
        {
            PlayerController playerScript = collision.gameObject.GetComponent<PlayerController>();
            playerScript.health += playerScript.health + healthToAdd;
            Destroy(gameObject);
        }
    }
}
