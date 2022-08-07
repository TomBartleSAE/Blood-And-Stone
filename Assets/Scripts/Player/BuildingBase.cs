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

    public event Action BuildingDestroyedEvent;

    public virtual void Awake()
    {
        health.DeathEvent += GetDestroyed;
    }

    public void GetDestroyed(GameObject building)
    {
        Node node = grid.GetNodeFromPosition(transform.position);
        PlayerManager.Instance.towerLayout[node.index.x, node.index.y] = 0;
        gameObject.SetActive(false);
        grid.Generate();
        BuildingDestroyedEvent?.Invoke();
    }
}
