using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Anthill.AI;
using UnityEngine;

public class FleeingState : AntAIState
{
    public float fleeSpeed;
    public float fleeTime;
    
    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("Entering Fleeing State");

        //TODO fine tune when health component/death event have been added
        //GetComponent<Health>().DeathEvent += RaycastCheck;
    }

    // public override void Execute(aDeltaTime, aTimeScale)
    // {
    //     base.Execute(aDeltaTime, aDeltaTime);
    //     
    //     Debug.Log("Executing Fleeing State");
    // }

    public override void Exit()
    {
        base.Exit();
        
        Debug.Log("Exiting Fleeing State");
    }

    void RaycastCheck()
    {
        //add raycast logic here
    }

    void Flee()
    {
        //TODO fine tune when steering behaviour added
        //GetComponent<SteeringBehaviour>().SetSpeed(fleeSpeed);
        StartCoroutine(FleeTimer());
    }

    IEnumerator FleeTimer()
    {
        for (int i = 0; i < fleeTime; i++)
        {
            yield return new WaitForSeconds(1);
        }

        
    }
}