using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Tanks;
using Tom;
using UnityEngine;

public class PatrollingState : AntAIState
{
    public GameObject owner;
    public PathfindingAgent pathfinding;
    public GuardModel guard;
    public GameObject[] waypoints;

    public Vector3 pointA;
    public Vector3 pointB;

    public Vision vision;
    public Transform vampire;

    public enum PatrolType
    {
        goingTo,
        returning
    };

    public PatrolType patrolType;
    
    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        owner = aGameObject;
    }
    
    public override void Enter()
    {
        base.Enter();
        guard = owner.GetComponent<GuardModel>();
        pathfinding = owner.GetComponent<PathfindingAgent>();
        vision = guard.vision;
        vampire = guard.vampire;
        //seems like a redundant reference but makes life easier
        waypoints = guard.waypoints;
        guard.vision.angle = 25;
        guard.vision.distance = 2.5f;
        guard.GetComponent<FollowPath>().moveSpeed = 2.5f;

        //gets random waypoint from array of possible waypoints
        GetPatrolPoints();

        //allows to find (same patrol route) even if has exited state previously
        FindPatrolPath(pointA, pointB);
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
        if (pathfinding.path.Count != 0)
        {
            if (Vector3.Distance(pathfinding.path[pathfinding.path.Count - 1].coordinates, owner.transform.position) < 0.5)
            {
                //determines which way on the route it's going
                if (patrolType == PatrolType.goingTo)
                {
                    patrolType = PatrolType.returning;
                }
                else if(patrolType == PatrolType.returning)
                {
                    patrolType = PatrolType.goingTo;
                }
            
                PatrolRoute();
            }
        }
        
        //looking for vampire
        if (vision.CanSeeObject(vampire))
        {
            guard.hasTarget = true;
            guard.isAlert = true;
        }
    }

    public override void Exit()
    {
        base.Exit();
        
        Finish();
    }

    //Gets points for patrol route per night
    public void GetPatrolPoints()
    {
        // HACK
        int tempPatrolPoint = Random.Range(0, waypoints.Length - 1);
        while(Vector3.Distance(waypoints[tempPatrolPoint].transform.position, owner.transform.position) < 0.5f)
        {
            tempPatrolPoint = Random.Range(0, waypoints.Length - 1);
        }
        
        pointA = owner.transform.position;
        pointB = waypoints[tempPatrolPoint].transform.position;
    }

    //changes path destination according to patrol direction
    void PatrolRoute()
    {
        switch (patrolType)
        {
            case PatrolType.goingTo :
                FindPatrolPath(owner.transform.position, pointB);
                break;
            case PatrolType.returning :
                FindPatrolPath(owner.transform.position, pointA);
                break;
        }
    }

    //Finds the path
    void FindPatrolPath(Vector3 startingPoint, Vector3 destinationCoords)
    {
        owner.GetComponent<PathfindingAgent>().FindPath(pointA, pointB);
    }
}
