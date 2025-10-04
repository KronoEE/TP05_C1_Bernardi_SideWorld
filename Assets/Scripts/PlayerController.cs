using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float velocity = 10.0f;
    [SerializeField] private Animator animator;

    void Update()
    {
        float velocityX = Input.GetAxis("Horizontal") * Time.deltaTime * velocity;

        animator.SetFloat("Movement", velocityX * velocity);

        if (velocityX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (velocityX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        Vector3 position = transform.position;

        transform.position = new Vector3(velocityX + position.x, position.y, position.z);
    }
}
