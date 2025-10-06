using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerDataSO data;
    [SerializeField] private LayerMask layerMask;

    private bool isGrounded;
    private bool takingDamage;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float velocityX = Input.GetAxis("Horizontal") * Time.deltaTime * data.velocity;

        animator.SetFloat("Movement", velocityX * data.velocity);

        if (velocityX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (velocityX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        Vector3 position = transform.position;

        if (!takingDamage)
        transform.position = new Vector3(velocityX + position.x, position.y, position.z);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, data.lengthRayCast, layerMask);
        isGrounded = hit.collider != null;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !takingDamage)
        {
            rb.AddForce(new Vector2(0f, data.jumpForce), ForceMode2D.Impulse);
        }

        animator.SetBool("isGrounded", isGrounded); 
        animator.SetBool("takingDamage", takingDamage);
    }

    public void ReceiveDamage(Vector2 direction, int damageAmount)
    {
        if (!takingDamage)
        {
        takingDamage = true;
        Vector2 rebound = new Vector2(transform.position.x - direction.x, 1).normalized;
        rb.AddForce(rebound * data.ReboundForce, ForceMode2D.Impulse);
        }   
    }

    public void DeactiveDamage()
    {
        takingDamage = false;
    }

}
