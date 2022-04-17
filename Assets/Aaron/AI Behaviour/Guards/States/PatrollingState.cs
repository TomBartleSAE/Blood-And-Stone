using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Tanks;
using Tom;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PatrollingState : AntAIState
{
    public GameObject target;
    public PathfindingAgent pathfinding;
    public FollowPath followPath;
    public GuardModel guard;
    
    
    public override void Enter()
    {
        base.Enter();

        pathfinding = GetComponentInParent<PathfindingAgent>();
        followPath = GetComponentInParent<FollowPath>();
        guard = GetComponentInParent<GuardModel>();
        
        pathfinding.FindPath(guard.patrolPointA, guard.patrolPointB);

        Debug.Log("Entering Patrolling State");
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        Debug.Log("Executing Patrolling State");
    }

    public override void Exit()
    {
        base.Exit();
        
        Finish();

        Debug.Log("Exiting Patrollin State");
    }
}
