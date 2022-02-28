using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigatingState : StateBase
{
    public override void Enter()
    {
        base.Enter();

        Debug.Log("Entering Investigating State");
    }

    public override void Execute()
    {
        base.Execute();

        Debug.Log("Executing Investigating State");
    }

    public override void Exit()
    {
        base.Exit();
        
        Debug.Log("Exiting Investigating State");
    }
}
