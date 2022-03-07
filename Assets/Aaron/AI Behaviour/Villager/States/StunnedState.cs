using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunnedState : StateBase
{
    public float stunTime;
    
    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("Entering Stunned State");

        //TODO fine tune when steering behaviours added
        //GetComponent<SteeringBehaviour>().ChangeSpeed(0);
        
        StartCoroutine(StunnedTimer());
    }
    
    public override void Execute()
    {
        base.Enter();
        
        Debug.Log("Executing Stunned State");
    }

    public override void Exit()
    {
        base.Enter();

        Debug.Log("Exiting Stunned State");
    }

    IEnumerator StunnedTimer()
    {
        for (int i = 0; i < stunTime; i++)
        {
            yield return new WaitForSeconds(1);
        }
        
        //ChangeState to IdleState
    }
}
