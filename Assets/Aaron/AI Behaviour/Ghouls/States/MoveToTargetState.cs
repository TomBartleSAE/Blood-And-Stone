using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTargetState : StateBase
{
    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("Entering Move to Target State");
    }

    public override void Execute()
    {
        base.Execute();
        
        Debug.Log("Executing Move to Target State");
    }

    public override void Exit()
    {
        base.Exit();
        
        Debug.Log("Exiting Move to Target State");
    }
}
