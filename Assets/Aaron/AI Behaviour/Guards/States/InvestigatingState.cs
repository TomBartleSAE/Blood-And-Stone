using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class InvestigatingState : AntAIState
{
    public override void Enter()
    {
        base.Enter();

        Debug.Log("Entering Investigating State");
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        Debug.Log("Executing Investigating State");
    }

    public override void Exit()
    {
        base.Exit();
        
        Debug.Log("Exiting Investigating State");
    }
}
