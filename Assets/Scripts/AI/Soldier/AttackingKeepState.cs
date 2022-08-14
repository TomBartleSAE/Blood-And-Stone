using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Tom;
using UnityEngine;

public class AttackingKeepState : AntAIState
{
    public GameObject owner;

    public Transform castle;
    public PathfindingAgent pathfinding;
    public SoldierModel soldierModel;
    
    public LayerMask buildingLayer;
    
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
        pathfinding.PathFailedEvent += BreakThroughWall;
        
        pathfinding.FindPath(owner.transform.position, castle.position);
    }

    private void OnDisable()
    {
	    pathfinding.PathFailedEvent -= BreakThroughWall;
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

    public void OnTriggerEnter(Collider other)
    {
	    if (other.GetComponent<Castle>())
	    {
		    Debug.Log("Castle in Range");
		    inRangeOfCastle = true;
	    }
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
    
    public void BreakThroughWall()
    {
	    Collider[] towers = Physics.OverlapSphere(transform.position, 100, buildingLayer);

	    foreach (var building in towers)
	    {
		    float shortestDistance = 100000;
		    float distance = Vector3.Distance(transform.position, building.transform.position);
		    if (distance < shortestDistance)
		    {
			    shortestDistance = distance;
			    soldierModel.target = building.transform;
		    }
	    }

	    //will change to AttackingDefensesState
	    soldierModel.attackedByTower = true;
    }
}
