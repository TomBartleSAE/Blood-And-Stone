using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Tanks;
using Tom;
using UnityEngine;

public class AttackingGhoulState : AntAIState
{
    public SoldierModel soldierModel;
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

        soldierModel = owner.GetComponent<SoldierModel>();
        pathfinding = owner.GetComponent<PathfindingAgent>();
        target = soldierModel.target;
	    damage = soldierModel.damage;

        pathfinding.FindPath(owner.transform.position, target.transform.position);
        
	    if (target != null)
	    {
		    target.GetComponent<Health>().DeathEvent += TargetDead;
	    }
    }

    private void OnDisable()
    {
	    if (target != null)
	    {
		    target.GetComponent<Health>().DeathEvent -= TargetDead;
	    }
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
                attackTimer = soldierModel.attackCooldown;
            }
        }
    }

    public override void Exit()
    {
	    base.Exit();
    }

    public void Attack()
    {
        target.GetComponent<Tom.Health>().ChangeHealth(-damage, gameObject);
        canAttack = true;
        soldierModel.anim.SetTrigger("Attack");
    }

    void TargetDead(GameObject deadThing)
    {
        soldierModel.GhoulsInRange.Remove(deadThing.transform);
        soldierModel.ChangeTarget();
        target.GetComponent<Health>().DeathEvent -= TargetDead;
    }
}