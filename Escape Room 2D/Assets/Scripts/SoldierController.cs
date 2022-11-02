using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.InputSystem;

public class SoldierController : MonoBehaviour
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
    GameOver gameOver;

    int countBloodItem = 0;
    int countManaItem = 0;
    public int countKeyItem = 0;
    int countCoin = 20; //test
    public float TimeDisplayText = 3;
    public bool TimerOnText = false;
    public float health;

    bool IsMoving
    {
        set
        {
            isMoving = value;
            animator.SetBool("isMoving", isMoving);
        }
    }

    public float moveSpeed = 1f;
    public float maxSpeed = 8f;
    public float idleFriction = 0.9f;
    public float collisionOffset = 0.05f;


    public ContactFilter2D movementFilter;
    public SoldierSwordAttack swordAttack;

    Vector2 movementInput = Vector2.zero;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    bool canMove = true;
    bool isMoving = false;

    float TimeLeft;
    public bool TimerOn = false;
    bool inRangeOpenChest;

    public DesertDamageableCharacter damageableCharacter;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        damageableCharacter = GetComponent<DesertDamageableCharacter>();
        damageableCharacter.SetMaxHealth(health);
        TimerOnText = true;
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
                CountTimeDisplay(bloodItem);
                BuffBlood();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (countManaItem > 0)
            {
                Debug.Log("Using mana item");
                ManaBar.SetTimeMana(5);
                TimeLeft = 5;
                TimerOn = true;
                ManaBar.TurnTimerOn();
                SetSkillUp();
                countManaItem -= 1;
                CountTimeDisplay(manaItem);
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
                SetSkillDown();
            }
        }
    }

    void CountTimeDisplay(TextMeshProUGUI x)
    {
        //if (TimerOnText)
        //{

        //    if (TimeLeft > 0)
        //    {
        //        TimeLeft -= Time.deltaTime;
        //        x.color = UnityEngine.Color.red;
        //        x.fontSize *= 1.5f;
        //    }
        //    else
        //    {
        //        x.color = UnityEngine.Color.white;
        //        x.fontSize /= 1.5f;
        //        TimerOnText = false;
        //    }
        //}
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
        swordAttack.damage *= 2;
    }

    public void SetSkillDown()
    {
        spriteRenderer.color = UnityEngine.Color.white;
        gameObject.transform.localScale = new Vector2(1.2f, 1.2f);
        moveSpeed = (float)(moveSpeed / 1.5);
        swordAttack.damage /= 2;
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
            return false;
        }
        else
        {
            return false;
        }

    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    public void SwordAttack()
    {
        LockMovement();

        if (spriteRenderer.flipX)
        {
            swordAttack.AttackLeft();
        }
        else
        {
            swordAttack.AttackRight();
        }
    }

    void OnFire()
    {
        animator.SetTrigger("swordAttack");
    }

    public void EndSwordAttack()
    {
        UnlockMovement();
        swordAttack.StopAttack();
    }

    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ManaItem")
        {
            Destroy(collision.gameObject);
            countManaItem += 1;
            CountTimeDisplay(manaItem);
        }
        if (collision.gameObject.tag == "BloodItem")
        {
            Destroy(collision.gameObject);
            countBloodItem += 1;
            CountTimeDisplay(bloodItem);

        }
        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            countCoin += 1;
            CountTimeDisplay(coin);

        }
        if (collision.gameObject.tag == "Key")
        {
            Destroy(collision.gameObject);
            countKeyItem += 1;
            CountTimeDisplay(keyItem);
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

    public void OnDeath()
    {
        gameOver.PauseGame();
    }


}
