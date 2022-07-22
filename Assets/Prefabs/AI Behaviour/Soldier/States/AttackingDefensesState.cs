using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using Anthill.AI;
using UnityEngine;

public class AttackingDefensesState : AntAIState
{
    public SoldierModel soldier;
    //public PathfindingAgent pathfinding;
    public Rigidbody rb;
    public FollowPath followPath;

    public GameObject owner;
    public Transform target;

    public bool inRange = false;
    public bool canAttack = true;

    public float pathTimer = 0.5f;
    public float attackTimer;
    public float damage;

    public override void Create(GameObject aGameobject)
    {
        base.Create(aGameobject);

        owner = aGameobject;
        followPath = owner.GetComponent<FollowPath>();
    }
    
    public override void Enter()
    {
        base.Enter();

        soldier = owner.GetComponent<SoldierModel>();
        target = soldier.target;
        rb = owner.GetComponent<Rigidbody>();

        soldier.pathfinding.FindPath(transform.position, target.position);
        attackTimer = soldier.attackCooldown;
        canAttack = true;

        target.GetComponent<Tom.Health>().DeathEvent += LeaveState;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        pathTimer -= Time.deltaTime;
        if (pathTimer <= 0)
        {
            if (!inRange)
            {
                soldier.pathfinding.FindPath(transform.position, target.position);
                pathTimer = 0.5f;
            }
        }

        CheckRange();

        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            if (inRange && canAttack)
            {
                canAttack = false;
                Attack();
                attackTimer = soldier.attackCooldown;
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public void CheckRange()
    {
        float range;
        
        range = Vector3.Distance(owner.transform.position, target.transform.position);

        if (range <= 0.5)
        {
            inRange = true;
        }
    }

    public void Attack()
    {
        target.GetComponent<Tom.Health>().ChangeHealth(-damage, owner);
        canAttack = true;
    }

    public void LeaveState(GameObject go)
    {
        soldier.attackedByTower = false;
        Finish();
    }
}
