using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DesertEnemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    HealthBar healthBar;

    Animator animator;
    public float health = 8f;
    bool isAlive = true;
    public Rigidbody2D rb;
    public float Health
    {
        set
        {
            if (value < health)
            {
                animator.SetTrigger("hit");
            }

            health = value;

            if (health <= 0)
            {
                Defeated();
            }
        }
        get { return health; }

    }

    public bool Targetable { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public bool Invincible { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        animator.SetBool("isAlive", isAlive);
        //healthBar.SetMaxHealth(health);
    }

    public void SetHealthBar(float healthX)
    {
        healthBar.SetHealth(healthX);
    }

    public void Defeated()
    {
        isAlive = false;
        animator.SetBool("isAlive", false);
    }

    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        throw new System.NotImplementedException();
    }

    public void OnHit(float damage)
    {
        throw new System.NotImplementedException();
    }

    public void OnObjectDestroyed()
    {
        throw new System.NotImplementedException();
    }
}
