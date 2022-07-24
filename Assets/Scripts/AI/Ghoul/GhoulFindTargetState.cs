using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Unity.VisualScripting;
using UnityEngine;

public class GhoulFindTargetState : AntAIState
{
    public GhoulModel ghoulModel;

    private float timer;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        ghoulModel = GetComponentInParent<GhoulModel>();
    }

    public override void Enter()
    {
        base.Enter();

        ghoulModel.inRange = false;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            FindTarget();
            timer = 1;
        }

        Finish();
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

        //This can serve as an auto target if the player hasn't selected a target to attack.
        if (ghoulModel.LocalAutoAttack)
        {
            if (DayNPCManager.Instance.Soldiers.Count > 0)
            {
                foreach (var soldier in DayNPCManager.Instance.Soldiers)
                {
                    distance = Vector3.Distance(this.transform.position, soldier.transform.position);
                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        ghoulModel.clickMovement.target = soldier.transform;
                        ghoulModel.hasTarget = true;
                    }
                }
            }
        }
    }
}