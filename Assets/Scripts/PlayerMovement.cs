using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    public float moveSpeed;
    private Vector2 moveForce;
    
    public void Move(Vector2 direction)
    {
        moveForce = direction * moveSpeed;
    }

    public void FixedUpdate()
    {
        rb.AddForce(moveForce, ForceMode2D.Impulse);
    }
}
