using UnityEngine;
public class HealthPowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int playerLayer = LayerMask.NameToLayer("Player");

        if (collision.gameObject.layer == playerLayer)
        {
            PlayerController playerScript = collision.gameObject.GetComponent<PlayerController>();
            playerScript.HealthBoost();
            Destroy(gameObject);
        }
    }
}
