using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{

    public float health = 15;
    public float damage = 1;
    public float moveSpeed = 500f;
    public float knockbackForce = 100f;

    //public static Enemy instance;
    //public int amountToPool = 10;
    //private List<GameObject> list = new List<GameObject>();
    //[SerializeField]
    //public GameObject enemy;

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
<<<<<<< Updated upstream
=======
        healthBar.SetMaxHealth(health);

        //for (int i = 0; i < amountToPool; i++)
        //{
        //    GameObject pool = Instantiate(enemy);
        //    pool.transform.parent = gameObject.transform;
        //    pool.SetActive(false);
        //    list.Add(pool);
        //}
    }

    //public GameObject GetGameObjectFromPool()
    //{
    //    foreach (GameObject pool in list)
    //    {
    //        if (!pool.activeInHierarchy)
    //        {
    //            return pool;
    //        }
    //    }
    //    return null;
    //}

    //private void SpawnEnemy()
    //{
    //    GameObject enemy = Enemy.instance.GetGameObjectFromPool();
    //    if (enemy != null)
    //    {
    //        enemy.transform.position = transform.position;
    //        enemy.SetActive(true);
    //    }
    //}

    public void SetHealthBar(float healthX)
    {
        healthBar.SetHealth(healthX);
>>>>>>> Stashed changes
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
