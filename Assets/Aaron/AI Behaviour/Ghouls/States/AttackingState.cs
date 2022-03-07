using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class AttackingState : AntAIState
{
    [SerializeField]
    public List<GameObject> Targets = new List<GameObject>();
    public Transform target;
    
    public override void Enter()
    {
        base.Enter();

        Debug.Log("Entering Attacking State");
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
        Debug.Log("Executing Attacking State");
    }

    public override void Exit()
    {
        base.Exit();
        
        Debug.Log("Exiting Attacking State");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            Debug.Log("Attack State Test");
            Targets.Add(other.gameObject);
        }
    }
}
