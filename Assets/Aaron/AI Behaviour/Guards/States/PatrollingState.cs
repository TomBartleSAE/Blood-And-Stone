using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PatrollingState : AntAIState
{
    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("Entering Patrolling State");
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        Debug.Log("Executing Patrolling State");
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("Exiting Patrollin State");
    }
}
