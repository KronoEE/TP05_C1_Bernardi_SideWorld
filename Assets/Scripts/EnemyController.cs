using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private EnemyDataSO data;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isMoving;
    private bool takingDamage;
    private Animator animator;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
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
        if(!takingDamage)
            rb.MovePosition(rb.position + movement * data.speed * Time.deltaTime);

        animator.SetBool("isMoving", isMoving);
    }

    public void ReceiveDamage(Vector2 direction, int damageAmount)
    {
        if (!takingDamage)
        {
            takingDamage = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 directionDamage = new Vector2(transform.position.x, 0);
            ReceiveDamage(directionDamage, 1);
        }
    }



}
