using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Anthill.AI;
using UnityEngine;

public class StunnedState : AntAIState
{
    public GameObject owner;
    public VillagerModel villager;
    
    public float stunTime;

    public bool stunned;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        owner = aGameObject;
        villager = GetComponent<VillagerModel>();
    }

    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("Entering Stunned State");

        //TODO fine tune when steering behaviours added
        //villager.GetComponent<Wander>().enabled = false;
        StartCoroutine(StunnedTimer());
    }
    
    // public override void Execute()
    // {
    //     base.Enter();
    //     
    //     Debug.Log("Executing Stunned State");
    // }

    public override void Exit()
    {
        base.Enter();

        Debug.Log("Exiting Stunned State");
    }

    IEnumerator StunnedTimer()
    {
        for (int i = 0; i < stunTime; i++)
        {
            yield return new WaitForSeconds(1);
        }

        villager.isStunned = false;
        
        Finish();
    }
}
