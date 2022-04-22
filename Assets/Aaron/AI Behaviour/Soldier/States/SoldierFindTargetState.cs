using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class SoldierFindTargetState : AntAIState
{
    public Wander wander;
    public SoldierModel soldierModel;
    public GameObject target;
    public NPCManager manager;
    
    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
    }

    public override void Enter()
    {
        base.Enter();
        
        wander = GetComponentInParent<Wander>();
        soldierModel = GetComponentInParent<SoldierModel>();
        manager = FindObjectOfType<NPCManager>();

        wander.enabled = true;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
        FindTarget();
    }

    public override void Exit()
    {
        base.Exit();
    }

    void FindTarget()
    {
        float distance = 10000000;
        float shortestDistance;
        shortestDistance = distance;

        foreach (var ghoul in manager.Ghouls)
        {
            if (ghoul != null)
            {
                distance = Vector3.Distance(this.transform.position, ghoul.transform.position);

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    soldierModel.target = ghoul;
                    soldierModel.hasTarget = true;
                }
            }
        }
    }
}
