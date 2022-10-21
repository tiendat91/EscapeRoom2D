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

    Animator animator;
    bool isAlive = true;

    [SerializeField]
    Rigidbody2D rigidbody;

    [SerializeField]
    GameObject bossPrefab;

    public DamageableCharacter damageableCharacter;

    public DetectionZone detectionZone;

    public float Health
    {
        set
        {
            if (value < health)
            {
                Debug.Log("Drop Blood");
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
        rigidbody = GetComponent<Rigidbody2D>();
        animator.SetBool("IsAlive", isAlive);
        healthBar.SetMaxHealth(health);

        damageableCharacter = GetComponent<DamageableCharacter>();
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

        if (damageableCharacter.Targetable && detectionZone.detectedObjs.Count > 0)
        {
            Vector2 direction = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;

            //Move towards detected object
            rigidbody.AddForce(direction * moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if (damageable != null)
        {
            //Offset for collision detection changes the direction where the force comes from
            Vector2 direction = (collider.transform.position - transform.position).normalized;

            //Knockback is in direction of swordCollider towards collider
            Vector2 knockback = direction * knockbackForce;

            //After making sure the collider has a script that implements IDamageble, we can run the OnHit implementation and pass
            //our Vector2 force
            damageable.OnHit(damage, knockback);
            Debug.Log("Bi Danh");
        }
    }


}
