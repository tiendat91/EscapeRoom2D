using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed;

    public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; } }
    private bool facingLeft = false;

    public static HeroController Instance;

    /// <summary>
    /// This check if player is attacking or not, compare to run and idle
    /// </summary>
    public bool IsAttack { get { return isAttack; } set { isAttack = value; } }
    private bool isAttack = true;

    /// <summary>
    /// This checks if the player can attack once at a time
    /// </summary>
    public bool CanAttackAgain { get { return canAttackAgain; } set { canAttackAgain = value; } }
    private bool canAttackAgain = true;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    private void Awake()
    {
        Instance = this;
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

    /// <summary>
    /// Read value from file C# Input Action
    /// </summary>
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

    /// <summary>
    /// Move player position
    /// </summary>
    private void Move()
    {
            rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    /// <summary>
    /// Get the mouse position in world coordinates
    /// </summary>
    private Vector2 GetMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerToMouseDirection = (mousePosition - transform.position).normalized;
        return playerToMouseDirection;
    }

    /// <summary>
    /// Move player's direction according to the mouse
    /// </summary>
    private void AdjustPlayerFacingDirection()
    {
        Vector2 playerToMouseDirection = GetMousePosition();
        animator.SetFloat("faceX", playerToMouseDirection.x);
        animator.SetFloat("faceY", playerToMouseDirection.y);

        //Get player facing direction
        if (playerToMouseDirection.x <= 0)
        {
            FacingLeft = true;
        }
        else
        {
            FacingLeft = false;
        }
    }

    /// <summary>
    /// Trigger player attack
    /// </summary>
    public void SwordAttackAnimation()
    {
            IsAttack = true;
            Vector2 playerToMouseDirection = GetMousePosition();
            animator.SetFloat("faceX", playerToMouseDirection.x);
            animator.SetFloat("faceY", playerToMouseDirection.y);
            animator.Play("archer_sword");
    }
}
