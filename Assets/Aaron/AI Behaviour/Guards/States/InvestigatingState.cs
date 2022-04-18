using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Unity.VisualScripting;
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

        //Run FindPath to get to event site
        pathfinding.FindPath(transform.position, guard.InvestigateTarget.transform.position);
        investigating = false;
        
        Debug.Log("Entering Investigating State");
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        CheckInvestigationPosition();

        Debug.Log("Executing Investigating State");
    }

    public override void Exit()
    {
        base.Exit();
        
        Debug.Log("Exiting Investigating State");
    }

    //checking to see if at event site/place to investigate; if so, begin investigation
    public void CheckInvestigationPosition()
    {
        float investigationSpot = Vector3.Distance(transform.position, guard.InvestigateTarget.transform.position);

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
            //if something found, go tot chase state
            if (guard.chaseTarget != null)
            {
                guard.hasTarget = true;
            }
            yield return new WaitForSeconds(1);
        }

        Debug.Log("Nothing Found");
        
        //if nothing spotted in time; return to patrol state
        if (guard.chaseTarget == null)
        {
            guard.isAlert = false;
        }
    }
}
