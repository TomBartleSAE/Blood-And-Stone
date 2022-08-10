using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TowerBase : BuildingBase
{
    public SphereCollider trigger;
    public float range = 2f;
    
    public Collider target;
    public List<Collider> targets;

    public override void Awake()
    {
        base.Awake();
        trigger.radius = range;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyBase>())
        {
            targets.Add(other);
            other.GetComponent<Tom.Health>().DeathEvent += RemoveTarget;
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<EnemyBase>())
        {
            targets.Remove(other);
            other.GetComponent<Tom.Health>().DeathEvent -= RemoveTarget;
            if (targets.Count > 0)
            {
                target = targets[Random.Range(0, targets.Count)];
            }
            else
            {
                target = null;
            }
        }
    }

    private void RemoveTarget(GameObject target)
    {
        targets.Remove(target.GetComponent<Collider>());
    }
}
