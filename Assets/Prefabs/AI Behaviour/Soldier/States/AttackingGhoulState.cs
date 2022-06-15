using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Tanks;
using UnityEngine;

public class AttackingGhoulState : AntAIState
{
    public SoldierModel soldier;
    public PathfindingAgent pathfinding;

    public GameObject owner;
    public Transform target;

    public bool inRange = false;
    public bool canAttack = true;

    public float attackTime;
    public float damage;

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
        target = soldier.target;

        InvokeRepeating("Pathfind", 0, 3);
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        if (inRange && canAttack)
        {
            StartCoroutine(Attack());
        }
        
        CheckRange();
    }

    public override void Exit()
    {
        base.Exit();
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
            inRange = true;
        }
    }

    public IEnumerator Attack()
    {
        target.GetComponent<Tom.Health>().ChangeHealth(-damage, gameObject);
        
        //stops constant attack every frame
        canAttack = false;
        
        for (int i = 0; i < attackTime; i++)
        {
            yield return new WaitForSeconds(1);
        }

        canAttack = true;
    }
}
