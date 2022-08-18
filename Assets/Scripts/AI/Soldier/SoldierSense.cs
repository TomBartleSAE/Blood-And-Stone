using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class SoldierSense : MonoBehaviour, ISense
{
    public enum SoldierBools
    {
        hasTarget = 0,
        attackedByGhoul = 1,
        attackedByTower = 2,
        castleStanding = 3,
        inRange = 4,
        targetAlive = 5,
        castlePathBlocked = 6
    }
    
    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.BeginUpdate(aAgent.planner);

        aWorldState.Set(SoldierBools.hasTarget, aAgent.GetComponent<SoldierModel>().hasTarget);
        aWorldState.Set(SoldierBools.attackedByGhoul, aAgent.GetComponent<SoldierModel>().attackedByGhoul);
        aWorldState.Set(SoldierBools.attackedByTower, aAgent.GetComponent<SoldierModel>().attackedByTower);
        aWorldState.Set(SoldierBools.castleStanding, aAgent.GetComponent<SoldierModel>().castleStanding);
        aWorldState.Set(SoldierBools.inRange, aAgent.GetComponent<SoldierModel>().inRange);
        aWorldState.Set(SoldierBools.targetAlive, aAgent.GetComponent<SoldierModel>().targetAlive);
        aWorldState.Set(SoldierBools.castlePathBlocked, aAgent.GetComponent<SoldierModel>().castlePathBlocked);
        
        aWorldState.EndUpdate();
    }
}
