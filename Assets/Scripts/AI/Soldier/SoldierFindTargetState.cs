using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Anthill.AI;
using Tanks;
using UnityEngine;

public class SoldierFindTargetState : AntAIState
{
    public SoldierModel soldierModel;
    public PathfindingAgent pathfinding;
    
    public Transform castle;
    private GameObject owner;

    public LayerMask buildingLayer;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        owner = aGameObject;
        pathfinding = owner.GetComponent<PathfindingAgent>();
        soldierModel = owner.GetComponent<SoldierModel>();
    }

    public override void Enter()
    {
        base.Enter();
        
        castle = soldierModel.castle;
        FindTarget();
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public void FindTarget()
    {
        pathfinding.FindPath(owner.transform.position, castle.position);

        if (pathfinding.path.Count > 0)
        {
            soldierModel.target = castle;
            soldierModel.hasTarget = true;
            return;
        }
        
	    Collider[] towers = Physics.OverlapSphere(transform.position, 100, buildingLayer, QueryTriggerInteraction.Ignore);
        towers = towers.OrderBy(x => Vector3.Distance(castle.position, x.transform.root.position)).ToArray();

        for (int i = 0; i < towers.Length; i++)
        {
        	Vector3 freeSpace = towers[i].transform.root.position + (owner.transform.position - towers[i].transform.root.position).normalized;
        	pathfinding.FindPath(owner.transform.position, freeSpace);

        	if (pathfinding.path.Count > 0)
        	{
        		soldierModel.target = towers[i].transform.root;
        		soldierModel.castlePathBlocked = true;
                soldierModel.hasTarget = true;
                break;
        	}
        }
    }
}
