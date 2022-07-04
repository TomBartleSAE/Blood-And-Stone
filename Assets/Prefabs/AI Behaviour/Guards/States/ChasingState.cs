using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Tanks;
using UnityEngine;

public class ChasingState : AntAIState
{
    private GameObject owner;
    private GuardModel guard;
    private PathfindingAgent pathfinding;
    private Transform target;

    public float timer;
    public float captureTimer;

    private bool isCapturing = false;
    private bool inRange = false;
    private bool losingTarget;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        owner = aGameObject;

        pathfinding = owner.GetComponent<PathfindingAgent>();
    }
    public override void Enter()
    {
        base.Enter();

        guard = owner.GetComponent<GuardModel>();
        
        //get vampire location
        target = guard.vampire;
        captureTimer = guard.captureTime;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        //pathfinding to chase vampire
        if (target != null)
        {
            timer -= Time.deltaTime;
            
            if (timer < 0)
            {
                MoveToPoint(target.position);
                timer = 1f;
            }
        }
        
        CheckRange();
    }

    public override void Exit()
    {
        base.Exit();
        
        //change chasing bool
        guard.isAlert = false;
    }

    //checking to see if in range to capture; if true will change to capturing state
    public void CheckRange()
    {
        if (Vector3.Distance(transform.position, target.position) < 0.5f)
        {
            captureTimer -= Time.deltaTime;

            if (captureTimer <= 0)
            {
                guard.targetCaptured = true;
            }
        }
        else
        {
            captureTimer = guard.captureTime;
        }
    }

    public void MoveToPoint(Vector3 targetPos)
    {
        pathfinding.FindPath(transform.position, targetPos);
    }
}
