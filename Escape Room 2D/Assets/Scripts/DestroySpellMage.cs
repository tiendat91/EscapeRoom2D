using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySpellMage : MonoBehaviour
{
    public float timeBeforeDestroy = 3f;

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, timeBeforeDestroy);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
