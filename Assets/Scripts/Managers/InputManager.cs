using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : ManagerBase<InputManager>
{
    private MainControls controls;
    [HideInInspector] public InputAction leftClick, rightClick;

    public event Action<ClickEventArgs> OnLeftClickEvent, OnRightClickEvent;

    public override void Awake()
    {
        base.Awake();
        
        controls = new MainControls();
        controls.Enable();

        leftClick = controls.Game.LeftClick;
        leftClick.Enable();
        leftClick.performed += PerformLeftClick;

        rightClick = controls.Game.RightClick;
        rightClick.Enable();
        leftClick.performed += PerformRightClick;
    }

    public void PerformLeftClick(InputAction.CallbackContext obj)
    {
        ClickEventArgs args = new ClickEventArgs();
        args.mousePosition = GetMousePosition();
        OnLeftClickEvent?.Invoke(args);
    }

    public void PerformRightClick(InputAction.CallbackContext obj)
    {
        ClickEventArgs args = new ClickEventArgs();
        args.mousePosition = GetMousePosition();
        OnRightClickEvent?.Invoke(args);
    }

    public Vector2 GetMousePosition()
    {
        return controls.Game.MousePosition.ReadValue<Vector2>();
    }
}

public class ClickEventArgs : EventArgs
{
    public Vector2 mousePosition;
}
