using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathfindingGrid : MonoBehaviour
{
    public Vector2Int gridSize = new Vector2Int(10, 10);
    public float tileSize = 1f;

    public Node[,] nodes;

    public LayerMask blockedLayer; // Determines which layers block pathfinding agents
    public LayerMask buildingLayer; // Determines which layers prevent building during the Day phase

    public Transform[] tileBlockers;

    public event Action GridGeneratedEvent;

    public void Awake()
    {
        Generate();
    }

    public void Generate()
    {
        nodes = new Node[gridSize.x, gridSize.y];

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                nodes[x, y] = new Node();
                Vector3 currentPosition = new Vector3(transform.position.x + (x * tileSize), 0, transform.position.z + (y * tileSize));
                nodes[x, y].coordinates = currentPosition;
                nodes[x, y].index = new Vector2Int(x, y);

                if (Physics.CheckBox(currentPosition, (Vector3.one * tileSize) / 2.1f, Quaternion.identity, blockedLayer))
                {
                    nodes[x, y].isBlocked = true;
                }
                
                // Needs to ignore triggers to stop range sphere from preventing building
                if (!Physics.CheckBox(currentPosition, (Vector3.one * tileSize) / 2.1f, Quaternion.identity, buildingLayer, QueryTriggerInteraction.Ignore))
                {
                    nodes[x, y].canBuild = true;
                }
                // Need this to ignore tile blockers but still make towers block off paths
                else if (Physics.CheckBox(currentPosition, (Vector3.one * tileSize) / 2.1f, Quaternion.identity, buildingLayer, QueryTriggerInteraction.Ignore))
                {
                    nodes[x, y].isBlocked = true;
                }
            }
        }

        // Prevents building on designated positions, caused too many issues when trying to do this with colliders
        foreach (Transform blocker in tileBlockers)
        {
            GetNodeFromPosition(blocker.position).canBuild = false;
        }
        
        GridGeneratedEvent?.Invoke();
    }

    public Node GetNodeFromPosition(Vector3 position)
    {
        Vector3 coordinates = new Vector3(Mathf.Round(position.x/tileSize) * tileSize, 0,
            Mathf.RoundToInt(position.z/ tileSize) * tileSize);

        foreach (Node n in nodes)
        {
            if (n.coordinates == coordinates)
            {
                return n;
            }
        }

        return null;
    }

    public bool IsIndexWithinGrid(Vector2Int index)
    {
        if (index.x >= 0 && index.x < gridSize.x && index.y >= 0 && index.y < gridSize.y)
        {
            return true;
        }

        return false;
    }

    private void OnDrawGizmosSelected()
    {
        if (nodes != null)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    if (nodes[x, y] != null)
                    {
                        if (nodes[x, y].isBlocked || !nodes[x, y].canBuild)
                        {
                            Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
                        }
                        else
                        {
                            Gizmos.color = new Color(0f, 1f, 0f, 0.5f);
                        }

                        Gizmos.DrawCube(nodes[x, y].coordinates, (Vector3.one * tileSize));
                    }
                }
            }
        }
    }
}