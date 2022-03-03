using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingKeepState : StateBase
{
    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("Entering Attacking Keep State");
    }

    public override void Execute()
    {
        base.Execute();
        
        Debug.Log("Executing Attacking Keep State");
    }

    public override void Exit()
    {
        base.Exit();
        
        Debug.Log("Exiting Attacking Keep State");
    }
}
