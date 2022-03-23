using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerPlacement : MonoBehaviour
{
    public PathfindingGrid grid;

    public BuildingBase selectedTower;
    private Node selectedNode;

    private Camera cam;

    private MainControls controls;
    private InputAction leftClick;

    public LayerMask buildableLayers;

    private void Awake()
    {
        cam = Camera.main;

        controls = new MainControls();
    }

    private void OnEnable()
    {
        controls.Enable();

        leftClick = controls.Day.LeftClick;
        leftClick.Enable();
        leftClick.performed += PerformClick;
    }

    void Update()
    {
        if (selectedTower)
        {
            Vector2 mousePosition = controls.Day.MousePosition.ReadValue<Vector2>();
            Ray ray = cam.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, buildableLayers))
            {
                Node hitNode = grid.GetNodeFromPosition(hit.point);

                if (hitNode != null)
                {
                    selectedNode = hitNode;
                }
                else
                {
                    selectedNode = null;
                }
            }
        }
    }

    public void BuildTower(Vector3 towerPosition)
    {
        Instantiate(selectedTower, towerPosition, Quaternion.identity);
        selectedNode.isBlocked = true;
    }

    public void PerformClick(InputAction.CallbackContext obj)
    {
        if (selectedNode != null)
        {
            if (!selectedNode.isBlocked)
            {
                BuildTower(selectedNode.coordinates);
            }
        }
    }
}