using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Tom;

public class DamageTower : TowerBase
{
    public float damage = 1f;
    [Tooltip("Numbers of seconds between firing")]
    public float delay = 1f;
    protected float attackTimer;
    public event Action TowerAttackedEvent;

    public Animator anim;
    public Transform weaponModel;

    
    public virtual void Update()
    {
        if (target != null)
        {
            Vector3 direction = target.transform.position;
            direction.y = weaponModel.position.y;

            weaponModel.LookAt(direction);
        }
        
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f && targets.Count > 0)
        {
            Attack();
        }
    }

    public virtual void Attack()
    {
        attackTimer = delay;
        anim.Play("Attack");
        TowerAttackedEvent?.Invoke();
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        
        if (other.GetComponent<EnemyBase>() && target == null)
        {
            target = other;
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        
        if (other.GetComponent<EnemyBase>() && target == other)
        {
            target = targets[Random.Range(0, targets.Count)];
        }
    }
}
