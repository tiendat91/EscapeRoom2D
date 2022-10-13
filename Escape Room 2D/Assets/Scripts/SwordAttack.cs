using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    [SerializeField]
    HealthBar healthBar;

    public Collider2D swordCollider;
    Vector2 rightAttackOffset;
    public float damage = 3;
    private void Start()
    {
        rightAttackOffset = transform.position;
    }

    public void AttackRight()
    {
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
    }

    public void AttackLeft()
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            //Deal damage to the enemy
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Health -= damage;
                Debug.Log("Hit");

                //khi b? ?ánh t?o l?c ??y ra nhân v?t
                Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;
                Vector2 direction = (Vector2)(parentPosition - collision.gameObject.transform.position).normalized;
                enemy.GetComponent<Rigidbody2D>().AddForce(direction * 500);
            }
        }
    }
}
