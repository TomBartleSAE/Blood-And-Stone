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

    public Vector3 pointA;
    public Vector3 pointB;

    public Vector3 pointACoords;
    public Vector3 pointBCoords;

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
        
        //all this needed in Create() as the event is fired in Enter in GuardModel
    }
    public override void Enter()
    {
        base.Enter();
        pathfinding = owner.GetComponent<PathfindingAgent>();
        pathfinding.PathCompletedEvent += ChangeDirection;
        guard = owner.GetComponent<GuardModel>();
        
        pointACoords = pointA;
        pointBCoords = pointB;
        
        GetPatrolPoints();

        guard.vision.angle = 25;
        guard.vision.distance = 2.5f;

        //allows to find (same patrol route) even if has exited state previously
        FindPatrolPath(pointA, pointB);
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
        if (pathfinding.path.Count != 0)
        {
            if (Vector3.Distance(pathfinding.path[pathfinding.path.Count - 1].coordinates, transform.position) < 0.5)
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
    }

    public override void Exit()
    {
        base.Exit();
        
        Finish();
    }

    //Gets points for patrol route per night
    public void GetPatrolPoints()
    {
        //gets second patrol point; makes sure it isn't blocked
        Node tempPointB = new Node();
        tempPointB.coordinates = new Vector3(Random.Range(-8, 8), 0, Random.Range(-7, 5));

        if (tempPointB.isBlocked == false)
        {
            pointA = transform.position;
            pointB = tempPointB.coordinates;
        }
        
        else if(tempPointB.isBlocked == true)
        {
            GetPatrolPoints();
        }
    }

    void ChangeDirection()
    {
        GetPatrolPoints();
        PatrolRoute();
    }

    //changes path destination according to patrol direction
    void PatrolRoute()
    {
        switch (patrolType)
        {
            case PatrolType.goingTo :
                FindPatrolPath(transform.position, pointB);
                break;
            case PatrolType.returning :
                FindPatrolPath(transform.position, pointA);
                break;
        }
    }

    //Finds the path
    void FindPatrolPath(Vector3 startingPoint, Vector3 destinationCoords)
    {
        pathfinding.FindPath(startingPoint, destinationCoords);
    }
}
