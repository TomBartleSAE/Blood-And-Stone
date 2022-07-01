using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class CapturingState : AntAIState
{
    public GuardModel guard;
    public bool isCapturing;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        guard = GetComponentInParent<GuardModel>();
    }
    public override void Enter()
    {
        base.Enter();

        Debug.Log("Entering Attacking Vampire State");
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        Debug.Log("Executing Attacking Vampire State");
        
        //will capture the vampire after capture time has been reached
        for (int i = 0; i < guard.captureTime; i++)
        {
            isCapturing = true;

            guard.targetCaptured = true;
            guard.CapturedVampire();
            isCapturing = false;
        }
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("Exiting Attacking Vampire State");
    }
}
