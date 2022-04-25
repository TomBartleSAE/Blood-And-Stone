using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using Anthill.AI;
using UnityEngine;

public class AttackingDefensesState : AntAIState
{
    public SoldierModel soldier;
    public PathfindingAgent pathfinding;

    public GameObject owner;
    public Transform target;

    public bool inRange = false;
    public bool canAttack = true;

    public float attackTime;
    public float damage;

    public override void Create(GameObject aGameobject)
    {
        base.Create(aGameobject);

        owner = aGameobject;
    }
    
    public override void Enter()
    {
        base.Enter();

        soldier = owner.GetComponent<SoldierModel>();
        pathfinding = owner.GetComponent<PathfindingAgent>();
        target = soldier.target;

        canAttack = true;
        
        pathfinding.FindPath(owner.transform.position, target.transform.position);
        
        Debug.Log("Entering Attacking Defenses State");
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        CheckRange();
        
        if (inRange && canAttack)
        {
            StartCoroutine(Attack());
        }
        
        Debug.Log("Executing Attacking Defenses State");
    }

    public override void Exit()
    {
        base.Exit();
        
        Debug.Log("Exiting Attacking Defenses State");
    }

    public void CheckRange()
    {
        float range;
        
        range = Vector3.Distance(transform.position, target.transform.position);

        if (range <= 1)
        {
            inRange = true;
        }
    }

    public IEnumerator Attack()
    {
        canAttack = false;
        
        target.GetComponent<Tom.Health>().ChangeHealth(-damage, owner);
        
        for (int i = 0; i < attackTime; i++)
        {
            yield return new WaitForSeconds(1);
        }

        canAttack = true;
    }
}
