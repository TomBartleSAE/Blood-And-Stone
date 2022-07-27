using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Tom;
using UnityEngine;

public class AttackingKeepState : AntAIState
{
    public GameObject owner;

    public Transform castle;
    public PathfindingAgent pathfinding;
    public SoldierModel soldier;

    public float attackTime;
    public float damage;

    public bool canAttack;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        owner = aGameObject;
        soldier = owner.GetComponent<SoldierModel>();
        pathfinding = owner.GetComponent<PathfindingAgent>();
    }
    public override void Enter()
    {
        base.Enter();

        castle = soldier.castle;
        
        pathfinding.FindPath(owner.transform.position, castle.position);
        
        Debug.Log("Entering Attacking Keep State");
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        if (Vector3.Distance(owner.transform.position, castle.position) <= 0.5f && canAttack)
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
        print("attack castle");
        canAttack = false;
        castle.GetComponentInParent<Health>().ChangeHealth(-damage, owner);
        soldier.anim.SetTrigger("Attack");

        for (int i = 0; i < attackTime; i++)
        {
            yield return new WaitForSeconds(1);
        }

        canAttack = true;
    }

}
