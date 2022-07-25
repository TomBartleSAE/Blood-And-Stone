using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Tanks;
using UnityEngine;

public class GuardDeadState : AntAIState
{
    private GameObject owner;
    private GuardModel guardModel;

    private GameObject guardView;
    private GameObject ghoulView;
    private GameObject lightConeObject;

    private Vector3 destinationPos;
    private Transform destination;

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

        //finds path to map exit every 0.5 seconds
        pathTimer -= Time.deltaTime;
        if (pathTimer <= 0)
        {
            guardModel.pathfindingAgent.FindPath(transform.position, destination.position);
            pathTimer = 0.5f;
        }
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
        lightConeObject.SetActive(false);
    }
}
