using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Tanks;
using UnityEngine;

public class SoldierFindTargetState : AntAIState
{
    public SoldierModel soldierModel;
    public PathfindingAgent pathfinding;
    public NPCManager manager;

    public GameObject target;
    public GameObject castle;
    private GameObject owner;
    
    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        owner = aGameObject;
    }

    public override void Enter()
    {
        base.Enter();
        
        soldierModel = owner.GetComponent<SoldierModel>();
        pathfinding = owner.GetComponent<PathfindingAgent>();
        manager = FindObjectOfType<NPCManager>();

        castle = soldierModel.castle;
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

    public void FindCastle(GameObject castle)
    {
        pathfinding.FindPath(transform.position, castle.transform.position);
        
        //if Path not clear, find the nearest wall and target it to make a path
    }

    public void BreakThroughWall()
    {
        
    }
}
