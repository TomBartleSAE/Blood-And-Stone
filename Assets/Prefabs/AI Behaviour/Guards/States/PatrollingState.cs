using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Tanks;
using Tom;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

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
        pathfinding = owner.GetComponent<PathfindingAgent>();
        guard = owner.GetComponent<GuardModel>();
        GetPatrolPoints();
    }
    public override void Enter()
    {
        base.Enter();

        pointACoords = pointA;
        pointBCoords = pointB;

        //allows to find (same patrol route) even if has exited state previously
        FindPatrolPath(pointA, pointB);
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        //HACK for if it's reached the destination - I'd rather a PathCompletedEvent or something
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

    public override void Exit()
    {
        base.Exit();
        
        Finish();
    }

    //Gets points for patrol route per night
    public void GetPatrolPoints()
    {
        //useful later perhaps
        //int gridRangeX = pathfinding.grid.gridSize.x;
        //int gridRangeZ = pathfinding.grid.gridSize.y;

        //gets second patrol point; makes sure it isn't blocked
        Node tempPointB = new Node();
        tempPointB.coordinates = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));

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

    //changes path destination according to patrol direction
    void PatrolRoute()
    {
        switch (patrolType)
        {
            case PatrolType.goingTo :
                //pathfinding.destination = pointB;
                FindPatrolPath(transform.position, pointB);
                break;
            case PatrolType.returning :
                //pathfinding.destination = pointA;
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
