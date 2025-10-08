using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerDataSO data;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] public int health = 3;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    private bool isGrounded;
    private bool takingDamage;
    private bool attacking;
    private bool m_FacingRight = true;

    public bool isDead;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (!isDead)
        {
            if (!attacking)
            {
                Movement();
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, data.lengthRayCast, layerMask);
                isGrounded = hit.collider != null;
                if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !takingDamage)
                {
                    rb.AddForce(new Vector2(0f, data.jumpForce), ForceMode2D.Impulse);
                }
            }
            if (Input.GetKeyDown(KeyCode.Mouse0) && !attacking && isGrounded)
            {
                Attacking();
            }
        }
        animator.SetBool("isGrounded", isGrounded); 
        animator.SetBool("takingDamage", takingDamage);
        animator.SetBool("attacking", attacking);
        animator.SetBool("isDead", isDead);
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
                isDead = true;
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
        attacking = true;
    }

    public void DeactiveAttack()
    {
        attacking = false;
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}
