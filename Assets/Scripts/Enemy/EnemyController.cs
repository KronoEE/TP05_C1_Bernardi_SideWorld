using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private EnemyDataSO data;
    [SerializeField] private int health = 100;
    [SerializeField] private GameObject deathEffect;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isMoving;
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
    }

    public void TakingDamage(int damageAmount)
    {
            health -= damageAmount;
            if (health <= 0)
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

            movement = new Vector2(direction.x, 0);

            isMoving = true;
        }
        else
        {
            movement = Vector2.zero;
            isMoving = false;
        }
            rb.MovePosition(rb.position + movement * data.speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int playerLayer = LayerMask.NameToLayer("Player");

        if (collision.gameObject.layer == playerLayer)
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
    private void Die()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        rb.velocity = Vector2.zero;

        animator.SetBool("isDead", isDead);
        Destroy(gameObject, 0.35f);
    }
}


