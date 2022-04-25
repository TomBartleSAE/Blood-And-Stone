using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class GhoulIdleState : AntAIState
{
    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Entering Ghoul Idle State");
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
