using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToKeepState : StateBase
{
    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("Entering Return to Keep State");
    }

    public override void Execute()
    {
        base.Execute();
        
        Debug.Log("Executing Return to Keep State");
    }

    public override void Exit()
    {
        base.Exit();
        
        Debug.Log("Exiting Return to Keep State");
    }
}
