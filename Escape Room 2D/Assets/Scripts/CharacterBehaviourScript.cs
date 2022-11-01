using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CharacterBehaviourScript : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI bloodItem;
    [SerializeField]
    TextMeshProUGUI manaItem;
    [SerializeField]
    TextMeshProUGUI keyItem;
    [SerializeField]
    TextMeshProUGUI coin;
    [SerializeField]
    ManaBar ManaBar;
    [SerializeField]
    TextMeshProUGUI coinShop;
    [SerializeField]
    ArrowSkill arrowSkill;


    int countBloodItem = 0;
    int countManaItem = 0;
    public int countKeyItem = 0;
    int countCoin = 20; //test
    public float TimeDisplayText = 3;
    public bool TimerOnText = false;

    public float _health;

    bool IsMoving
    {
        set
        {
            isMoving = value;
            animator.SetBool("IsMoving", isMoving);
        }
    }


    public float moveSpeed = 1.2f;
    public float maxSpeed = 8f;
    public float idleFriction = 0.9f;
    public float collisionOffset = 0.05f;

    float TimeLeft;
    public bool TimerOn = false;
    bool inRangeOpenChest;
    bool turnOffSkill;


    public ContactFilter2D movementFilter;
    public SwordAttack swordAttack;
    bool isMoving = false;

    Vector2 movementInput = Vector2.zero;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    SpriteRenderer spriteRenderer;
    Animator animator;
    public DamageableCharacter damageableCharacter;

    bool canMove = true;

    void Start()
    {
        TimerOnText = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        damageableCharacter = GetComponent<DamageableCharacter>();
        SetHealthForCharacter();

        turnOffSkill = false;
    }
    void SetHealthForCharacter()
    {
        damageableCharacter.SetMaxHealth(_health);
    }
    private void Update()
    {
        bloodItem.text = "X " + countBloodItem;
        manaItem.text = "X " + countManaItem;
        coin.text = "X " + countCoin;
        coinShop.text = "X " + countCoin;
        keyItem.text = "X " + countKeyItem;

        //USING ITEMS
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (countBloodItem > 0)
            {
                countBloodItem -= 1;
                BuffBlood();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (countManaItem > 0)
            {
                if (turnOffSkill == false) //het tgian moi cho tang skill
                {
                    Debug.Log("Using mana item");
                    ManaBar.SetTimeMana(5); //5s su dung skill
                    TimeLeft = 5;
                    TimerOn = true;
                    ManaBar.TurnTimerOn();
                    SetSkillUp();
                    countManaItem -= 1;
                }
            }
        }

        //Count time using mana
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
            }
            else
            {
                TimerOn = false;
                turnOffSkill = false;
                SetSkillDown();
            }
        }
    }

    void BuffBlood()
    {
        damageableCharacter.BuffBlood(1);
    }

    void SetSkillUp()
    {
        spriteRenderer.color = UnityEngine.Color.yellow;
        gameObject.transform.localScale = new Vector2(1.4f, 1.4f);
        moveSpeed = (float)(moveSpeed * 1.5);
        swordAttack.damage = 4;
        turnOffSkill = true;
        arrowSkill.UpDamage();
    }

    public void SetSkillDown()
    {
        spriteRenderer.color = UnityEngine.Color.white;
        gameObject.transform.localScale = new Vector2(1.2f, 1.2f);
        moveSpeed = (float)(moveSpeed / 1.5);
        swordAttack.damage = 2;
        arrowSkill.DownDamage();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            //if movement input is not 0, try to move
            if (movementInput != Vector2.zero)
            {
                bool success = TryMove(movementInput);

                //nhan vat truot tren vat khi va cham -> movement more smoother 
                if (!success && movementInput.x > 0)//xay ra collision thi di chuyen theo huong khac
                {
                    success = TryMove(new Vector2(movementInput.x, 0));
                }
                if (!success && movementInput.y > 0)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
                animator.SetBool("IsMoving", success);
            }
            else
            {
                animator.SetBool("IsMoving", false);
            }

            //Set direction of sprite to movement direction
            if (movementInput.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (movementInput.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        }

    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            int count = rb.Cast(
                    direction, //X and Y values between -1 and 1 that present the direction from the body to look for collision
                    movementFilter, //The settings that determine where a collision can occur on such as layers to collide with
                    castCollisions, //List of collisions to store the found collisions into after the cast is finished 
                    moveSpeed * Time.fixedDeltaTime + collisionOffset //the amount to cast equal to the movement plus an offset
                    );
            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            //cant move if there is no direction to move in
            return false;
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire()
    {
        animator.SetTrigger("swordAttack");
    }

    void SwordAtack()
    {
        LockMovement();
        if (spriteRenderer.flipX == true)
        {
            swordAttack.AttackLeft();
        }
        else
        {
            swordAttack.AttackRight();
        }
    }

    public void EndSwordAttack()
    {
        UnLockMovement();
        swordAttack.StopAttack();
    }

    void LockMovement()
    {
        canMove = false;
    }
    void UnLockMovement()
    {
        canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ManaItem")
        {
            Destroy(collision.gameObject);
            countManaItem += 1;
        }
        if (collision.gameObject.tag == "BloodItem")
        {
            Destroy(collision.gameObject);
            countBloodItem += 1;

        }
        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            countCoin += 1;

        }
        if (collision.gameObject.tag == "Key")
        {
            Destroy(collision.gameObject);
            countKeyItem += 1;
        }
        if (collision.gameObject.tag == "Chest")
        {
            var chest = collision.gameObject.GetComponent<Chest>();
            chest.textPress.enabled = true;
        }
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Chest")
        {
            var chest = collision.gameObject.GetComponent<Chest>();
            chest.textPress.enabled = true;
            if (Input.GetKey(KeyCode.R))
            {
                chest.ChestOpen();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Chest")
        {
            var chest = collision.gameObject.GetComponent<Chest>();
            chest.textPress.enabled = false;
        }
    }

    public void BuyBloodItem()
    {
        if (countCoin >= 10)
        {
            countCoin -= 10;
            countBloodItem += 1;
        }
    }

    public void BuyManaItem()
    {
        if (countCoin >= 5)
        {
            countCoin -= 5;
            countManaItem += 1;
        }
    }


}
