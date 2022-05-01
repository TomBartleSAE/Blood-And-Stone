using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : BuildingBase
{
    public SphereCollider trigger;
    public float range = 2f;
    
    public List<Collider> targets;

    private void Awake()
    {
        trigger.radius = range;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyBase>())
        {
            targets.Add(other);
            other.GetComponent<Tom.Health>().DeathEvent += RemoveTarget;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<EnemyBase>())
        {
            targets.Remove(other);
            other.GetComponent<Tom.Health>().DeathEvent -= RemoveTarget;
        }
    }

    private void RemoveTarget(GameObject target)
    {
        targets.Remove(target.GetComponent<Collider>());
    }
}
