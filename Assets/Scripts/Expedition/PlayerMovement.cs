using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);

        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.x < 0) {
            transform.localScale = new Vector3(-1f, 1, 1);
        } else {
            transform.localScale = new Vector3(1f, 1, 1); ;
        }

        if (movement.magnitude > .1f) {
            animator.SetFloat("Last_Horizontal", movement.x);
            animator.SetFloat("Last_Vertical", movement.y);
        }
    }

    void FixedUpdate()
    {
        movement = movement.normalized;
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);


    }
}
