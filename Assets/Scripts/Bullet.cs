using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int damage = 50;
    [SerializeField] private GameObject hitEffect;
    void Start()
    {
        rb.velocity = transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Bullet collided with: " + collision.gameObject.name);

        int enemyLayer = LayerMask.NameToLayer("Enemy");
        if (collision.gameObject.layer == enemyLayer)
        {
            EnemyController enemy = collision.GetComponent<EnemyController>();
            if (enemy != null)
            {
               enemy.TakingDamage(damage);
            }
            EnemyPlantController plantEnemy = collision.GetComponent<EnemyPlantController>();
            if (plantEnemy != null)
            {
                plantEnemy.TakingDamage(damage);
            } 
        }
        Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}