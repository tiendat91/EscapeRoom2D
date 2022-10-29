using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    HealthBar healthBar;

    public float health = 15;
    public float damage = 1;
    public float moveSpeed = 500f;
    public float knockbackForce = 100f;
    bool isAlive = true;
    Animator animator;
    Rigidbody2D rb;
    public DamageableCharacter damageableCharacter;
    [SerializeField]
    public DetectionZone detectionZone;
    SpriteRenderer spriteRenderer;

    public float Health
    {
        set
        {
            if (value < health)
            {
                animator.SetTrigger("hit");
            }

            health = value;
            SetHealthBar(value);

            if (health <= 0)
            {
                Defeated();
            }
        }
        get { return health; }

    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageableCharacter = GetComponent<DamageableCharacter>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator.SetBool("IsAlive", isAlive);
        SetMaxHealth();
    }

    void SetMaxHealth()
    {
        healthBar.SetMaxHealth(health);
    }

    public void SetHealthBar(float healthX)
    {
        healthBar.SetHealth(healthX);
    }
    public void Defeated()
    {
        isAlive = false;
        animator.SetBool("IsAlive", false);
    }

    public void RemoveEnemy()
    {
        Destroy(gameObject);
        Debug.Log("Destroyed");
    }

    private void FixedUpdate()
    {
        if (detectionZone.detectedObjs.Count > 0)
        {
            Vector2 direction = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;
            if (direction.x >= 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
            rb.AddForce(direction * moveSpeed * Time.deltaTime);
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Collider2D collider = collision.collider;
        DamageableCharacter damageable = collider.GetComponent<DamageableCharacter>();
        if (damageable != null)
        {
            //Offset for collision detection changes the direction where the force comes from
            Vector2 direction = (collider.transform.position - (transform.position) * -1).normalized;

            //Knockback is in direction of swordCollider towards collider
            Vector2 knockback = direction * knockbackForce;
            //After making sure the collider has a script that implements IDamageble, we can run the OnHit implementation and pass
            //our Vector2 force
            animator.SetTrigger("attack");
            damageable.OnHit(damage, knockback);
        }
    }
}
