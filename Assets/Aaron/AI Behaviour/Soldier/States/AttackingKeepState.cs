using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Tom;
using UnityEngine;

public class AttackingKeepState : AntAIState
{
    public GameObject owner;

    public GameObject castle;
    public PathfindingAgent pathfinding;
    public SoldierModel soldier;

    public float attackTime;
    public float damage;

    public bool canAttack;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        owner = aGameObject;
    }
    public override void Enter()
    {
        base.Enter();

        soldier = owner.GetComponent<SoldierModel>();
        pathfinding = owner.GetComponent<PathfindingAgent>();

        pathfinding.FindPath(transform.position, castle.transform.position);
        
        Debug.Log("Entering Attacking Keep State");
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        if (Vector3.Distance(transform.position, castle.transform.position) <= 0.5 && canAttack)
        {
            StartCoroutine(AttackCastle());
        }
        
        Debug.Log("Executing Attacking Keep State");
    }

    public override void Exit()
    {
        base.Exit();
        
        Debug.Log("Exiting Attacking Keep State");
    }

    public IEnumerator AttackCastle()
    {
        castle.GetComponent<Health>().ChangeHealth(damage);
        canAttack = false;
        
        for (int i = 0; i < attackTime; i++)
        {
            yield return new WaitForSeconds(1);
        }

        canAttack = true;
    }

}
