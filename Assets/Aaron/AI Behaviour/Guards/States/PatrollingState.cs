using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PatrollingState : StateBase
{
    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("Entering Patrolling State");
    }

    public override void Execute()
    {
        base.Execute();

        Debug.Log("Executing Patrolling State");
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("Exiting Patrollin State");
    }
}
