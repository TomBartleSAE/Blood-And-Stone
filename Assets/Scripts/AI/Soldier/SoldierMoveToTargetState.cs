using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class SoldierMoveToTargetState : AntAIState
{
    public PathfindingAgent pathfinding;
    public FollowPath followPath;
    public SoldierModel soldier;
    
    public override void Enter()
    {
        base.Enter();
        
        pathfinding = GetComponentInParent<PathfindingAgent>();
        followPath = GetComponentInParent<FollowPath>();
        soldier = GetComponentInParent<SoldierModel>();
        
        pathfinding.FindPath(this.transform.position, soldier.target.position);

        Debug.Log("Entering Moving to Keep State");
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
        Debug.Log("Executing Moving to Keep State");
    }

    public override void Exit()
    {
        base.Exit();
        
        Debug.Log("Exiting Moving to Keep State");
    }
}
