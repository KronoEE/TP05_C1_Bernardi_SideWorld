using UnityEngine;
public class EnemyPlantController : MonoBehaviour
{
    [SerializeField] private Healthbar healthbar;
    [SerializeField] private Transform player;
    [SerializeField] private EnemyDataSO data;
    [SerializeField] private ParticleSystem deathEffect;

    private bool isAttacking;
    private bool playerAlive;
    private bool isDead;
    private int currentHealth;

    private Animator animator;
    private void Start()
    {
        currentHealth = data.maxHealth;
        healthbar.UpdateHealthBar(data.maxHealth, currentHealth);
        playerAlive = true;
        isDead = false;
        animator = GetComponent<Animator>();
    }
    public void TakingDamage(int damageAmount)
    {
            currentHealth -= damageAmount;
            healthbar.UpdateHealthBar(data.maxHealth, currentHealth);
            if (currentHealth <= 0)
            {
                isDead = true;
                Die();
            }
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
                FacePlayer(collision.transform);
                animator.SetBool("isInRange", isInRange);
                isAttacking = true;
                animator.SetBool("isAttacking", isAttacking);
                Vector2 directionDamage = new Vector2(transform.position.x, 0);
                PlayerController playerScript = collision.gameObject.GetComponent<PlayerController>();

                playerScript.TakingDamage(directionDamage, data.damageAmount);
                playerAlive = !playerScript.isDead;
                if (!playerAlive)
                {
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
    private void FacePlayer(Transform player)
    {
        if (player == null) return;

        if (player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public void EndAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", false);
        animator.SetBool("isInRange", false);
    }
    private void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        animator.SetBool("isDead", isDead);
        Destroy(gameObject, 0.5f);
    }
}


