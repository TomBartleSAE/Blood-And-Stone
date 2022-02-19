using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollowInput : MonoBehaviour
{
    private MainControls controls;
    private InputAction mouseClick;

    public PlayerMovement movement;
    private bool mouseClicked = false;

    private void Awake()
    {
        controls = new MainControls();
    }

    private void OnEnable()
    {
        controls.Enable();

        mouseClick = controls.Vampire.MouseClick;
        mouseClick.Enable();
        mouseClick.performed += ctx => mouseClicked = true;
        mouseClick.canceled += ctx => mouseClicked = false;
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public void Update()
    {
        if (mouseClicked)
        {
            MouseClick();
        }
        else
        {
            movement.Move(Vector2.zero);
        }
    }

    private void MouseClick()
    {
        Vector2 mousePosition = controls.Vampire.MousePosition.ReadValue<Vector2>();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 moveDirection = mousePosition - (Vector2)transform.position;
        if (moveDirection.magnitude > 0.1f)
        {
            moveDirection.Normalize();
            movement.Move(moveDirection);
        }
    }
}
