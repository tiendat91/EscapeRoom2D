using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DesertDamageableCharacter : MonoBehaviour, IDamageable
{
    [SerializeField]
    HealthBar healthBar;

    Animator animator;
    public float health = 8f;
    public bool targetable = true;
    public bool disableSimulation = false;
    bool isAlive = true;
    public Rigidbody2D rb;
    Collider2D physicCollider;
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

    public bool Targetable
    {
        get { return targetable; }
        set
        {
            targetable = value;
            if (disableSimulation)
            {
                rb.simulated = false;
            }
            physicCollider.enabled = true;
        }
    }
    public bool Invincible { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        animator.SetBool("isAlive", isAlive);
        physicCollider = GetComponent<Collider2D>();
        healthBar.SetMaxHealth(health);
    }

    public void SetHealthBar(float healthX)
    {
        healthBar.SetHealth(healthX);
    }

    public void Defeated()
    {
        isAlive = false;
        animator.SetBool("isAlive", false);
        Targetable = false;
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        Health -= damage;

        rb.AddForce(knockback, ForceMode2D.Impulse);
    }

    public void OnHit(float damage)
    {
        Health -= damage;
    }

    public void OnObjectDestroyed()
    {
        Destroy(gameObject);
    }
}
