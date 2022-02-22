using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingToKeepState : StateBase
{
    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("Entering Moving to Keep State");
    }

    public override void Execute()
    {
        base.Execute();
        
        Debug.Log("Executing Moving to Keep State");
    }

    public override void Exit()
    {
        base.Exit();
        
        Debug.Log("Exiting Moving to Keep State");
    }
}
