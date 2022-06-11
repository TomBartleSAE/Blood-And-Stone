using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Unity.VisualScripting;
using UnityEngine;

public class GuardSense : MonoBehaviour, ISense
{
    public enum GuardBools
    {
        hasTarget = 0,
        isAlert = 1,
        inRange = 2,
        targetCaptured = 3,
        isPatrolling = 4
    }

    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.BeginUpdate(aAgent.planner);

        aWorldState.Set(GuardBools.hasTarget, aAgent.GetComponent<GuardModel>().hasTarget);
        aWorldState.Set(GuardBools.isAlert, aAgent.GetComponent<GuardModel>().isAlert);
        aWorldState.Set(GuardBools.inRange, aAgent.GetComponent<GuardModel>().inRange);
        aWorldState.Set(GuardBools.targetCaptured, aAgent.GetComponent<GuardModel>().targetCaptured);
        aWorldState.Set(GuardBools.isPatrolling, aAgent.GetComponent<GuardModel>().isPatrolling);
        
        aWorldState.EndUpdate();
    }
}
