using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageDamageableCharacter : MonoBehaviour, IDamageable
{
    [SerializeField]
    HealthBar healthBar;

    public float health;
    public bool targetable = true;
    public bool invincible = true;
    public bool disableSimulation = false;
    public bool canTurnInvincible = false;
    public float invincibilityTime = 0.25f;
    private float invincibleTimeElapsed = 0;
    bool isAlive = true;
    Animator animator;
    public Rigidbody2D rb;
    Collider2D physicCollider;
    float MaxHealth;
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

    public void BuffBlood(float blood)
    {
        Debug.Log("Using blood item");
        if (Health < MaxHealth)
        {
            Health += blood;

        }
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
        animator.SetBool("IsAlive", isAlive);
        physicCollider = GetComponent<Collider2D>();
    }

    public void SetHealthBar(float healthX)
    {
        healthBar.SetHealth(healthX);
    }

    public void Defeated()
    {
        isAlive = false;
        animator.SetBool("IsAlive", false);
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
            Debug.Log("hit run");
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

    public void SetMaxHealth(float _health)
    {
        healthBar.SetMaxHealth(_health);
        MaxHealth = _health;
    }

    void Update()
    {

    }
}