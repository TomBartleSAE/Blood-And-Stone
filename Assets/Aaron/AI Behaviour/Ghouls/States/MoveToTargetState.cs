using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class MoveToTargetState : AntAIState
{
    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("Entering Move to Target State");
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
        Debug.Log("Executing Move to Target State");
    }

    public override void Exit()
    {
        base.Exit();
        
        Debug.Log("Exiting Move to Target State");
    }
}
