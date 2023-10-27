using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpellMage : MonoBehaviour
{
    public GameObject bulletPref;
    public Transform aimPoint;
    public float speed;

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 mouseWPos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 look = new Vector2(mouseWPos.x - transform.position.x, mouseWPos.y - transform.position.y);
        transform.up = look;
        if (Input.GetMouseButtonDown(0))
        {
            Shooting();
        }
    }
    //float TimeToUse = 0;
    //public float DelayTime = 1;

    public void UpDamage() //buff range, damage
    {
        bulletPref.GetComponent<DestroySpellMage>().damage = 1.5f;
        bulletPref.GetComponent<DestroySpellMage>().timeBeforeDestroy = 1.5f;

    }
    public void DownDamage()
    {
        bulletPref.GetComponent<DestroySpellMage>().damage = 0.5f;
        bulletPref.GetComponent<DestroySpellMage>().timeBeforeDestroy = 0.6f;
    }

    void Shooting()
    {
        //if (Time.time > TimeToUse)
        //{
        GameObject bullet = Instantiate(bulletPref, aimPoint.position, aimPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(aimPoint.up * speed, ForceMode2D.Impulse);
        //    TimeToUse = Time.time + DelayTime;
        //}
    }

}
