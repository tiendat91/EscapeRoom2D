using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterBehaviourScript : MonoBehaviour
{
    bool IsMoving
    {
        set
        {
            isMoving = value;
            animator.SetBool("IsMoving", isMoving);
        }
    }
    public float moveSpeed = 150f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public SwordAttack swordAttack;
    public float maxSpeed = 8f;
    public float idleFriction = 0.9f;
    bool isMoving = false;

    Vector2 movementInput = Vector2.zero;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    SpriteRenderer spriteRenderer;
    Animator animator;

    bool canMove = true;

    public float _health = 5;
    public float Health
    {
        set
        {
            if (value < _health)
            {
                animator.SetTrigger("hit");
            }
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            //if movement input is not 0, try to move
            if (movementInput != Vector2.zero)
            {
                bool success = TryMove(movementInput);

                //nhân v?t tr??t trên v?t th? khi va ch?m -> movement more smoother 
                if (!success && movementInput.x > 0)//x?y ra collision, th? di chuy?n theo h??ng khác (x ho?c y)
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

    //private void FixedUpdate()
    //{
    //    //Move animation and add velocity

    //    //Accelerate the player while run direction is pressed BUT dont run faster than max speed
    //    if (canMove == true && movementInput != Vector2.zero)
    //    {
    //        rb.velocity = Vector2.ClampMagnitude(rb.velocity + (movementInput * moveSpeed * Time.deltaTime), maxSpeed);

    //        //Whether looking left or right
    //        if (movementInput.x > 0)
    //        {
    //            spriteRenderer.flipX = false;
    //        }
    //        else if (movementInput.x < 0)
    //        {
    //            spriteRenderer.flipX = true;
    //        }

    //        IsMoving = true;
    //    }
    //    else
    //    {
    //        //No movement so interpolate velocity towards 0
    //        rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, idleFriction);
    //        IsMoving = false;

    //    }
    //}
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
}
