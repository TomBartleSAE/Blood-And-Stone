using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class GhoulIdleState : AntAIState
{
    //public PathfindingAgent pathfinding;
    //public FollowPath followPath;
    
    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
    }

    public override void Enter()
    {
        base.Enter();

        //pathfinding = GetComponentInParent<PathfindingAgent>();
        //followPath = GetComponentInParent<FollowPath>();
//
        //pathfinding.enabled = false;
        //followPath.enabled = false;

        Debug.Log("Entering Ghoul Idle State");
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
