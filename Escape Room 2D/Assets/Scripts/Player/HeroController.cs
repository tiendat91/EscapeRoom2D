using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }
    //Read value from file C# Input Action
    private void PlayerInput()
    {
        movement = playerControls.Movements.Move.ReadValue<Vector2>();
        animator.SetFloat("moveX", movement.x);
        animator.SetFloat("moveY", movement.y);

        //Change animator states
        if (movement != Vector2.zero)
        {
            animator.Play("archer_run");
        }
        else
        {
            animator.Play("archer_idle");
        }
    }

    //Di chuyển nhân vật
    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    //Hướng nhân vật theo mouse
    private void AdjustPlayerFacingDirection()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerToMouseDirection = (mousePosition - transform.position).normalized;
        Debug.Log(playerToMouseDirection);
        animator.SetFloat("faceX", playerToMouseDirection.x);
        animator.SetFloat("faceY", playerToMouseDirection.y);
    }
}
