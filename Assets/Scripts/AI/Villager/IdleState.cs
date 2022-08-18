using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class IdleState : AntAIState
{
    public PathfindingAgent pathfinding;
    public GameObject owner;

    public Vector3 destination;

    public bool hasPath = true;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        
        owner = aGameObject;
    }
    
    public override void Enter()
    {
        base.Enter();
        
        pathfinding = owner.GetComponent<PathfindingAgent>();

        GetDestination();
    }

     public override void Execute(float aDeltaTime, float aTimeScale)
     {
         base.Execute(aDeltaTime, aTimeScale);

         //gets new path once destination reached, again, I'd prefer to have a PathCompletedEvent 
         if (pathfinding.path.Count != 0)
         {
             if (Vector3.Distance(pathfinding.path[pathfinding.path.Count - 1].coordinates, transform.position) < 0.5)
             {
                 pathfinding.path.Clear();
                 hasPath = false;
                 GetDestination();
                 pathfinding.FindPath(transform.position, destination);
             }
         }
         else
         {
             GetDestination();
         }
     }

    public override void Exit()
    {
        base.Exit();
    }

    //gets next destination for path
    void GetDestination()
    {
        Node destinationNode = new Node();
        destinationNode.coordinates = new Vector3(Random.Range(-8, 8), 0, Random.Range(5, -7));

        if (destinationNode.isBlocked)
        {
            GetDestination();
        }

        if (!destinationNode.isBlocked || pathfinding.path.Count == 0)
        {
            destination = destinationNode.coordinates;
            pathfinding.FindPath(owner.transform.position, destinationNode.coordinates);
        }
    }
}
