using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpHeight = 16f;
    private bool isFacingRight = true;

    public Animator animator;
    private Rigidbody2D rb;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private bool isJumping = false;
    private bool isFalling = false;
    private bool isPunching = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            isJumping = true;
        }

        if (Input.GetKey(KeyCode.E) && !isPunching)
        {
            isPunching = true;
        }

        if (Input.GetKey(KeyCode.Space) && rb.velocity.y > 0f)
        {
            isJumping = true;
        }

        if (rb.velocity.y < 0f && !IsGrounded())
        {
            isFalling = true;
        }

        UpdateAnimations();
        Flip();
    }

        private void FixedUpdate()
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }

        private void UpdateAnimations()
        {
            animator.SetBool("jump", isJumping);
            animator.SetBool("fall", isFalling);            
            animator.SetBool("punch", isPunching);            

            isJumping = false;
            isFalling = false;
        }
        private void EndPunch()
        {
            isPunching = false;
            Debug.Log("Punch animation ended. isPunching set to false.");
    }

        private bool IsGrounded()
        {
            return Physics2D.OverlapCircle(groundCheck.position, 1.2f, groundLayer);
        }

        private void Flip()
        {
            if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
        }
}
