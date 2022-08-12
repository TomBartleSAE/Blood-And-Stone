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
    public ClickMovement clickMovement;

    public Transform targetDestination;
    private float timer;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        pathfinding = GetComponentInParent<PathfindingAgent>();

        ghoulModel = GetComponentInParent<GhoulModel>();
    }

    public override void Enter()
    {
        base.Enter();

        clickMovement = ghoulModel.clickMovement;

        targetDestination = ghoulModel.clickMovement.target.transform;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        
        //will find path if in autoAttack; FindPath() is also in ClickMovement so will get called there if not in autoAttack

        if (ghoulModel.autoAttack)
        {
            
            timer -= Time.deltaTime;
            
            if (timer <= 0)
            {
                FindPath();
                timer = clickMovement.repathTime;
            }
        }
    }

    public override void Exit()
    {
        targetDestination = null;
        base.Exit();
    }

    void FindPath()
    {
        pathfinding.FindPath(ghoulModel.transform.position, targetDestination.position);
    }
}