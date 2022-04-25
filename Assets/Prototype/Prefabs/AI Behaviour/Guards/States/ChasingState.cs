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
    private GameObject target;

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
        target = guard.chaseTarget;
        
        //Gets path to target every 5 seconds. Note repeat time for testing/changing
        InvokeRepeating("ChaseTarget",0, 5 );
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        if (guard.chaseTarget != null)
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
        GameObject target = guard.chaseTarget;

        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance <= guard.guardRange && isCapturing != true)
        {
            inRange = true;
            isCapturing = true;
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
        isCapturing = false;
        
        //Can remove if not needed
        GameOverEvent?.Invoke();
    }

    public IEnumerator TargetLost()
    {
        for (int i = 0; i < guard.searchCooldown; i++)
        {
            yield return new WaitForSeconds(1);
        }

        guard.isAlert = false;
        guard.InvestigateTarget = null;
    }

    public void ChaseTarget()
    {
        pathfinding.FindPath(transform.position, target.transform.position);
    }
}
