using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGhost : MonoBehaviour
{
    public TowerPlacement tower;
    public GameObject ghost;

    public Material openMaterial, blockedMaterial;
    
    private void Awake()
    {
        HideGhost();
        tower.MouseOverNodeEvent += ShowGhost;
        tower.MouseOffGridEvent += HideGhost;
    }

    public void ShowGhost(BuildingBase building, Node node)
    {
        ghost.GetComponentInChildren<MeshFilter>().mesh = building.GetComponentInChildren<MeshFilter>().sharedMesh;
        Material ghostMaterial;
        if (node.isBlocked)
        {
            ghost.GetComponentInChildren<MeshRenderer>().material = blockedMaterial;
        }
        else
        {
            ghost.GetComponentInChildren<MeshRenderer>().material = openMaterial;
        }

        ghost.transform.position = node.coordinates;
        ghost.SetActive(true);
    }

    public void HideGhost()
    {
        ghost.SetActive(false);
    }
}
