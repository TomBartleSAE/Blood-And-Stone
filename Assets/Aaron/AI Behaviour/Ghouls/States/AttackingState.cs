using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : StateBase
{
    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("Entering Attacking State");
    }

    public override void Execute()
    {
        base.Execute();
        
        Debug.Log("Executing Attacking State");
    }

    public override void Exit()
    {
        base.Exit();
        
        Debug.Log("Exiting Attacking State");
    }
}
