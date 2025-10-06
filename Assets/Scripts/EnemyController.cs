using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private EnemyDataSO data;
    [SerializeField] private int health = 3;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isMoving;
    private bool takingDamage;
    private bool playerAlive;
    private bool isDead;
    private Animator animator;
    private void Start()
    {
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
        animator.SetBool("isDead", isDead);
    }

    public void TakingDamage(Vector2 direction, int damageAmount)
    {
        if (!takingDamage)
        {
            health -= damageAmount;
            takingDamage = true;
            if (health <= 0)
            {
                isDead = true;
                isMoving = false;
            }
            else
            {
                Vector2 rebound = new Vector2(transform.position.x - direction.x, 0.5f).normalized;
                rb.AddForce(rebound * 10f, ForceMode2D.Impulse);
            }       
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

            movement = new Vector2(direction.x, 0);

            isMoving = true;
        }
        else
        {
            movement = Vector2.zero;
            isMoving = false;
        }
        if (!takingDamage)
            rb.MovePosition(rb.position + movement * data.speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 directionDamage = new Vector2(transform.position.x, 0);
            PlayerController playerScript = collision.gameObject.GetComponent<PlayerController>();

            playerScript.TakingDamage(directionDamage, 1);
            playerAlive = !playerScript.isDead;
            if (!playerAlive)
            {
                isMoving = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector2 directionDamage = new Vector2(collision.gameObject.transform.position.x, 0);
            TakingDamage(directionDamage, 1);
        }
    }
}


