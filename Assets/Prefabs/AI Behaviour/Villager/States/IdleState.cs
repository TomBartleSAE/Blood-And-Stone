using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class IdleState : AntAIState
{
    public PathfindingAgent pathfinding;
    public GameObject owner;

    public Vector3 destination;

    public bool newPath = true;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        
        owner = aGameObject;
        pathfinding = owner.GetComponent<PathfindingAgent>();
    }
    
    public override void Enter()
    {
        base.Enter();
        
        GetDestination();

        GoToDestination(transform.position, destination);
    }

     public override void Execute(float aDeltaTime, float aTimeScale)
     {
         base.Execute(aDeltaTime, aTimeScale);

         //gets new path once destination reached, again, I'd prefer to have a PathCompletedEvent 
         if (Vector3.Distance(destination, transform.position) < 0.5f)
         {
             pathfinding.path.Clear();
             GetDestination();
             pathfinding.FindPath(transform.position, destination);
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
        destinationNode.coordinates = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));

        if (!destinationNode.isBlocked)
        {
            destination = destinationNode.coordinates;
            newPath = false;
        }
        else
        {
            GetDestination();
        }
    }

    //finds the path
    void GoToDestination(Vector3 startPos, Vector3 destinationPos)
    {
        pathfinding.FindPath(startPos, destinationPos);
    }
}
