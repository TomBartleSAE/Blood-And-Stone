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
    public ClickMovement clickMovement;

    public bool canAttack;

    public float timer;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        ghoulModel = GetComponentInParent<GhoulModel>();
        clickMovement = ghoulModel.clickMovement;
    }
    
    public override void Enter()
    {
        base.Enter();

        target = ghoulModel.target;
        
        //cooldown situation
        canAttack = true;

        ghoulModel.newGhoulTargetEvent += NewTarget;
        
        target.GetComponent<Health>().DeathEvent += TargetDead;
        
        /*if (target.GetComponent<Health>().currentHealth > 0)
        {
            ghoulModel.targetAlive = true;
        }*/

        Attack();
    }

    private void OnDisable()
    {
        ghoulModel.newGhoulTargetEvent -= NewTarget;
        target.GetComponent<Health>().DeathEvent -= TargetDead;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        timer -= Time.deltaTime;
        if (timer <= 0 && ghoulModel.inRange)
        {
            canAttack = true;
            Invoke(nameof(Attack), 1.25f);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    //deals damage then starts the cooldown timer
    public void Attack()
    {
        int damage = ghoulModel.damage;

        if (canAttack)
        {
            target.GetComponent<Health>().ChangeHealth(-damage, gameObject); // Changing health by negative damage means damage values can be positive which makes more sense
            canAttack = false;
            timer = ghoulModel.attackCooldown;
        }
    }

    public void TargetDead(GameObject deadThing)
    {
	    //HACK - should I set up a function or event that updates all at once?
        target = null;
        ghoulModel.target = null;
        clickMovement.target = null;
        ghoulModel.hasTarget = false;
        ghoulModel.inRange = false;
        ghoulModel.targetAlive = false;
        ghoulModel.isIdle = true;
    }

    public void NewTarget(Transform newTarget)
    {
	    target = newTarget;
	    ghoulModel.targetAlive = true;
	    ghoulModel.isIdle = false;
    }
}
