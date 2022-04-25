using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class GhoulSense : MonoBehaviour, ISense
{
    public enum GhoulBools
    {
        hasTarget = 0,
        targetAlive = 1,
        castleStanding = 2,
        inRange = 3,
        isIdle = 4
    }

    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.BeginUpdate(aAgent.planner);

        aWorldState.Set(GhoulBools.hasTarget, aAgent.GetComponent<GhoulModel>().hasTarget);
        aWorldState.Set(GhoulBools.targetAlive, aAgent.GetComponent<GhoulModel>().targetAlive);
        aWorldState.Set(GhoulBools.castleStanding, aAgent.GetComponent<GhoulModel>().castleStanding);
        aWorldState.Set(GhoulBools.inRange, aAgent.GetComponent<GhoulModel>().inRange);
        aWorldState.Set(GhoulBools.isIdle, aAgent.GetComponent<GhoulModel>().isIdle);
        
        aWorldState.EndUpdate();
    }
}
