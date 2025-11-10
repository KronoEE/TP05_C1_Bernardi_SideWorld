using UnityEngine;
public class EnemyController : MonoBehaviour
{
    [SerializeField] private Healthbar healthbar;
    [SerializeField] private Transform player;
    [SerializeField] private EnemyDataSO data;
    [SerializeField] private ParticleSystem deathEffect;

    private int currentHealth;
    private float movementX;
    private bool isAttacking;
    private bool isMoving;
    private bool playerAlive;
    private bool isDead;
    private Rigidbody2D rb;
    private Animator animator;
    private void Start()
    {
        currentHealth = data.maxHealth;
        healthbar.UpdateHealthBar(data.maxHealth, currentHealth);
        playerAlive = true;
        isDead = false;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (playerAlive && !isDead)
        {
            Movement();
        }
        animator.SetBool("isMoving", isMoving);
    }
    public void TakingDamage(int damageAmount)
    {
            currentHealth -= damageAmount;
            healthbar.UpdateHealthBar(data.maxHealth, currentHealth);
            if (currentHealth <= 0)
            {
                isDead = true;
                isMoving = false;
                Die();
            }
    }
    private void Movement()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < data.detectionRadius)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            movementX = direction.x;
            isMoving = true;
        }
        else
        {
            movementX = 0;
            isMoving = false;
        }
            rb.velocity = new Vector2(movementX * data.speed, rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        int playerLayer = LayerMask.NameToLayer("Player");

        if (collision.gameObject.layer == playerLayer)
        {
            float distance = Vector2.Distance(transform.position, collision.transform.position);
            bool isInRange = distance <= data.attackRange;
            if (isInRange)
            {
                animator.SetBool("isInRange", isInRange);
                isAttacking = true;
                animator.SetBool("isAttacking", isAttacking);
                Vector2 directionDamage = new Vector2(transform.position.x, 0);
                PlayerController playerScript = collision.gameObject.GetComponent<PlayerController>();

                playerScript.TakingDamage(directionDamage, data.damageAmount);
                playerAlive = !playerScript.isDead;
                if (!playerAlive)
                {
                    isMoving = false;
                    isInRange = false;
                }
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isAttacking = false;
        animator.SetBool("isAttacking", false);
        animator.SetBool("isInRange", false);
    }
    private void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        rb.velocity = Vector2.zero;

        animator.SetBool("isDead", isDead);
        Destroy(gameObject, 0.5f);
    }
}


