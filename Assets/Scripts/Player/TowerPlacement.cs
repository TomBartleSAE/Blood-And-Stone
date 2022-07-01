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

    public LayerMask buildableLayers;

    public event Action<BuildingBase, Node> MouseOverNodeEvent;
    public event Action MouseOffGridEvent;


    private void OnEnable()
    {
        InputManager.Instance.OnLeftClickEvent += PerformLeftClick;
        InputManager.Instance.OnRightClickEvent += PerformRightClick;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnLeftClickEvent -= PerformLeftClick;
        InputManager.Instance.OnRightClickEvent -= PerformRightClick;
    }

    void Update()
    {
        if (selectedBuilding)
        {
            Ray ray = cam.ScreenPointToRay(InputManager.Instance.GetMousePosition());
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

    public void PerformLeftClick(ClickEventArgs args)
    {
        if (selectedNode != null && selectedBuilding != null)
        {
            if (selectedNode.canBuild)
            {
                Build(selectedNode.coordinates);
            }
        }
    }

    public void PerformRightClick(ClickEventArgs args)
    {
        selectedBuilding = null;
        MouseOffGridEvent?.Invoke(); // Not the best name for this but the tower ghost needs to turn off when you right click
    }

    public void SelectBuilding(BuildingBase building)
    {
        selectedBuilding = building;
    }
}