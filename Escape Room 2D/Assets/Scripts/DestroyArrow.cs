using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyArrow : MonoBehaviour
{
    public float timeBeforeDestroy;
    public float damage;

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, timeBeforeDestroy);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            //Deal damage to the enemy
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Health -= damage;
                enemy.SetHealthBar(enemy.Health);

                Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;
                Vector2 direction = (Vector2)(parentPosition - collision.gameObject.transform.position).normalized;
                enemy.GetComponent<Rigidbody2D>().AddForce(direction * 350);
            }
        }
        if (collision.gameObject.tag != "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
