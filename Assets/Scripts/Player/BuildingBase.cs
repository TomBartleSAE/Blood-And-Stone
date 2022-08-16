using System;
using System.Collections;
using System.Collections.Generic;
using Tom;
using Unity.Mathematics;
using UnityEngine;

public class BuildingBase : MonoBehaviour
{
    public int cost;

    public Health health;

    public PathfindingGrid grid;

    public ParticleSystem dustParticle;

    public event Action BuildingDestroyedEvent;

    public virtual void Awake()
    {
        health.DeathEvent += GetDestroyed;
    }

    private void OnDestroy()
    {
        health.DeathEvent -= GetDestroyed;
    }

    public void GetDestroyed(GameObject building)
    {
        Node node = grid.GetNodeFromPosition(transform.position);
        PlayerManager.Instance.towerLayout[node.index.x, node.index.y] = 0;
        ParticleSystem newParticle = Instantiate(dustParticle, transform.position, quaternion.identity);
        newParticle.Play();
        gameObject.SetActive(false);
        grid.Generate();
        BuildingDestroyedEvent?.Invoke();
    }

    public void RefundBlood()
    {
        // Multiply cost in ChangeBlood by decimal (e.g. 0.75 for 75%) to only refund a fraction of the cost
        PlayerManager.Instance.ChangeBlood(cost);
    }
}
