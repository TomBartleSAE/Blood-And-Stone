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

    public float timer = 1;
    public float captureTimer;
    public float loseTimer = 3;

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
        guard.vision.angle = 45f;
        guard.vision.distance = 5f;
        guard.isAlert = true;
        guard.IsAlerted();
        
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
        
        //if can't see target, start timer
        //else reset timer
        if (!guard.vision.CanSeeObject(target))
        {
            losingTarget = true;

            loseTimer -= Time.deltaTime;

            if (loseTimer < 0)
            {
                guard.hasTarget = false;
                guard.isAlert = false;
                loseTimer = 3;
            }
        }
        
        CheckRange();
    }

    public override void Exit()
    {
        base.Exit();
        
        //change chasing bool
        guard.isAlert = false;
        guard.NotAlertedAnymore();
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
                guard.CapturedVampire();
            }
        }
        else
        {
            captureTimer = guard.captureTime;
        }
    }

    public void MoveToPoint(Vector3 targetPos)
    {
        pathfinding.FindPath(owner.transform.position, targetPos);
    }
}
