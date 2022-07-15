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
    public Transform target;
    public GameObject me;

    public bool canAttack;
    //public float attackCooldown; // Move this to the GhoulModel script so it can be changed when ghoul's level up

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        ghoulModel = GetComponentInParent<GhoulModel>();
    }
    
    public override void Enter()
    {
        base.Enter();

        target = ghoulModel.clickMovement.target;
        canAttack = true;

        me = ghoulModel.gameObject;
        
        Attack();
        
        //StartCoroutine(AttackTimer());
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
    }

    public override void Exit()
    {
        base.Exit();

        ghoulModel.hasTarget = false;
    }

    //cooldown timer
    IEnumerator AttackTimer()
    {
        for (int i = 0; i < ghoulModel.attackCooldown; i++)
        {
            yield return new WaitForSeconds(1);
        }

        canAttack = true;
        Attack();
    }

    //deals damage then starts the cooldown timer
    public void Attack()
    {
        int damage = ghoulModel.damage;

        if (canAttack == true)
        {
            target.GetComponent<Health>().ChangeHealth(-damage, me); // Changing health by negative damage means damage values can be positive which makes more sense
            canAttack = false;
        }

        StartCoroutine(AttackTimer());
    }
}
