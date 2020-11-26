using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintModifier = 1.5f;

    public Rigidbody2D rb;
    public Animator animator;
    public GameObject interactor;
    public bool iSprinting { get; private set; } = false;
    public bool isMoving { get { return movement != null && movement.magnitude > .1f; } }

    Vector2 movement;
    bool _frozen;

    void Update()
    {
        if (_frozen)
        {
            movement.x = 0;
            movement.y = 0;
        }
        else
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }

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

        iSprinting = Input.GetAxisRaw("Sprint") >= 0.5f; ;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * (iSprinting ? sprintModifier : 1f) * Time.fixedDeltaTime);
    }

    // Moves the interactor component to match player rotation
    void UpdateInteractor(Vector2 dir)
    {
        if (dir.x > 0) {
            interactor.transform.rotation = Quaternion.Euler(0, 0, 90);
        } else if (dir.x < 0) {
            interactor.transform.rotation = Quaternion.Euler(0, 0, 270);
        }
        else if (dir.y > 0) {
            interactor.transform.rotation = Quaternion.Euler(0, 0, 180);

        } else {
            interactor.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void SetFrozen(bool value)
    {
        _frozen = value;
    }

    public Vector2 GetMovement()
    {
        return movement;
    }

    public bool isRunning()
    {
        return iSprinting;
    }
}
