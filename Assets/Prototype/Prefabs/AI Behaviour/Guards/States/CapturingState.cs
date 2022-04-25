using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class CapturingState : AntAIState
{

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Entering Attacking Vampire State");
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
        Debug.Log("Executing Attacking Vampire State");
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("Exiting Attacking Vampire State");
    }
}
