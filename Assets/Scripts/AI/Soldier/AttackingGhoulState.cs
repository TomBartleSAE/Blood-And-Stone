using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Tanks;
using Tom;
using UnityEngine;

public class AttackingGhoulState : AntAIState
{
    public SoldierModel soldier;
    public PathfindingAgent pathfinding;

    public GameObject owner;
    public Transform target;

    public bool canAttack = true;

    public float damage;
    private float attackTimer;
    private float pathTimer;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        owner = aGameObject;
    }

    public override void Enter()
    {
        base.Enter();

        soldier = owner.GetComponent<SoldierModel>();
        pathfinding = owner.GetComponent<PathfindingAgent>();
        target = soldier.target;
        damage = soldier.damage;
        target.GetComponent<Health>().DeathEvent += TargetDead;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            if (canAttack)
            {
                canAttack = false;
                Attack();
                attackTimer = soldier.attackCooldown;
            }
        }

        pathTimer -= Time.deltaTime;
        if (pathTimer <= 0)
        {
            FindPath();
        }
    }

    public override void Exit()
    {
        soldier.attackedByGhoul = false;
        soldier.inRange = false;
        soldier.target = soldier.castle;
        base.Exit();
    }

    public void FindPath()
    {
        pathfinding.FindPath(transform.position, target.transform.position);
    }

    public void Attack()
    {
        target.GetComponent<Tom.Health>().ChangeHealth(-damage, gameObject);
        canAttack = true;
        soldier.anim.SetTrigger("Attack");
    }

    void TargetDead(GameObject deadThing)
    {
        Finish();
    }
}