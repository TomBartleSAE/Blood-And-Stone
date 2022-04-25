using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector3 coordinates;
    public Vector2Int index;
    public bool isBlocked;
    public bool canBuild;
    public int fCost;
    public int gCost;
    public int hCost;
    public Node parent;
}
