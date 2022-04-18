using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Tanks;
using Tom;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PatrollingState : AntAIState
{
    public GameObject owner;
    public PathfindingAgent pathfinding;
    public FollowPath followPath;
    public GuardModel guard;
            
    private Vector3 pointA;
    private Vector3 pointB;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        owner = aGameObject;
    }
    public override void Enter()
    {
        base.Enter();

        pathfinding = owner.GetComponent<PathfindingAgent>();
        followPath = owner.GetComponent<FollowPath>();
        guard = owner.GetComponent<GuardModel>();
        
        pointA = guard.patrolPointA;
        pointB = guard.patrolPointB;

        guard.viewRange = 10;
        
        //Not sure if we want to have them spawn at their patrol start point? If not then can use this
        //find path to patrolPointA;
        MoveToPatrol();

        //then this, but in a different part
        //pathfinding.FindPath(guard.patrolPointA, guard.patrolPointB);

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

        Debug.Log("Exiting Patrolling State");
    }

    public void MoveToPatrol()
    {
        pathfinding.FindPath(transform.position, guard.patrolPointA);
    }

    //probably not actually going to work tbh
    public void Patrol()
    {
        if (transform.position == pointA)
        {
            pathfinding.FindPath(pointA, pointB);
        }
        else if (transform.position == pointB)
        {
            pathfinding.FindPath(pointB, pointA);
        }
    }
}
