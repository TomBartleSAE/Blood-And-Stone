using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Unity.VisualScripting;
using UnityEngine;

public class GhoulFindTargetState : AntAIState
{
    public GhoulModel ghoulModel;
    public Wander wander;

    public bool autoAttack;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        Debug.Log("Entering Find Target State");
        
        ghoulModel = GetComponentInParent<GhoulModel>();
    }

    public override void Enter()
    {
        base.Enter();
        
        Spawner.Instance.FinishedSpawningEvent += FindTarget;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        if (ghoulModel.hasTarget == false)
        {
            FindTarget();
        }

        Finish();
    }

    public override void Exit()
    {
        GetComponentInParent<Wander>().enabled = false;
        base.Exit();
    }

    void FindTarget()
    {
        float distance = 10000000;
        float shortestDistance;
        shortestDistance = distance;
        
        //This can serve as an auto target if the player hasn't selected a target to attack.
        if (autoAttack)
        {
            foreach (var soldier in DayNPCManager.Instance.Soldiers)
            {
                if (soldier != null)
                {
                    distance = Vector3.Distance(this.transform.position, soldier.transform.position);

                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        ghoulModel.target = soldier.transform;
                        ghoulModel.hasTarget = true;
                    }
                }
            }
        }
    }
}
