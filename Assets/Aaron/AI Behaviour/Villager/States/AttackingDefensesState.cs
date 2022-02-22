using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingDefensesState : StateBase
{
    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("Entering Attacking Defenses State");
    }

    public override void Execute()
    {
        base.Execute();
        
        Debug.Log("Executing Attacking Defenses State");
    }

    public override void Exit()
    {
        base.Exit();
        
        Debug.Log("Exiting Attacking Defenses State");
    }
}
