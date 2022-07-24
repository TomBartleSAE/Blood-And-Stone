using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Tanks;
using UnityEngine;

public class GuardDeadState : AntAIState
{
    private GameObject owner;
    private GuardModel guard;

    private GameObject guardView;
    private GameObject ghoulView;
    private GameObject lightConeObject;

    private Vector3 destinationPos;
    private Transform destination;

    public override void Create(GameObject aGameObject)
    {
        base.Create(gameObject);
        owner = aGameObject;
    }

    public override void Enter()
    {
        base.Enter();

        guard = owner.GetComponent<GuardModel>();

        guard.vampire = null;
        lightConeObject = guard.lightConeObject;

        CreateGhoul();
    }

    //preventing entering other states after death
    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        guard.hasTarget = false;
        guard.isAlert = false;
        guard.isPatrolling = false;
        
        base.Execute(aDeltaTime, aTimeScale);
    }

    public override void Exit()
    {
        base.Exit();
    }

    //clears current path, gets path to exit, repeats every few seconds
    IEnumerator GetOutOfTown(Vector3 startPos, Vector3 destPos)
    {
        guard.pathfindingAgent.path.Clear();
        guard.pathfindingAgent.FindPath(startPos, destPos);

        //HACK will continue to get the path every half second. Will hopefully avoid the case of ghouls standing around after death
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(GetOutOfTown(transform.position, destinationPos));
    }

    //changes model; gets the exit Vector3, starts to move off screen
    void CreateGhoul()
    {
        guardView = guard.guardView;
        ghoulView = guard.ghoulView;
        destination = FindObjectOfType<ReturnToCastleTrigger>().transform;
        destinationPos = new Vector3(destination.position.x, destination.position.y - 0.5f, destination.position.z);

        guardView.SetActive(false);
        ghoulView.SetActive(true);
        lightConeObject.SetActive(false);

        StartCoroutine(GetOutOfTown(transform.position, destinationPos));
    }
}
