using UnityEngine;
public class DeadZone : MonoBehaviour
{
    [SerializeField] private GameObject deathPanel;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
         int playerLayer = LayerMask.NameToLayer("Player");
         if (collision.gameObject.layer == playerLayer)
            {
                audioManager.Stop();
                audioManager.PlaySFX(audioManager.LooseSfx);
                Destroy(collision.gameObject);
                deathPanel.SetActive(true);
                Time.timeScale = 0;
            }
    }
}
