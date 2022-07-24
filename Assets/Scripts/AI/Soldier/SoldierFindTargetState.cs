using System;
using System.Collections;
using System.Collections.Generic;
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
        pathfinding.PathFailedEvent += BreakThroughWall;
    }

    public override void Enter()
    {
        base.Enter();
        
        castle = soldierModel.castle;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
        //FindTarget();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public void FindCastle()
    {
        pathfinding.FindPath(transform.position, castle.position);
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
