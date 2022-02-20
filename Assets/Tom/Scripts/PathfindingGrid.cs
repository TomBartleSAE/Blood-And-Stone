using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathfindingGrid : MonoBehaviour
{
    public Tilemap terrainMap;
    public Tilemap gridMap;

    public List<Tile> blockedTiles = new List<Tile>();

    public Node[,] nodes;
    
    public void Awake()
    {
        Generate();
    }

    public void Generate()
    {
        terrainMap.CompressBounds();
        nodes = new Node[terrainMap.size.x, terrainMap.size.y];
        
        for (int x = 0; x < terrainMap.size.x; x++)
        {
            for (int y = 0; y < terrainMap.size.y; y++)
            {
                nodes[x, y] = new Node();
                Vector3Int currentPosition = new Vector3Int(terrainMap.origin.x + x, terrainMap.origin.y + y, 0);
                nodes[x, y].coordinates = currentPosition;
                nodes[x, y].index = new Vector2Int(x, y);
                
                Tile currentTile = terrainMap.GetTile<Tile>(currentPosition);

                foreach (Tile blockedTile in blockedTiles)
                {
                    if (currentTile == blockedTile)
                    {
                        gridMap.SetTile(currentPosition, blockedTile);
                        nodes[x, y].isBlocked = true;
                        break;
                    }
                }
            }
        }    
    }

    public Vector3Int ConvertPositionToCell(Vector3 position)
    {
        Vector3Int cellPosition = terrainMap.WorldToCell(position);
        return cellPosition - terrainMap.origin;
    }
}
