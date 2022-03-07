using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingDefensesState : StateBase
{
    public Transform target;
    
    public float defenseDamage;
    
    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("Entering Attacking Defenses State");

        FindTarget();
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

    void FindTarget()
    {
        //get defense that attacked it
    }
}
