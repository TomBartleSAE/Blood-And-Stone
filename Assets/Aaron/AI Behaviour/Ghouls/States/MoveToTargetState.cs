using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Anthill.AI;
using Newtonsoft.Json.Converters;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class MoveToTargetState : AntAIState
{
    public GhoulModel ghoulModel;
    public Rigidbody rb;
    public Transform target;

    public PathfindingAgent pathfinding;
    public FollowPath followPath;

    public Vector2 targetDestination;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        ghoulModel = GetComponentInParent<GhoulModel>();
        rb = GetComponentInParent<Rigidbody>();

    }
    
    public override void Enter()
    {
        base.Enter();
        
        pathfinding = GetComponentInParent<PathfindingAgent>();
        followPath = GetComponentInParent<FollowPath>();
        
        pathfinding.enabled = true;
        followPath.enabled = true;
        
        target = ghoulModel.target.transform;
        pathfinding.destination = target;

        if (pathfinding.destination != null)
        {
            pathfinding.FindPath(ghoulModel.transform.position, ghoulModel.targetPos);
        }

        Debug.Log("Entering Move to Target State");
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        Debug.Log("Executing Move to Target State");
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("Exiting Move to Target State");
        pathfinding.enabled = false;
        followPath.enabled = false; 
    }

    public void SetDestination()
    {
        pathfinding.destination = target;
    }
}
