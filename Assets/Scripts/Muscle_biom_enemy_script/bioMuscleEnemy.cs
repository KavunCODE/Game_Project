using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bioMuscleEnemy : MonoBehaviour
{
    private Transform target;
    private Vector2 moveDir;
    public Animator animator;
    private Rigidbody2D rb;
    private bool isFacingRight = true;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }


    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float moveSpeed = 4f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>(); 
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Flip();
    }
    private void FixedUpdate()
    {
        if (target != null)
        {
            // Calculate the direction from the enemy to the player
            Vector2 direction = (target.position - transform.position).normalized;

            // Calculate the new velocity based on the direction and desired speed
            Vector2 newVelocity = direction * moveSpeed;

            // Clamp the velocity magnitude to the maximum speed
            if (newVelocity.magnitude > moveSpeed || newVelocity.magnitude < moveSpeed)
            {
                newVelocity = newVelocity.normalized * moveSpeed;
            }

            // Set the new velocity
            rb.velocity = new Vector2(newVelocity.x, rb.velocity.y);
        }

    }

    private void Flip()
    {
        // Get the horizontal velocity
        float horizontalVelocity = rb.velocity.x;

        // Check the sign of the horizontal velocity to determine the direction
        if ((horizontalVelocity > 0 && !isFacingRight) || (horizontalVelocity < 0 && isFacingRight))
        {
            // Toggle the facing direction
            isFacingRight = !isFacingRight;

            // Flip the character's sprite
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;

            Debug.Log("Flipped");
        }
    }

}




