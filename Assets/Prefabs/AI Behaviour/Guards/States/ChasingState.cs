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

    private Coroutine coroutine;

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
        
        coroutine = null;  

        guard = owner.GetComponent<GuardModel>();
        target = guard.vampire;
        losingTarget = false;
        
        //Gets path to target every 2 seconds. Note repeat time for testing/changing
        InvokeRepeating("ChaseTarget",0, 2 );
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        guard.vision.angle = 45f;
        guard.vision.distance = 5f;

        if (guard.vampire != null)
        {
            CheckRange();  
        }

        if (inRange == false && coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        if (!guard.vision.CanSeeObject(guard.vampire))
        {
            losingTarget = true;
            StartCoroutine(TargetLost());
        }
        else
        {
            losingTarget = false;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public void CheckRange()
    {
        Transform target = guard.vampire;

        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance <= guard.guardRange && isCapturing != true)
        {
            inRange = true;
            coroutine = StartCoroutine(CapturingTarget());
        }
        else if (distance > guard.guardRange)
        {
            inRange = false;
            isCapturing = false;
        }
    }

    //will capture the vampire after capture time has been reached
    public IEnumerator CapturingTarget()
    {
        isCapturing = true;

        for (int i = 0; i < guard.captureTime; i++)
        {
            yield return new WaitForSeconds(1);
        }

        guard.targetCaptured = true;
        guard.CapturedVampire();
        isCapturing = false;
    }

    //if vampire is out of sight, this will run to determine if they have been lost
    public IEnumerator TargetLost()
    {
        losingTarget = true;
        
        for (int i = 0; i < guard.searchCooldown; i++)
        {
            yield return new WaitForSeconds(1);
        }

        guard.isAlert = false;
        guard.hasTarget = false;
        guard.investigateTarget = null;
    }
    
    public void ChaseTarget()
    {
        if (!losingTarget)
        {
            pathfinding.FindPath(transform.position, target.transform.position);
        }
    }
}
