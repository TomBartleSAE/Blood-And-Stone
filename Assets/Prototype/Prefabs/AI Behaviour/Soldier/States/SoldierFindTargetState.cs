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
    public NPCManager manager;

    public GameObject target;
    public Transform castle;
    private GameObject owner;

    public LayerMask buildingLayer;

    public void OnEnable()
    {
        pathfinding.PathFailedEvent += BreakThroughWall;
    }

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
        manager = FindObjectOfType<NPCManager>();



        castle = soldierModel.castle;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
        FindTarget();
    }

    public override void Exit()
    {
        base.Exit();
    }

    void FindTarget()
    {
        float distance = 10000000;
        float shortestDistance;
        shortestDistance = distance;

        foreach (var ghoul in manager.Ghouls)
        {
            if (ghoul != null)
            {
                distance = Vector3.Distance(this.transform.position, ghoul.transform.position);

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    soldierModel.target = ghoul.transform;
                    soldierModel.hasTarget = true;
                }
            }
        }
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
