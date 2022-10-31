using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DesertEnemy : MonoBehaviour
{
    public float damage = 1f;
    public float knockbackForce = 10f;
    public float moveSpeed = 500f;
    public DetectionZone detectionZone;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;

    public GameObject coin;
    public GameObject bloodItem;
    public GameObject manaItem;
    public float numberOfCoin = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (detectionZone.detectedObjs.Count > 0)
        {
            Vector2 direction = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;
            if (direction.x >= 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
            //Move towards detected object
            rb.AddForce(direction * moveSpeed * Time.deltaTime);
            animator.SetBool("isMoving", true);
        } else
        {
            animator.SetBool("isMoving", false);
        }
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        //damage to player
    //        DesertDamageableCharacter damageableCharacter = other.GetComponent<DesertDamageableCharacter>();

    //        if (damageableCharacter != null)
    //        {
    //            //Offset for collision detection changes the direction where the force comes from
    //            Vector2 direction = (other.transform.position - transform.position).normalized;

    //            //Knockback is in direction of swordCollider towards collider
    //            Vector2 knockback = direction * knockbackForce;

    //            damageableCharacter.OnHit(damage);
    //        }
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D other = collision.collider;

        if (other.tag == "Player")
        {
            if (other.name == "Soldier")
            {
                //damage to player
                DesertDamageableCharacter damageableCharacter = other.GetComponent<DesertDamageableCharacter>();

                if (damageableCharacter != null)
                {
                    //Offset for collision detection changes the direction where the force comes from
                    Vector2 direction = (other.transform.position - transform.position).normalized;

                    //Knockback is in direction of swordCollider towards collider
                    Vector2 knockback = direction * knockbackForce;

                    animator.SetTrigger("attack");
                    damageableCharacter.OnHit(damage, knockback);
                }
            }
            else if (other.name == "Hero")
            {
                //damage to player
                DamageableCharacter damageableCharacter = other.GetComponent<DamageableCharacter>();

                if (damageableCharacter != null)
                {
                    //Offset for collision detection changes the direction where the force comes from
                    Vector2 direction = (other.transform.position - transform.position).normalized;

                    //Knockback is in direction of swordCollider towards collider
                    Vector2 knockback = direction * knockbackForce;

                    animator.SetTrigger("attack");
                    damageableCharacter.OnHit(damage, knockback);
                }
            } else
            {
                //Mage
            }
        }
    }

    public void OnObjectDestroyed()
    {
        Destroy(gameObject);

        //Tao vang
        Vector2 spawnPos = transform.position;
        int percentDropItem = Random.Range(0, 100);
        for (int i = 0; i < numberOfCoin; i++)
        {
            spawnPos += Random.insideUnitCircle.normalized * 0.15f;
            Instantiate(coin, spawnPos, Quaternion.identity);
            Debug.Log("Tao vang");
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
}
