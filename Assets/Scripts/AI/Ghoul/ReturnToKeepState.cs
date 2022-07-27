using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class ReturnToKeepState : AntAIState
{
    public PathfindingAgent pathfinding;
    public Transform keepLocation;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        
        pathfinding = GetComponentInParent<PathfindingAgent>();
    }
    public override void Enter()
    {
        base.Enter();
        
        //pathfinding.FindPath(transform.position, keepLocation.position);
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
