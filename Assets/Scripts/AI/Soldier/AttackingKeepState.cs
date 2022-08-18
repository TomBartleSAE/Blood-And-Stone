using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Anthill.AI;
using Tom;
using UnityEngine;

public class AttackingKeepState : AntAIState
{
    public GameObject owner;

    public Transform castle;
    public PathfindingAgent pathfinding;
    public SoldierModel soldierModel;
    
    
    public float damage;
    public float attackTimer;
    
    public bool canAttack;
    public bool inRangeOfCastle = false;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        owner = aGameObject;
        soldierModel = owner.GetComponent<SoldierModel>();
        pathfinding = owner.GetComponent<PathfindingAgent>();
    }
    public override void Enter()
    {
        base.Enter();

        castle = soldierModel.castle;
        
        pathfinding.FindPath(owner.transform.position, castle.position);
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        attackTimer -= Time.deltaTime;

        if (Vector3.Distance(transform.position, castle.position) < 0.5)
        {
	        inRangeOfCastle = true;
        }

        if (attackTimer <= 0)
        {
	        canAttack = true;
        }

        if (inRangeOfCastle && canAttack)
        {
	        canAttack = true;
	        AttackCastle();
	        attackTimer = soldierModel.attackCooldown;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public void AttackCastle()
    {
	    if (canAttack)
	    {
		    canAttack = false;
		    castle.GetComponentInParent<Health>().ChangeHealth(-damage, owner);
		    soldierModel.anim.SetTrigger("Attack");
	    }
    }
}
