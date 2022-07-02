using System.Collections;
using System.Collections.Generic;
using Tom;
using UnityEngine;

public class WeakeningTower : TowerBase
{
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.gameObject.GetComponent<EnemyBase>())
        {
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
