using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Tanks;
using Unity.VisualScripting;
using UnityEngine;

public class ChasingState : AntAIState
{
    public GameObject owner;
    private GuardModel guard;
    public PathfindingAgent pathfinding;
    private Transform target;

    private Coroutine coroutine;

    public bool isCapturing = false;
    public bool inRange = false;

    public static event Action GameOverEvent;
    
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
        
        //Gets path to target every 5 seconds. Note repeat time for testing/changing
        InvokeRepeating("ChaseTarget",0, 5 );
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        if (guard.vampire != null)
        {
            CheckRange();  
        }

        if (inRange == false && coroutine != null)
        {
            StopCoroutine(coroutine);
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

    public IEnumerator TargetLost()
    {
        for (int i = 0; i < guard.searchCooldown; i++)
        {
            yield return new WaitForSeconds(1);
        }

        guard.isAlert = false;
        guard.investigateTarget = null;
    }

    public void ChaseTarget()
    {
        pathfinding.FindPath(transform.position, target.transform.position);
    }
}
