using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GhoulClickMovement : MonoBehaviour
{
    public PathfindingAgent pathfinding;
    public Camera cam;
    public LayerMask enemyLayer;
    public LayerMask walkableLayer;
    public Transform target;
    
    private MainControls controls;
    private InputAction rightClick;

    private void Awake()
    {
        controls = new MainControls();
        pathfinding = GetComponent<PathfindingAgent>();
        cam = FindObjectOfType<Camera>();
    }

    private void OnEnable()
    {
        
        controls.Enable();
        rightClick = controls.Day.RightClick;
        rightClick.Enable();
        rightClick.performed += PerformClick;
    }

    private void OnDisable()
    {
        controls.Disable();
        rightClick.Disable();
        rightClick.performed -= PerformClick;
    }

    void PerformClick(InputAction.CallbackContext obj)
    {
        Vector2 mousePosition = controls.Day.MousePosition.ReadValue<Vector2>();
        Ray ray = cam.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyLayer))
        {
            target = hit.transform;
            GetComponent<GhoulModel>().target = target;
            GetComponent<GhoulModel>().isIdle = false;
            return;
        }
        else
        {
            target = null;
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, walkableLayer))
        {
            GetComponent<GhoulModel>().hasTarget = false;
            GetComponent<GhoulModel>().target = null;
            
            Node hitNode = pathfinding.grid.GetNodeFromPosition(hit.point);

            if (!hitNode.isBlocked)
            {
                MoveToPoint(hitNode.coordinates);
            }
        }
    }

    public void MoveToPoint(Vector3 destination)
    {
        pathfinding.FindPath(transform.position, destination);
    }
}
