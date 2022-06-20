using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Unity.VisualScripting;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

public class InvestigatingState : AntAIState
{
    public GameObject owner;
    private PathfindingAgent pathfinding;
    private GuardModel guard;
    
    private bool investigating;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        owner = aGameObject;
    }
    public override void Enter()
    {
        base.Enter();
        pathfinding = owner.GetComponent<PathfindingAgent>();
        guard = owner.GetComponent<GuardModel>();
        guard.isPatrolling = false;
        
        //values for vision cone - heightened awareness = higher values
        guard.GetComponentInChildren<Vision>().angle = 45;
        guard.GetComponentInChildren<Vision>().distance = 7.5f;

        //Run FindPath to get to event site
        pathfinding.FindPath(transform.position, guard.investigateTarget.transform.position);
        investigating = false;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        CheckInvestigationPosition();
    }

    public override void Exit()
    {
        guard.isAlert = false;

        //restore back to default values for standard awareness
        guard.GetComponentInChildren<Vision>().angle = 25;
        guard.GetComponentInChildren<Vision>().distance = 5;
        
        base.Exit();
    }

    //checking to see if at event site/place to investigate; if so, begin investigation
    public void CheckInvestigationPosition()
    {
        float investigationSpot = Vector3.Distance(transform.position, guard.investigateTarget.transform.position);

        if (investigationSpot < 0.5f && investigating == false)
        {
            investigating = true;
            StartCoroutine(Investigation());
        }
    }

    public IEnumerator Investigation()
    {
        for (int i = 0; i < guard.investigationTime; i++)
        {
            //if something found, go to chase state
            if (guard.vampire != null)
            {
                guard.hasTarget = true;
            }
            yield return new WaitForSeconds(1);
        }
        
        //if nothing spotted in time; return to patrol state
        if (guard.vampire == null)
        {
            guard.isAlert = false;
            guard.isPatrolling = true;
        }
    }
}
