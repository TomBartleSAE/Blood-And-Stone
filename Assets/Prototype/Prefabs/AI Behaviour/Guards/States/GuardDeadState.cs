using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Tanks;
using UnityEngine;

public class GuardDeadState : AntAIState
{
    public GameObject owner;
    
    void Create(GameObject aGameObject)
    {
        aGameObject = owner.gameObject;
        
        base.Create(gameObject);
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
