using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerPlacement : MonoBehaviour
{
    public PathfindingGrid grid;
    
    public BuildingBase[] towerPrefabs;

    public BuildingBase selectedBuilding;
    private Node selectedNode;

    public Camera cam;

    public LayerMask buildableLayers;

    public event Action<BuildingBase, Node> MouseOverNodeEvent;
    public event Action MouseOffGridEvent;
    
    private void Start()
    {
        InputManager.Instance.OnLeftClickEvent += PerformLeftClick;
        InputManager.Instance.OnRightClickEvent += PerformRightClick;
        
        SetupTowerLayout();
        grid.Generate();
    }
    
    private void OnDestroy()
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

    public void Build(BuildingBase building, Vector3 position)
    {
        BuildingBase newBuilding = Instantiate(building, position, Quaternion.identity); 
        newBuilding.grid = grid;

        Node node = grid.GetNodeFromPosition(position);
        node.isBlocked = true;
        node.canBuild = false;

        PlayerManager.Instance.towerLayout[node.index.x, node.index.y] = Array.IndexOf(towerPrefabs, building) + 1;
            
        grid.Generate();
    }

    public void PerformLeftClick(ClickEventArgs args)
    {
        // TODO: Add custom messages to each scenario
        if (selectedNode == null || selectedBuilding == null)
        {
            return;
        }
        
        if (!selectedNode.canBuild)
        {
            return;
        }

        if (PlayerManager.Instance.currentBlood < selectedBuilding.cost)
        {
            return;
        }
        
        Build(selectedBuilding, selectedNode.coordinates);
        PlayerManager.Instance.ChangeBlood(-selectedBuilding.cost);
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
    
    public void SetupTowerLayout()
    {
        int[,] towerLayout = PlayerManager.Instance.towerLayout;

        for (int x = 0; x < towerLayout.GetLength(0); x++)
        {
            for (int y = 0; y < towerLayout.GetLength(1); y++)
            {
                if (towerLayout[x, y] >= 1)
                {
                    int index = towerLayout[x, y] - 1;
                    Build(towerPrefabs[index], grid.nodes[x,y].coordinates);
                }
            }
        }
    }
}