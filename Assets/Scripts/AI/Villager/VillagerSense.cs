using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class VillagerSense : MonoBehaviour, ISense
{
    public enum VillagerBools
    {
        isScared = 0,
        isStunned = 1,
        isEaten = 2
    }

    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.BeginUpdate(aAgent.planner);

        aWorldState.Set(VillagerBools.isScared, aAgent.GetComponent<VillagerModel>().isScared);
        aWorldState.Set(VillagerBools.isStunned, aAgent.GetComponent<VillagerModel>().isStunned);
        aWorldState.Set(VillagerBools.isEaten, aAgent.GetComponent<VillagerModel>().isEaten);
        
        aWorldState.EndUpdate();
    }
}
