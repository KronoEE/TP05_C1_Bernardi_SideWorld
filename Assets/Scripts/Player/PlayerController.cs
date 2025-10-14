using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Video;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerDataSO data;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private CoinManager coinManager;

    AudioManager audioManager;

    private bool isGrounded;
    private bool takingDamage;
    private bool m_FacingRight = true;
    public bool attackCondition;
    public bool bisAttacking;
    public  bool isDead;

    public int coins = 0;
    public float jumpForce = 10f;
    public int health = 3;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (!isDead)
        {
            if (!bisAttacking)
            {
                Movement();
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, data.lengthRayCast, layerMask);
                isGrounded = hit.collider != null;
                if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !takingDamage)
                {
                    audioManager.PlaySFX(audioManager.jumpSfx);
                    rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                }
            }
            bool condition = !bisAttacking && isGrounded;
            attackCondition = condition;
            if (Input.GetKeyDown(KeyCode.E) && attackCondition)
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
            health -= damageAmount;
            if (health <= 0)
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
        }
    }
}
