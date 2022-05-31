using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Anthill.AI;
using UnityEngine;

public class FleeingState : AntAIState
{
    public Wander wander;
    public VillagerModel villager;
    public GameObject owner;
    
    public float fleeTime;
    
    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        
        owner = aGameObject;
        villager = aGameObject.GetComponentInParent<VillagerModel>();
        wander = GetComponentInParent<Wander>();
    }
    
    public override void Enter()
    {
        base.Enter();

        fleeTime = 5;
        wander.speed = 15;

        StartCoroutine(Flee());
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aDeltaTime);
    }

    public override void Exit()
    {
        base.Exit();
    }
    
    IEnumerator Flee()
    {
        for (int i = 0; i < fleeTime; i++)
        {
            yield return new WaitForSeconds(1);
        }

        villager.isScared = false;
    }
}