using System.Collections;
using System.Collections.Generic;
using Tom;
using UnityEngine;
using System;

public class WeakeningTower : TowerBase
{
    public event Action EnemyBeingWeakened;

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.gameObject.GetComponent<EnemyBase>())
        {
            EnemyBeingWeakened?.Invoke();
            other.GetComponent<Health>().healthMultiplier = 2f;
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        
        if (other.gameObject.GetComponent<EnemyBase>())
        {
            other.GetComponent<Health>().healthMultiplier = 1f;
        }
    }
}
