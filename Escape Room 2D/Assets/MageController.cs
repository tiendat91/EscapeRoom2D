using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MageController : MonoBehaviour
{
    bool IsMoving
    {
        set
        {
            isMoving = value;
            animator.SetBool("IsMoving", isMoving);
        }
    }

    public float moveSpeed = 9f;
    public float maxSpeed = 9f;
    public float idleFriction = 0.9f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public MageSwordAttack swordAttack;
    [SerializeField]

    public MageDamageableCharacter mageDamageableCharacter;
    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    bool canMove = true;
    bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        mageDamageableCharacter = GetComponent<MageDamageableCharacter>();
    }

    private void FixedUpdate()
    {
        //Move animation and add velocity

        //Accelerate the player while run direction is pressed BUT dont run faster than max speed
        if (canMove == true && movementInput != Vector2.zero)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity + (movementInput * moveSpeed * Time.deltaTime), maxSpeed);

            //Whether looking left or right
            if (movementInput.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (movementInput.x < 0)
            {
                spriteRenderer.flipX = true;
            }

            IsMoving = true;
        }
        else
        {
            //No movement so interpolate velocity towards 0
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, idleFriction);
            IsMoving = false;

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

    void OnFire()
    {
        animator.SetTrigger("swordAttack");
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
    
}