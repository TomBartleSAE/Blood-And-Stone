using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Tanks;
using UnityEngine;

public class GuardDeadState : AntAIState
{
    public GameObject owner;
    public GuardModel guard;

    public GameObject guardView;
    public GameObject ghoulView;
    public GameObject lightCone;
    
    public Vector3 destinationPos;
    public Vector3 currentPos;
    public Transform destination;

    public override void Create(GameObject aGameObject)
    {
        base.Create(gameObject);
        owner = aGameObject;
    }

    public override void Enter()
    {
        base.Enter();

        guard = owner.GetComponent<GuardModel>();

        CreateGhoul();
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        currentPos = transform.position;

        guard.hasTarget = false;
        guard.isAlert = false;
        guard.isPatrolling = false;
        
        base.Execute(aDeltaTime, aTimeScale);
    }

    public override void Exit()
    {
        base.Exit();
    }

    void GetOutOfTown(Vector3 startPos, Vector3 destPos)
    {
        guard.pathfindingAgent.path.Clear();
        guard.pathfindingAgent.destination = destination;
        guard.pathfindingAgent.FindPath(startPos, destPos);
    }

    void CreateGhoul()
    {
        guardView = guard.guardView;
        ghoulView = guard.ghoulView;
        lightCone = guard.lightCone;
        destination = FindObjectOfType<ReturnToCastleTrigger>().transform;
        destinationPos = new Vector3(destination.position.x, destination.position.y - 0.5f, destination.position.z);

        guardView.SetActive(false);
        ghoulView.SetActive(true);
        lightCone.SetActive(false);

        GetOutOfTown(transform.position, destinationPos);
    }
}
