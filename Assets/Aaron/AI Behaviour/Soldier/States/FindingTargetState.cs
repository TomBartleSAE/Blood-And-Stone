using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class FindingTargetState : AntAIState
{
    public SoldierModel soldier;
    
    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        soldier = GetComponentInParent<SoldierModel>();
    }

    public override void Enter()
    {
        base.Enter();
        
        
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
