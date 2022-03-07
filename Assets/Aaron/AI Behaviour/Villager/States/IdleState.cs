using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class IdleState : AntAIState
{
    public float wanderSpeed;

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Entering Idle State");
    }

     public override void Execute(float aDeltaTime, float aTimeScale)
     {
         base.Execute(aDeltaTime, aTimeScale);
         
         Debug.Log("Executing Idle State");
     }

    public override void Exit()
    {
        base.Exit();
        
        Debug.Log("Exiting Idle State");
    }
}
