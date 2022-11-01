using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSkill : MonoBehaviour
{
    public GameObject bulletPref;
    public Transform aimPoint;

    public float speed;

    public bool isFiring = true;
    public float timeBetweenShots;
    private float nextFireTime = 0f;

    // update is called once per frame
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

    public void UpDamage() //buff range, damage
    {
        bulletPref.GetComponent<DestroyArrow>().damage = 1.5f;
        bulletPref.GetComponent<DestroyArrow>().timeBeforeDestroy = 1.5f;

    }
    public void DownDamage()
    {
        bulletPref.GetComponent<DestroyArrow>().damage = 0.5f;
        bulletPref.GetComponent<DestroyArrow>().timeBeforeDestroy = 0.6f;
    }

    private bool CanFire
    {
        get { return Time.time > nextFireTime; }
    }

    void Shooting()
    {
        GameObject bullet = Instantiate(bulletPref, aimPoint.position, aimPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(aimPoint.up * speed, ForceMode2D.Impulse);
    }
}
