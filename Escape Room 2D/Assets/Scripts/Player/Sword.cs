using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slashPrefab;
    [SerializeField] private Transform slashSpawnPoint;
    private PlayerControls playerControls;
    private Animator myAnimator;
    private HeroController heroController;
    private ActiveWeapon activeWeapon;
    private GameObject slashAnimation;
    private void Awake()
    {
        heroController = GetComponentInParent<HeroController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        myAnimator = GetComponent<Animator>();
        playerControls = new PlayerControls();
    }
    private void Update()
    {
        MouseFollowWithOffset();
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void Start()
    {
        playerControls.Combat.Attack.started += _ => Attack();
    }
    private void Attack()
    {
        //Get player facing direction
        if (heroController.FacingLeft)
        {
            slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            slashAnimation.GetComponent<SpriteRenderer>().flipX = false;
        }
        myAnimator.SetTrigger("attack");
        slashAnimation = Instantiate(slashPrefab, slashSpawnPoint.position, Quaternion.identity);
        slashAnimation.transform.parent = this.transform.parent;
    }
    private void MouseFollowWithOffset()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(heroController.transform.position);

        float angle = Mathf.Atan2(Input.mousePosition.y, Input.mousePosition.x) * Mathf.Rad2Deg *2f;
        if(mousePosition.x <= playerScreenPoint.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
