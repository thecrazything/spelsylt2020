using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;
    public GameObject interactor;

    Vector2 movement;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);

        animator.SetFloat("Speed", movement.sqrMagnitude);

        // Make sure the player only moves in one direction at a time
        if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
        {
            movement.x = 0;
        }
        else
        {
            movement.y = 0;
        }

        // Save last movement as current player rotation
        if (movement.magnitude > .1f) {
            animator.SetFloat("Last_Horizontal", movement.x);
            animator.SetFloat("Last_Vertical", movement.y);

            UpdateInteractor(movement);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    // Moves the interactor component to match player rotation
    void UpdateInteractor(Vector2 dir)
    {
        if (dir.x > 0) {
            interactor.transform.localPosition = new Vector3(.4f, 0, 0);
        } else if (dir.x < 0) {
            interactor.transform.localPosition = new Vector3(-.4f, 0, 0);
        }
        else if (dir.y > 0) {
            interactor.transform.localPosition = new Vector3(0, .45f, 0);

        } else {
            interactor.transform.localPosition = new Vector3(0, -.45f, 0);
        }
    }
}
