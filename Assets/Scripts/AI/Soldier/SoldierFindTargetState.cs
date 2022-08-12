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

    public void FindTarget()
    {
	    
    }

    
}
