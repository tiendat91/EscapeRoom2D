using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public Rigidbody2D rigidbody;


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


}
