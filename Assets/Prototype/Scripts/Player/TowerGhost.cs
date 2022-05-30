using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGhost : MonoBehaviour
{
    public TowerPlacement tower;
    public GameObject ghost;
    public ParticleSystem rangeRing;

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

        if (!node.canBuild)
        {
            ghost.GetComponentInChildren<MeshRenderer>().material = blockedMaterial;
        }
        else
        {
            ghost.GetComponentInChildren<MeshRenderer>().material = openMaterial;
        }

        if (building.GetComponent<TowerBase>())
        {
            // Remove this if we use something other than ParticleSystem to show range
            ParticleSystem.ShapeModule ringShape = rangeRing.shape;
            ringShape.radius = building.GetComponent<TowerBase>().range + 0.5f; 
            rangeRing.Play();
        }
        else
        {
            rangeRing.Stop();
        }

        ghost.transform.position = node.coordinates;
        ghost.SetActive(true);
    }

    public void HideGhost()
    {
        ghost.SetActive(false);
    }
}
