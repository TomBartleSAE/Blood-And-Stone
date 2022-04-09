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
    
    
    public override void Enter()
    {
        base.Enter();

        pathfinding = GetComponentInParent<PathfindingAgent>();
        followPath = GetComponentInParent<FollowPath>();
        
        FindObjectOfType<Health>().DeathEvent += Investigate;
        
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

        Debug.Log("Exiting Patrollin State");
    }

    public void Investigate(GameObject target)
    {
        pathfinding.FindPath(this.transform.position, target.transform.position);
        
    }
}
