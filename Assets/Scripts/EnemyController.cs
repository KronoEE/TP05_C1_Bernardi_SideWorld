using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private EnemyDataSO data;

    private Rigidbody2D rb;
    private Vector2 movement;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < data.detectionRadius)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            movement = new Vector2(direction.x, 0);
        }
        else
        {
            movement = Vector2.zero;
        }
        rb.MovePosition(rb.position + movement * data.speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 directionDamage = new Vector2(transform.position.x, 0);
            collision.gameObject.GetComponent<PlayerController>().ReceiveDamage(directionDamage, 1);
        }
    }



}
