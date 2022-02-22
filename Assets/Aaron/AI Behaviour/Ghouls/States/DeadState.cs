using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : StateBase
{
    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("Entering Dead State");
    }

    public override void Execute()
    {
        base.Execute();
        
        Debug.Log("Executing Dead State");
    }

    public override void Exit()
    {
        base.Exit();
        
        Debug.Log("Exiting Dead State");
    }
}
