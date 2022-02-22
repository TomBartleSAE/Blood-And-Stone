using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingGhoulState : StateBase
{
    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("Entering Attacking Ghoul State");
    }

    public override void Execute()
    {
        base.Execute();
        
        Debug.Log("Executing Attacking Ghoul State");
    }

    public override void Exit()
    {
        base.Exit();
        
        Debug.Log("Exiting Attacking Ghoul State");
    }
}
