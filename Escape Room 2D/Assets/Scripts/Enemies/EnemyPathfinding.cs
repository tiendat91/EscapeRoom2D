using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    private Rigidbody2D rigidbody;
    private Vector2 moveDirection;
    private Knockback knockback;
    private void Awake()
    {
        knockback = GetComponent<Knockback>();
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (knockback.GettingKnockedBack)
        {
            return;
        }
        rigidbody.MovePosition(rigidbody.position + moveDirection * (moveSpeed * Time.fixedDeltaTime));
    }
    public void MoveTo(Vector2 targetPosition)
    {
        moveDirection = targetPosition;
    }
   
}
