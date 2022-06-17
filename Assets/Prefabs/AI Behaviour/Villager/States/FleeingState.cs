using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Anthill.AI;
using UnityEngine;

public class FleeingState : AntAIState
{
    public VillagerModel villager;
    public GameObject owner;
    
    public float fleeTime;
    
    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        
        owner = aGameObject;
        villager = aGameObject.GetComponentInParent<VillagerModel>();
    }
    
    public override void Enter()
    {
        base.Enter();

        fleeTime = 5;

        villager.GetComponent<FollowPath>().moveSpeed = 5;

        StartCoroutine(Flee());
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aDeltaTime);
    }

    public override void Exit()
    {
        
        villager.GetComponent<FollowPath>().moveSpeed = 1.5f;
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