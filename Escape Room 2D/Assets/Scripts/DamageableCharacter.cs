using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class DamageableCharacter : MonoBehaviour, IDamageable
{
    public GameObject healthText;
    public bool disableSimulation = false;
    public bool canTurnInvincible = false;
    public float invincibilityTime = 0.25f;


    Animator animator;
    Rigidbody2D rb;
    Collider2D physicsColider;
    bool isAlive = true;
    private float invincibleTimeElapsed = 0f;

    public float _health = 3;
    public bool _targetable = true;
    public bool _invincible = false;

    public float Health
    {
        set
        {
            //When health is dropped (new value less than old value), play hit animation and show damage taken as text
            if (value < _health)
            {
                animator.SetTrigger("hit");

                //Spawn damage text right above the character
                RectTransform textTransform = Instantiate(healthText).GetComponent<RectTransform>();
                textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);

                Canvas canvas = GameObject.FindObjectOfType<Canvas>();
                textTransform.SetParent(canvas.transform);

            }
            _health = value;

            if (_health <= 0)
            {
                animator.SetBool("isAlive", false);
                Targetable = false;
            }
        }
        get
        {
            return _health;
        }
    }
    public bool Targetable
    {
        get { return _targetable; }
        set
        {
            _targetable = value;
            if (disableSimulation)
            {
                rb.simulated = false;
            }

            physicsColider.enabled = value;
        }
    }
    public bool Invincible
    {
        get { return _invincible; }
        set
        {
            _invincible = value;

            if (_invincible == true)
            {
                invincibleTimeElapsed = 0f;
            }
        }
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        if (!Invincible)
        {
            Health -= damage;

            //apply force to the enemy 
            //Impulse for instantaneous forces
            rb.AddForce(knockback, ForceMode2D.Impulse);

            if (canTurnInvincible)
            {
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
                //Activate invincibility and timer
                Invincible = true;
            }
        }
    }

    public void OnObjectDestroyed()
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {
        animator = GetComponent<Animator>();

        animator.SetBool("isAlive", isAlive);
        rb = GetComponent<Rigidbody2D>();
        physicsColider = GetComponent<Collider2D>();

    }

    void Update()
    {

    }
}
