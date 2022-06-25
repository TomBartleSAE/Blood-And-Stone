using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLayoutGrid : MonoBehaviour
{
    public bool showGrid;
    public Vector2 gridSize;
    public float cubeSize = 0.25f;
    
    public void OnDrawGizmos()
    {
        if (showGrid)
        {
            Gizmos.matrix = Matrix4x4.TRS(transform.position, Quaternion.Euler(0,45,0), transform.localScale);
            Gizmos.color = Color.green;
            
            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    Gizmos.DrawCube(new Vector3(transform.position.x + x * cubeSize, 0, transform.position.z + y * cubeSize), Vector3.one * cubeSize);
                }
            }
        }
    }
}
