using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Anthill.AI;
using Unity.VisualScripting;
using UnityEngine;

public class StunnedState : AntAIState
{
    public GameObject owner;
    public VillagerModel villager;
    public Wander wander;

    public float stunTime;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        
        owner = aGameObject;
        villager = aGameObject.GetComponentInParent<VillagerModel>();
        wander = aGameObject.GetComponentInParent<Wander>();
    }

    public override void Enter()
    {
        base.Enter();
        
        wander.enabled = false;
        stunTime = 5;

        StartCoroutine(StunnedTimer());
    }
    
    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
    }

    public override void Exit()
    {
        base.Enter();
    }

    IEnumerator StunnedTimer()
    {
        for (int i = 0; i < stunTime; i++)
        {
            yield return new WaitForSeconds(1);
        }

        villager.isStunned = false;
        villager.isScared = true;

        wander.enabled = true;
        
        Finish();
    }
}
