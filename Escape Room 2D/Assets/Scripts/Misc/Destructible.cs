using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private GameObject destroyVFX;

    private Animator animator;
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<DamageSource>())
        {
            //Check if gameObject have animation or VFX (ex: barrel, bush,..)
            if (animator)
            {
                animator.SetTrigger("destroy");
            }else if (destroyVFX)
            {
                Instantiate(destroyVFX, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
