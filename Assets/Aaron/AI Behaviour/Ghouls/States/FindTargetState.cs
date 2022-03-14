using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Unity.VisualScripting;
using UnityEngine;

public class FindTargetState : AntAIState
{
    public VillagerManager manager;
    public GhoulModel ghoulModel;
    public Wander wander;

    public float speed;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        manager = FindObjectOfType<VillagerManager>();
        ghoulModel = GetComponentInParent<GhoulModel>();
        
        wander = GetComponentInParent<Wander>();
    }

    public override void Enter()
    {
        base.Enter();
        float distance = 10000000;
        float shortestDistance;
        shortestDistance = distance;
        
        foreach (var villager in manager.Villagers)
        {
            if (villager != null)
            {
                distance = Vector3.Distance(this.transform.position, villager.transform.position);
            
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    ghoulModel.target = villager;
                }
            }
        }

        wander.enabled = false;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        Vector3.MoveTowards(transform.position, ghoulModel.target.transform.position, (speed * Time.deltaTime));
    }

    public override void Exit()
    {
        base.Exit();
    }
}
