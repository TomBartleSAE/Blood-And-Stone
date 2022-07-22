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

    public float timer;

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
        target.GetComponent<Health>().DeathEvent += TargetDead;
        if (target.GetComponent<Health>().currentHealth > 0)
        {
            ghoulModel.targetAlive = true;
        }

        me = ghoulModel.gameObject;
        
        Attack();
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
<<<<<<< Updated upstream
            canAttack = true;
            Attack();
=======
            Invoke(nameof(Attack), 1.25f);
>>>>>>> Stashed changes
        }

        float distance = Vector3.Distance(ghoulModel.transform.position, target.position);
        if (distance > ghoulModel.attackRange || target == null)
        {
            ghoulModel.inRange = false;
        }
    }

    public override void Exit()
    {
        base.Exit();

        //ghoulModel.hasTarget = false;
    }

    //deals damage then starts the cooldown timer
    public void Attack()
    {
        int damage = ghoulModel.damage;

        if (canAttack && ghoulModel.targetAlive)
        {
            target.GetComponent<Health>().ChangeHealth(-damage, me); // Changing health by negative damage means damage values can be positive which makes more sense
            canAttack = false;
            timer = ghoulModel.attackCooldown;
        }
    }

    public void TargetDead(GameObject deadThing)
    {
        target = null;
        ghoulModel.hasTarget = false;
        ghoulModel.inRange = false;
        ghoulModel.targetAlive = false;
        ghoulModel.isIdle = true;
    }
}
