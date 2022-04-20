using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Anthill.AI;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class GhoulMoveToTargetState : AntAIState
{
    public GhoulModel ghoulModel;
    public Rigidbody rb;
    public Transform target;

    public PathfindingAgent pathfinding;

    public Vector3 targetDestination;

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

        target = ghoulModel.target.transform;
        targetDestination = target.position;
        
        pathfinding.enabled = true;
        GetComponentInParent<FollowPath>().enabled = true;
        
        pathfinding.FindPath(ghoulModel.transform.position, target.position);

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
    }
}
