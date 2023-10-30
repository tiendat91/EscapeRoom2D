using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    private Rigidbody2D rigidbody;
    private Vector2 moveDirection;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + moveDirection * (moveSpeed * Time.fixedDeltaTime));
    }
    public void MoveTo(Vector2 targetPosition)
    {
        moveDirection = targetPosition;
    }
   
}
