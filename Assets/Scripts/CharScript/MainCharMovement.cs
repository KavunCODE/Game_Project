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

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    // Start is called before the first frame update


    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetKey(KeyCode.E))
        {
            animator.SetBool("punch", true);
            StartCoroutine(Cooldown(0.533f));
        }

        Flip();
    }

    private IEnumerator Cooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        animator.SetBool("punch", false);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
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
