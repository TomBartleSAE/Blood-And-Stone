using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class VillagerDeathState : AntAIState
{
    public VillagerManager manager;
    public GameObject owner;
    
    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        owner = aGameObject;
        manager = FindObjectOfType<VillagerManager>();
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Destroying");

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
