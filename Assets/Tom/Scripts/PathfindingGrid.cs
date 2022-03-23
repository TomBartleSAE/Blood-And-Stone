using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathfindingGrid : MonoBehaviour
{
    public Vector2Int gridSize = new Vector2Int(10, 10);

    public Node[,] nodes;

    public LayerMask blockedLayers;

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
                Vector3Int currentPosition = new Vector3Int((int) transform.position.x + x, 0, (int) transform.position.z + y);
                nodes[x, y].coordinates = currentPosition;
                nodes[x, y].index = new Vector2Int(x, y);

                if (Physics.CheckBox(currentPosition, Vector3.one / 3f, Quaternion.identity, blockedLayers))
                {
                    nodes[x, y].isBlocked = true;
                }
            }
        }
    }

    public Node GetNodeFromPosition(Vector3 position)
    {
        Vector3Int coordinates = new Vector3Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y),
            Mathf.RoundToInt(position.z));

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
                        if (nodes[x, y].isBlocked)
                        {
                            Gizmos.color = new Color(1f,0f,0f,0.5f);
                        }
                        else
                        {
                            Gizmos.color = new Color(0f,1f,0f,0.5f);
                        }
                        
                        Gizmos.DrawCube(nodes[x,y].coordinates, Vector3.one);
                    }
                }
            }
        }
    }
}