using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class VillagerDeathState : AntAIState
{
    public GameObject owner;
    
    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        owner = aGameObject;
    }

    public override void Enter()
    {
        base.Enter();

        Finish();
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
