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

    public static event Action<bioMuscleEnemy> OnbioMuscleEnemyKilled;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float health, maxHealth = 3f;
    [SerializeField] private float moveSpeed = 6f;
    private bool isFacingLeft = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        target = GameObject.Find("MainChar").transform;
    }

    private void Update()
    {
        if (target)
        {

            Vector3 direction = (target.position - transform.position).normalized;

            rb.velocity = direction * moveSpeed;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Destroy(gameObject);
            OnbioMuscleEnemyKilled?.Invoke(this);
        }
    }
}


