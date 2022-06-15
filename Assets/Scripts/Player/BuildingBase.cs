using System;
using System.Collections;
using System.Collections.Generic;
using Tom;
using UnityEngine;

public class BuildingBase : MonoBehaviour
{
    public int cost;

    public Health health;

    public PathfindingGrid grid;

    public virtual void Awake()
    {
        health.DeathEvent += GetDestroyed;
    }

    public void GetDestroyed(GameObject building)
    {
        grid.GetNodeFromPosition(transform.position).isBlocked = false;
        gameObject.SetActive(false);
    }
}
