using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PhysicsObject
{
    [SerializeField]
    private float maxSpeed = 7;
    [SerializeField]
    private BoxCollider2D triggerCollider;
    private Animator animator;
    private bool run = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (run)
        {
            Vector2 move = Vector2.zero;
            move.x = -1;
            targetVelocity = move * maxSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
            Destroy(gameObject);
        if (collision.gameObject.tag == "Player")
            animator.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            run = true;
            triggerCollider.enabled = false;
        }
    }
}
