using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageSwordAttack : MonoBehaviour
{
    [SerializeField]
    HealthBar healthBar;

    public Collider2D swordCollider;
    public float damage = 2f;
    Vector2 rightAttackOffset;

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

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.tag == "Enemy")
    //    {
    //        //damage to enemy
    //        DesertEnemy enemy = other.GetComponent<DesertEnemy>();
    //        DesertDamageableCharacter damageableCharacter = other.GetComponent<DesertDamageableCharacter>();

    //        if (enemy != null)
    //        {
    //            //enemy.Health -= damage;
    //            //enemy.SetHealthBar(enemy.Health);

    //            //khi b? ?�nh t?o l?c ??y ra nh�n v?t
    //            //Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;
    //            //Vector2 direction = (Vector2)(parentPosition - collision.gameObject.transform.position).normalized;
    //            //enemy.rigidbody.AddForce(direction * 500);
    //        }

    //        if (damageableCharacter != null)
    //        {
    //            damageableCharacter.Health -= damage;
    //        }
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D other = collision.collider;
        if (other.tag == "Enemy")
        {
            //damage to enemy
            MageDamageableCharacter damageableCharacter = other.GetComponent<MageDamageableCharacter>();

            if (damageableCharacter != null)
            {
                damageableCharacter.OnHit(damage);
            }
        }
    }

}