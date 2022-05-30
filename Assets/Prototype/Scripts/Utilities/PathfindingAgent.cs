using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathfindingAgent : MonoBehaviour
{
    public PathfindingGrid grid;
    
    private List<Node> openNodes = new List<Node>();
    private List<Node> closedNodes = new List<Node>();

    public Transform destination;
    
    public List<Node> path;

    public event Action NewPathEvent;
    public event Action PathFailedEvent;

    public bool pathOnStart = false;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        grid.GridGeneratedEvent -= RefindPath;
    }
    
    public void Start()
    {
        grid.GridGeneratedEvent += RefindPath;
        
        if (pathOnStart)
        {
            FindPath(transform.position, destination.position);
        }
    }

    public List<Node> FindPath(Vector3 start, Vector3 destination)
    {
        // Node index for start and destination for later use
        Vector2Int startIndex = grid.GetNodeFromPosition(start).index;
        Vector2Int destinationIndex = grid.GetNodeFromPosition(destination).index;
        
        foreach (Node node in grid.nodes)
        {
            node.parent = null;
            node.fCost = 0;
            node.gCost = 0;
            node.hCost = 0;
        }
        
        openNodes.Clear();
        closedNodes.Clear();

        Node currentNode = grid.GetNodeFromPosition(start);
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

            if (currentNode.index == destinationIndex)
            {
                break;
            }

            for (int i = currentNode.index.x - 1; i <= currentNode.index.x + 1; i++)
            {
                for (int j = currentNode.index.y - 1; j <= currentNode.index.y + 1; j++)
                {
                    if (grid.IsIndexWithinGrid(new Vector2Int(i,j)))
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
        
        path = new List<Node>();

        while (currentNode != grid.nodes[startIndex.x, startIndex.y])
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        
        path.Add(currentNode);
        
        if (path[0].index != destinationIndex)
        {
            // Used if path is blocked or cannot reach destination
            path.Clear();
            PathFailedEvent?.Invoke();
            return path;
        }
        
        path.Reverse();
        
        NewPathEvent?.Invoke();
        
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

    private void RefindPath()
    {
        if (path != null)
        {
            FindPath(transform.position, path[path.Count - 1].coordinates);
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        if (path != null)
        {
            for (int x = 0; x < grid.gridSize.x; x++)
            {
                for (int y = 0; y < grid.gridSize.y; y++)
                {
                    if (grid.nodes[x,y] != null)
                    {
                        if (openNodes.Contains(grid.nodes[x,y]))
                        {
                            Gizmos.color = Color.green;
                            Gizmos.DrawCube(grid.nodes[x,y].coordinates, (Vector3.one * grid.tileSize));
                        }
                        else if (path.Contains(grid.nodes[x,y]))
                        {
                            Gizmos.color = Color.blue;
                            Gizmos.DrawCube(grid.nodes[x,y].coordinates, (Vector3.one * grid.tileSize));
                        }
                        else if (closedNodes.Contains(grid.nodes[x,y]))
                        {
                            Gizmos.color = Color.yellow;
                            Gizmos.DrawCube(grid.nodes[x,y].coordinates, (Vector3.one * grid.tileSize));
                        }
                    }
                }
            }
        }
    }
}

