using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterBehaviourScript : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>(); 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //if movement input is not 0, try to move
        if(movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);

            //nhân v?t tr??t trên v?t th? khi va ch?m -> movement more smoother 
            if (!success)//x?y ra collision, th? di chuy?n theo h??ng khác (x ho?c y)
            {
                success = TryMove(new Vector2(movementInput.x,0));
                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }
        }
    }
    private bool TryMove(Vector2 direction)
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

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();

    }
}
