using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Anthill.AI;
using UnityEngine;

public class AttackingDefensesState : AntAIState
{
    public SoldierModel soldier;
    public PathfindingAgent pathfinding;

    public GameObject owner;
    public GameObject target;

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
        
        InvokeRepeating("Pathfind", 0, 3);
        
        Debug.Log("Entering Attacking Defenses State");
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

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

    public void Pathfind()
    {
        pathfinding.FindPath(transform.position, target.transform.position);
    }

    public void CheckRange()
    {
        float range;
        
        range = Vector3.Distance(transform.position, target.transform.position);

        if (range <= 0.5f)
        {
            canAttack = true;
        }
    }

    public IEnumerator Attack()
    {
        target.GetComponent<Tom.Health>().ChangeHealth(-damage, gameObject);

        canAttack = false;

        for (int i = 0; i < attackTime; i++)
        {
            yield return new WaitForSeconds(1);
        }

        canAttack = true;
    }
}
