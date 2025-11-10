using System.Collections;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Healthbar healthbar;
    [SerializeField] private PlayerDataSO data;
    [SerializeField] private Animator animator;
    [SerializeField] private CoinManager coinManager;
    [Header("Panels")]
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject panelWin;

    AudioManager audioManager;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool takingDamage;
    private bool m_FacingRight = true;
    private int currentJumpForce;
    private int currentHealth;
    private bool bisAttacking;
    [Header("Public variables")]
    public bool attackCondition;
    public bool isDead;
    public int coins = 0;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Start()
    {
        currentHealth = data.maxHealth;
        currentJumpForce = data.maxJumpForce;
        healthbar.UpdateHealthBar(data.maxHealth, currentHealth);
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (!isDead)
        {
            if (!bisAttacking)
            {
                Movement();
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, data.lengthRayCast, data.layerMask);
                isGrounded = hit.collider != null;
                if (Input.GetKeyDown(data.jumpKey) && isGrounded && !takingDamage)
                {
                    audioManager.PlaySFX(audioManager.jumpSfx);
                    rb.AddForce(new Vector2(0f, currentJumpForce), ForceMode2D.Impulse);
                }
            }
            bool condition = !bisAttacking && isGrounded;
            attackCondition = condition;
            if (Input.GetKeyDown(data.attackKey) && attackCondition)
            {
                audioManager.PlaySFX(audioManager.ShootSfx);
                Attacking();
            }
        }
        animator.SetBool("isGrounded", isGrounded); 
        animator.SetBool("takingDamage", takingDamage);
        animator.SetBool("attacking", bisAttacking);
    }

    public void Movement()
    {
        float velocityX = Input.GetAxis("Horizontal") * Time.deltaTime * data.velocity;

        animator.SetFloat("Movement", velocityX * data.velocity);

        if (velocityX < 0 && m_FacingRight)
        {
            Flip();
        }
        if (velocityX > 0 && !m_FacingRight)
        {
            Flip();
        }

        Vector3 position = transform.position;

        if (!takingDamage)
            transform.position = new Vector3(velocityX + position.x, position.y, position.z);
    }

    public void TakingDamage(Vector2 direction, int damageAmount)
    {
        if (!takingDamage)
        {
            takingDamage = true;
            currentHealth -= damageAmount;
            healthbar.UpdateHealthBar(data.maxHealth, currentHealth);
            if (currentHealth <= 0)
            {
                audioManager.Stop();
                audioManager.PlaySFX(audioManager.LooseSfx);
                deathPanel.SetActive(true);
                animator.SetBool("isDead", isDead);
                isDead = true;
                Time.timeScale = 0;
            }
            if (!isDead)
            {
                Vector2 rebound = new Vector2(transform.position.x - direction.x, 0.5f).normalized;
                rb.AddForce(rebound * data.reboundForce, ForceMode2D.Impulse);
            }
        }   
    }

    public void DeactiveDamage()
    {
        takingDamage = false;
        rb.velocity = Vector2.zero;
    }

    public void Attacking()
    {
        bisAttacking = true;
    }

    public void DeactiveAttack()
    {
        animator.SetBool("attacking", !bisAttacking);
        bisAttacking = false;
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        transform.Rotate(0f, 180f, 0f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        int coinLayer = LayerMask.NameToLayer("Coin");

        if (other.gameObject.layer == coinLayer)
        {
            Destroy(other.gameObject);
            audioManager.PlaySFX(audioManager.coinsSfx);
            coinManager.coinCount++;

            if (coinManager.coinCount == coinManager.goalCoins)
            {
                audioManager.Stop();
                audioManager.PlaySFX(audioManager.WinSfx);
                panelWin.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
    public IEnumerator TemporaryJumpBoost()
    {
        currentJumpForce = currentJumpForce + data.jumpForceToAdd;
        yield return new WaitForSeconds(data.jumpBoostDuration);

        currentJumpForce = data.maxJumpForce;
    }
    public void HealthBoost()
    {
        currentHealth += data.maxHealth;
        healthbar.UpdateHealthBar(data.maxHealth, currentHealth);
        if (currentHealth > data.maxHealth)
        {
            currentHealth = data.maxHealth;
            healthbar.UpdateHealthBar(data.maxHealth, currentHealth);
        }
    }
}
