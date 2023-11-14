using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject slashPrefab;
    [SerializeField] private Transform slashSpawnPoint;
    [SerializeField] private Transform weaponCollider;

    private Animator myAnimator;
    private HeroController heroController;
    private ActiveWeapon activeWeapon;
    private GameObject slashAnimation;
    private void Awake()
    {
        heroController = GetComponentInParent<HeroController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        myAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        MouseFollowWithOffset();
        EnableColliderAttack();
    }


    /// <summary>
    /// Swing sword animation & Instantiate sword slash
    /// </summary>
    public void Attack()
    {
        //Instantiate new sword slash
        if (heroController.CanAttackAgain)
        {
            //Swing sword animation
            heroController.SwordAttackAnimation();
            slashAnimation = Instantiate(slashPrefab, slashSpawnPoint.position, Quaternion.identity);
            slashAnimation.transform.parent = this.transform.parent;
            //Change weapon direction by hero direction
            if (heroController.FacingLeft)
            {
                slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                slashAnimation.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }

    /// <summary>
    /// Enable the object weapon collider when the character attacks
    /// </summary>
    public void EnableColliderAttack()
    {
        if (heroController.IsAttack)
        {
            weaponCollider.gameObject.SetActive(true);
        }
        else
        {
            weaponCollider.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Adjust weapon attack direction according to the mouse
    /// </summary>
    private void MouseFollowWithOffset()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(heroController.transform.position);
        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        if(mousePosition.x <= playerScreenPoint.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
