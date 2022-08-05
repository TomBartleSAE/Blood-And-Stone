using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Tanks;
using UnityEngine;

public class GuardDeadState : AntAIState
{
    public GameObject owner;
    public GuardModel guardModel;

    public GameObject guardView;
    public GameObject ghoulView;
    public GameObject lightConeObject;

    public Vector3 destinationPos;
    public Transform destination;

    private float pathTimer = 0.5f;

    public override void Create(GameObject aGameObject)
    {
        base.Create(gameObject);
        owner = aGameObject;
    }

    public override void Enter()
    {
        base.Enter();

        guardModel = owner.GetComponent<GuardModel>();
        destination = guardModel.mapExit;

        guardModel.vampire = null;
        lightConeObject = guardModel.lightConeObject;

        CreateGhoul();
    }

    //preventing entering other states after death
    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        guardModel.hasTarget = false;
        guardModel.isAlert = false;
        guardModel.isPatrolling = false;
        
        base.Execute(aDeltaTime, aTimeScale);
    }

    public override void Exit()
    {
        base.Exit();
    }
    
    //changes model; gets the exit Vector3, starts to move off screen
    void CreateGhoul()
    {
        guardView = guardModel.guardView;
        ghoulView = guardModel.ghoulView;

        guardView.SetActive(false);
        ghoulView.SetActive(true);
        guardModel.GetComponent<WalkAnimation>().anim = ghoulView.GetComponent<Animator>();
        lightConeObject.SetActive(false);
        guardModel.pathfindingAgent.FindPath(transform.position, destination.position);
    }
}
