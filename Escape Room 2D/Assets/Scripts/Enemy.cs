using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{

    public float health = 5;
    Animator animator;
    bool isAlive = true;
    public Rigidbody2D rigidbody;
    public float Health
    {
        set
        {
            if (value < health) //value gi?m xu?ng chuy?n tr?ng thái hit
            {
                animator.SetTrigger("hit");
            }

            health = value;

            if (health < 0)
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
