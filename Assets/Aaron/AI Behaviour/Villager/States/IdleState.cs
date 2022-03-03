using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateBase
{
    public float wanderSpeed;

    public override void Enter()
    {
        base.Enter();
        
        Wander();
        
        Debug.Log("Entering Idle State");
    }

    public override void Execute()
    {
        base.Execute();
        
        Debug.Log("Executing Idle State");
    }

    public override void Exit()
    {
        base.Exit();
        
        Debug.Log("Exiting Idle State");
    }

    public void Wander()
    {
        //TODO fine tune when steering behaviour added
        //GetComponent<SteeringBehaviour>().SetSpeed(wanderSpeed);
    }
}
