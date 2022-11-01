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
    public bool invincible = true;
    public bool disableSimulation = false;
    public bool canTurnInvincible = false;
    public float invincibilityTime = 0.25f;
    private float invincibleTimeElapsed = 0;
    bool isAlive = true;
    public Rigidbody2D rb;
    Collider2D physicCollider;

    float maxHealth;
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
    public bool Invincible
    {
        get
        {
            return invincible;
        }
        set
        {
            invincible = value;
            if (invincible)
            {
                invincibleTimeElapsed = 0f;
            }
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        animator.SetBool("isAlive", isAlive);
        physicCollider = GetComponent<Collider2D>();
        SetMaxHealth(health);
    }

    public void SetMaxHealth(float _maxHealth)
    {
        healthBar.SetMaxHealth(_maxHealth);
        maxHealth = _maxHealth;
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
        if (!Invincible)
        {
            Health -= damage;

            //Apply force to the slime
            //Impulse for instantaneous forces
            rb.AddForce(knockback, ForceMode2D.Impulse);

            if (canTurnInvincible)
            {
                //Activate Invincibility and timer
                Invincible = true;
            }
        }
    }

    public void OnHit(float damage)
    {
        if (!Invincible)
        {
            Health -= damage;

            if (canTurnInvincible)
            {
                //Activate Invincibility and timer
                Invincible = true;
            }

        }
    }

    public void OnObjectDestroyed()
    {
        throw new System.NotImplementedException();
    }

    public void FixedUpdate()
    {
        if (Invincible)
        {
            invincibleTimeElapsed += Time.deltaTime;

            if (invincibleTimeElapsed > invincibilityTime)
            {
                Invincible = false;
            }
        }
    }

    public void BuffBlood(float blood)
    {
        if (Health < maxHealth)
        {
            Health += blood;

        }
    }
}
