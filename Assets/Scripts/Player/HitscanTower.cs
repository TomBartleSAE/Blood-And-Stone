using System.Collections;
using System.Collections.Generic;
using Tom;
using UnityEngine;

public class HitscanTower : DamageTower
{
    public override void Attack()
    {
        base.Attack();
        
        target.GetComponent<Health>().ChangeHealth(-damage, gameObject);
    }
}
