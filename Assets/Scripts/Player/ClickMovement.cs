using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickMovement : MonoBehaviour
{
    public PathfindingAgent agent;

    private MainControls controls;
    private InputAction leftClick;

    public Camera cam;

    public LayerMask enemyLayer;
    public LayerMask walkableLayers;

    public Transform target;

    private float timer;

    private void Awake()
    {
        controls = new MainControls();
    }

    private void OnEnable()
    {
        controls.Enable();

        leftClick = controls.Night.MouseClick;
        leftClick.Enable();
        leftClick.performed += PerformClick;
    }

    private void OnDisable()
    {
        controls.Disable();
        leftClick.Disable();
        leftClick.performed -= PerformClick;
    }

    public void Update()
    {
        if (target != null)
        {
            timer -= Time.deltaTime;
            
            if (timer < 0)
            {
                MoveToPoint(target.position);
                timer = 1f;
            }
        }
    }

    private void PerformClick(InputAction.CallbackContext obj)
    {
        Vector2 mousePosition = controls.Day.MousePosition.ReadValue<Vector2>();
        Ray ray = cam.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyLayer))
        {
            target = hit.transform;
            return;
        }
        else
        {
            // HACK
            target = null;
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, walkableLayers))
        {
            Node hitNode = agent.grid.GetNodeFromPosition(hit.point);

            if (!hitNode.isBlocked)
            {
                MoveToPoint(hitNode.coordinates);
            }
        }
    }

    public void MoveToPoint(Vector3 destination)
    {
        agent.FindPath(transform.position, destination);
    }
}
