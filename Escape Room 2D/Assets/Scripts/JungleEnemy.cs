using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JungleEnemy : MonoBehaviour
{
    public float damage = 1f;
    public float knockbackForce = 10f;
    public float moveSpeed = 500f;
    public DetectionZone detectionZone;
    Rigidbody2D rb;
    Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (detectionZone.detectedObjs.Count > 0)
        {
            Vector2 direction = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;

            //Move towards detected object
            rb.AddForce(direction * moveSpeed * Time.deltaTime);
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
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
            //damage to player
            MageDamageableCharacter damageableCharacter = other.GetComponent<MageDamageableCharacter>();

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
    }
}

