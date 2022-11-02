using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JungleEnemy : MonoBehaviour
{
    [SerializeField]
    HealthBar healthBar;

    public float health = 15;
    public float damage = 1f;
    public float moveSpeed = 500f;
    public float knockbackForce = 10f;
    bool isAlive = true;

    Rigidbody2D rb;
    Animator animator;
    public MageDamageableCharacter mageDamageableCharacter;
    public DetectionZone detectionZone;
    SpriteRenderer spriteRenderer;
    public GameObject coin;
    public GameObject bloodItem;
    public GameObject manaItem;
    public float numberOfCoin = 1;

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
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mageDamageableCharacter = GetComponent<MageDamageableCharacter>();
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

        //Tao vang
        Vector2 spawnPos = transform.position;
        int percentDropItem = Random.Range(0, 100);
        for (int i = 0; i < numberOfCoin; i++)
        {
            spawnPos += Random.insideUnitCircle.normalized * 0.15f;
            Instantiate(coin, spawnPos, Quaternion.identity);
        }
        if (percentDropItem > 0 && percentDropItem <= 10) //Ti le 10% ra mau
        {
            spawnPos += Random.insideUnitCircle.normalized * 0.15f;
            Instantiate(bloodItem, spawnPos, Quaternion.identity);
        }
        if (percentDropItem > 10 && percentDropItem <= 35) //Ti le 25% ra mana
        {
            spawnPos += Random.insideUnitCircle.normalized * 0.15f;
            Instantiate(manaItem, spawnPos, Quaternion.identity);
        }
    }

    private void FixedUpdate()
    {
        if (detectionZone.detectedObjs.Count > 0)
        {
            Vector2 direction = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;
            if (direction.x >= 0)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
            //Move towards detected object
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
        Collider2D other = collision.collider;

        if (other.tag == "Player")
        {
            MageDamageableCharacter mageDamageableCharacter = other.GetComponent<MageDamageableCharacter>();
            if (mageDamageableCharacter != null)
            {
                Vector2 direction = (other.transform.position - transform.position).normalized;
                Vector2 knockback = direction * knockbackForce;

                animator.SetTrigger("attack");
                mageDamageableCharacter.OnHit(damage, knockback);
            }
        }
    }
}

