using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TowerBase : BuildingBase
{
    public SphereCollider trigger;
    public float range = 2f;
    
    public GameObject target;
    public List<GameObject> targets;

    public override void Awake()
    {
        base.Awake();
        trigger.radius = range;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyBase>() && !other.isTrigger)
        {
            targets.Add(other.gameObject);
            other.GetComponent<Tom.Health>().DeathEvent += RemoveTarget;
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<EnemyBase>() && !other.isTrigger)
        {
            targets.Remove(other.gameObject);
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
        targets.Remove(target);
        target.GetComponent<Tom.Health>().DeathEvent -= RemoveTarget;
    }
}
