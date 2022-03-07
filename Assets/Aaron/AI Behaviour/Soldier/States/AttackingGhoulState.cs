using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class AttackingGhoulState : AntAIState
{
    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("Entering Attacking Ghoul State");
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
        Debug.Log("Executing Attacking Ghoul State");
    }

    public override void Exit()
    {
        base.Exit();
        
        Debug.Log("Exiting Attacking Ghoul State");
    }
}
