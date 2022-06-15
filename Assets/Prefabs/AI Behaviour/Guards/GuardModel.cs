using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Anthill.AI;
using Tom;
using UnityEngine;
using Random = UnityEngine.Random;

public class GuardModel : MonoBehaviour
{
    public Health health;
    public PathfindingAgent pathfindingAgent;

    public Transform investigateTarget;
    public Transform chaseTarget;

    public event Action VampireCapturedEvent;
    public event Action GetPatrolPointsEvent;

    public bool hasTarget;
    public bool isAlert;
    public bool inRange;
    public bool targetCaptured;
    public bool isPatrolling;

    public float guardRange;
    public float captureTime;
    public float viewRange;
    public float searchCooldown;
    public float investigationTime;

    // Start is called before the first frame update
    void Start()
    {
        pathfindingAgent = GetComponent<PathfindingAgent>();
        health = GetComponent<Health>();

        //TODO probably move this to NPCManager/spawn code
        NPCManager.Instance.Guards.Add(gameObject);

        isPatrolling = true;
        GetPatrolPointsEvent?.Invoke();


        //TODO sub to death events for alert
    }

    public void Reaction(GameObject deadThing)
    {
        Debug.Log("Guard Reacting ok");
        isAlert = true;
        
        //TODO clear this out maybe
        if (deadThing == this)
        {
            NPCManager.Instance.Guards.Remove(this.gameObject);
            Destroy(this.gameObject);
        }

        else
        {
            investigateTarget = deadThing.transform;
            Investigate(deadThing);
        }
    }

    public void Investigate(GameObject target)
    {
        pathfindingAgent.FindPath(this.transform.position, target.transform.position);
    }

    public void VampireCaptured()
    {
        VampireCapturedEvent?.Invoke();
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ClickMovement>())
        {
            hasTarget = false;
            chaseTarget = null;
        }
    }
}
