using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class GuardSense : MonoBehaviour, ISense
{
    public enum GuardBools
    {
        
    }

    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.BeginUpdate(aAgent.planner);
        
        
        
        aWorldState.EndUpdate();
    }
}
