using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Tanks;
using UnityEngine;

public class GuardIdleState : AntAIState
{
    private GameObject owner;
    
    void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        
        owner = aGameObject;
    }

    void Enter()
    {
        base.Enter();
    }

    void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
    }

    void Exit()
    {
        base.Exit();
    }
}
