using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Tom;
using Unity.VisualScripting;
using UnityEngine;

public class AttackingState : AntAIState
{
    public GhoulModel ghoulModel;

    public float attackCooldown;
    
    public List<GameObject> Targets = new List<GameObject>();

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        ghoulModel = GetComponentInParent<GhoulModel>();
    }
    
    public override void Enter()
    {
        base.Enter();

        attackCooldown = 3;
        Attack();
        
        StartCoroutine(AttackTimer());
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        if (Targets == null)
        {
            ghoulModel.hasTarget = false;
        }
    }

    public override void Exit()
    {
        base.Exit();

        ghoulModel.hasTarget = false;
    }

    IEnumerator AttackTimer()
    {
        for (int i = 0; i < attackCooldown; i++)
        {
            yield return new WaitForSeconds(1);
        }

        Attack();
    }

    public void Attack()
    {
        int damage = ghoulModel.damage;
        
        foreach (var target in Targets)
        {
            target.GetComponent<Health>().ChangeHealth(damage, gameObject);
        }

        StartCoroutine(AttackTimer());
    }
}
