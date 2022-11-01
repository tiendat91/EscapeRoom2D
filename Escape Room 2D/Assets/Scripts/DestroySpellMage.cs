using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySpellMage : MonoBehaviour
{
    public float damage = 3f;
    public float timeBeforeDestroy = 3f;

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, timeBeforeDestroy);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("trung ");
    //    Collider2D other = collision.collider;
    //    if (other.tag == "Enemy")
    //    {
    //        //damage to enemy
    //        MageDamageableCharacter damageableCharacter = other.GetComponent<MageDamageableCharacter>();

    //        if (damageableCharacter != null)
    //        {
    //            Debug.Log("trung enemy");
    //            damageableCharacter.OnHit(damage);
    //        }
    //        Destroy(this.gameObject);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trung ");
        if (other.tag == "Enemy")
        {
            //damage to enemy
            MageDamageableCharacter damageableCharacter = other.GetComponent<MageDamageableCharacter>();

            if (damageableCharacter != null)
            {
                Debug.Log("trung enemy");
                damageableCharacter.OnHit(damage);
            }
            Destroy(this.gameObject);
        }
    }

}
