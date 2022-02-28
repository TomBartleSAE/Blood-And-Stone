using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingVampireState : StateBase
{

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Entering Attacking Vampire State");
    }

    public override void Execute()
    {
        base.Execute();
        
        Debug.Log("Executing Attacking Vampire State");
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("Exiting Attacking Vampire State");
    }
}
