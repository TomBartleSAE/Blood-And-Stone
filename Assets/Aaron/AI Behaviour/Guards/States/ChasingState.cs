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

    private Coroutine coroutine;

    public bool isCapturing = false;
    public bool inRange = false;
    
    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        owner = aGameObject;
    }
    public override void Enter()
    {
        base.Enter();

        coroutine = null;  

        guard = owner.GetComponent<GuardModel>();
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
        
        Debug.Log("Capturing Started"); 

        for (int i = 0; i < guard.captureTime; i++)
        {
            Debug.Log("Capturing");
            yield return new WaitForSeconds(1);
        }
        
        //TODO: Link up game over state
        guard.targetCaptured = true;
        Debug.Log("CAPTURED");
        isCapturing = false;
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
    
}
