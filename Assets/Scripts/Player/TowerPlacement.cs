using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerPlacement : MonoBehaviour
{
    public PathfindingGrid grid;

    public BuildingBase selectedBuilding;
    private Node selectedNode;

    public Camera cam;

    private MainControls controls;
    private InputAction leftClick;

    public LayerMask buildableLayers;

    public event Action<BuildingBase, Node> MouseOverNodeEvent;
    public event Action MouseOffGridEvent;

    private void Awake()
    {
        controls = new MainControls();
    }

    private void OnEnable()
    {
        controls.Enable();

        leftClick = controls.Day.LeftClick;
        leftClick.Enable();
        leftClick.performed += PerformClick;
    }

    private void OnDisable()
    {
        controls.Disable();
        leftClick.Disable();
        leftClick.performed += PerformClick;
    }

    void Update()
    {
        if (selectedBuilding)
        {
            Vector2 mousePosition = controls.Day.MousePosition.ReadValue<Vector2>();
            Ray ray = cam.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, buildableLayers))
            {
                Node hitNode = grid.GetNodeFromPosition(hit.point);

                if (hitNode != null)
                {
                    if (selectedNode == null || hitNode != selectedNode)
                    {
                        selectedNode = hitNode;
                        MouseOverNodeEvent?.Invoke(selectedBuilding, selectedNode);
                    }
                }
            }
            else
            {
                selectedNode = null;
                MouseOffGridEvent?.Invoke();
            }
        }
    }

    public void Build(Vector3 position)
    {
        if (PlayerManager.Instance.currentBlood >= selectedBuilding.cost)
        {
            BuildingBase newBuilding = Instantiate(selectedBuilding, position, Quaternion.identity);
            newBuilding.grid = grid;
            
            selectedNode.isBlocked = true;
            selectedNode.canBuild = false;
            
            PlayerManager.Instance.ChangeBlood(-selectedBuilding.cost);
            
            grid.Generate();
        }
    }

    public void PerformClick(InputAction.CallbackContext obj)
    {
        if (selectedNode != null)
        {
            if (selectedNode.canBuild)
            {
                Build(selectedNode.coordinates);
            }
        }
    }

    public void SelectBuilding(BuildingBase building)
    {
        selectedBuilding = building;
    }
}