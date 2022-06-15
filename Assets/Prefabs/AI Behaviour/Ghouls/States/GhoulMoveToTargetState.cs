using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using Anthill.AI;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class GhoulMoveToTargetState : AntAIState
{
    public GhoulModel ghoulModel;
    public PathfindingAgent pathfinding;

    public Transform targetDestination;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        pathfinding = GetComponentInParent<PathfindingAgent>();
        
        ghoulModel = GetComponentInParent<GhoulModel>();
    }
    
    public override void Enter()
    {
        base.Enter();
        
        targetDestination = ghoulModel.target.transform;
        
        InvokeRepeating("FindPath",0f,1.5f);
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
    }

    public override void Exit()
    {
        base.Exit();
    }

    void FindPath()
    {
        pathfinding.FindPath(this.transform.position, targetDestination.position);
    }
}
