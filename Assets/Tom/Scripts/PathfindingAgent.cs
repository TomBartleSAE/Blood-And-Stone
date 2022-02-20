using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathfindingAgent : MonoBehaviour
{
    public PathfindingGrid grid;
    public Tilemap pathMap;
    public Tile pathTile;

    public Tile openTile;
    
    private List<Node> openNodes = new List<Node>();
    private List<Node> closedNodes = new List<Node>();

    public Transform destination;

    public void Start()
    {
        FindPath(transform.position, destination.position);
    }

    public List<Node> FindPath(Vector3 start, Vector3 destination)
    {
        // Node index for start and destination for later use
        Vector3Int startIndex = grid.ConvertPositionToCell(start);
        Vector3Int destinationIndex = grid.ConvertPositionToCell(destination);
        
        foreach (Node node in grid.nodes)
        {
            node.parent = null;
            node.fCost = 0;
            node.gCost = 0;
            node.hCost = 0;
        }
        
        openNodes.Clear();
        closedNodes.Clear();

        Node currentNode = grid.nodes[grid.ConvertPositionToCell(start).x, grid.ConvertPositionToCell(start).y];
        openNodes.Add(currentNode);

        while (openNodes.Count > 0)
        {
            // Find open node with lowest fCost
            int lowestFCost = openNodes[0].fCost;
            foreach (Node node in openNodes)
            {
                if (node.fCost <= lowestFCost)
                {
                    lowestFCost = node.fCost;
                    currentNode = node;
                }
            }

            openNodes.Remove(currentNode);
            closedNodes.Add(currentNode);

            if (currentNode.index == (Vector2Int) destinationIndex)
            {
                break;
            }

            for (int i = currentNode.index.x - 1; i <= currentNode.index.x + 1; i++)
            {
                for (int j = currentNode.index.y - 1; j <= currentNode.index.y + 1; j++)
                {
                    if (i >= 0 && i < grid.terrainMap.size.x && j >= 0 && j < grid.terrainMap.size.y)
                    {
                        Node neighbour = grid.nodes[i, j];
                        if (neighbour.isBlocked || closedNodes.Contains(neighbour))
                        {
                            continue;
                        }

                        int neighbourDistance =
                            currentNode.gCost + CalculateDistance(currentNode.index, neighbour.index);

                        if (neighbourDistance < neighbour.gCost || !openNodes.Contains(neighbour))
                        {
                            neighbour.gCost = neighbourDistance;
                            neighbour.hCost = CalculateDistance(neighbour.index,
                                new Vector2Int(destinationIndex.x, destinationIndex.y));
                            neighbour.fCost = neighbour.gCost + neighbour.hCost;

                            neighbour.parent = currentNode;
                            if (!openNodes.Contains(neighbour))
                            {
                                openNodes.Add(neighbour);
                            }
                        }
                    }
                }
            }
        }

        List<Node> path = new List<Node>();

        while (currentNode != grid.nodes[startIndex.x, startIndex.y])
        {
            pathMap.SetTile(currentNode.coordinates, pathTile);
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        pathMap.SetTile(currentNode.coordinates, pathTile);

        return path;
    }

    public int CalculateDistance(Vector2Int start, Vector2Int end)
    {
        Vector2Int distance = end - start;
        distance = new Vector2Int(Mathf.Abs(distance.x), Mathf.Abs(distance.y)); // Removes negative numbers
        
        if (distance.x > distance.y)
        {
            return distance.y * 14 + 10 * (distance.x - distance.y);
        }

        return distance.x * 14 + 10 * (distance.y - distance.x);
    }
}
