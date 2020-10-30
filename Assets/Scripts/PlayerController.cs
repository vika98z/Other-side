using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject
{
    [SerializeField]
    private float maxSpeed = 7;
    [SerializeField]
    private float jumpTakeOffSpeed = 7;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;
        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
            velocity.y = jumpTakeOffSpeed;
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
                velocity.y = velocity.y * 0.5f;
        }

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        //animator.SetBool("grounded", grounded);
        if (grounded)
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x));// maxSpeed);
        else
            animator.SetFloat("velocityX", 0);

        targetVelocity = move * maxSpeed;
    }
}