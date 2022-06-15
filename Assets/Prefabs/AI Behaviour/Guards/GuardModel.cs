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

    public GameObject guardView;
    public GameObject ghoulView;
    public GameObject mapExit;

    public event Action VampireCapturedEvent;
    public event Action GetPatrolPointsEvent;

    public bool hasTarget;
    public bool isAlert;
    public bool inRange;
    public bool targetCaptured;
    public bool isPatrolling;
    public bool isDead = false;

    public float guardRange;
    public float captureTime;
    public float viewRange;
    public float searchCooldown;
    public float investigationTime;
    
    public event Action<GameObject> NewConversionEvent;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        AddToList();
        
        pathfindingAgent = GetComponent<PathfindingAgent>();
        health = GetComponent<Health>();
        mapExit = GameObject.Find("Return to Castle Trigger");
        
        isPatrolling = true;
        GetPatrolPointsEvent?.Invoke();

        GetComponent<Health>().DeathEvent += CheckGhoulCapacity;

        //TODO get reference to own Guard and Ghoul objects
    }

    #region Investigation
    public void Reaction(GameObject deadThing)
    {
        isAlert = true;
        
        if (deadThing != this)
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
    #endregion

    void CheckGhoulCapacity(GameObject thing)
    {
        NPCManager.Instance.Guards.Remove(gameObject);
        int ghoulMax = NPCManager.Instance.maxGhoulCapacity;
        int ghoulCurrent = NPCManager.Instance.currentGhoulAmount;

        if (ghoulCurrent < ghoulMax)
        {
            GhoulConversion();
        }

        else
        {
            Destroy(gameObject);
        }
    }
    
    public void GhoulConversion()
    {
        isPatrolling = false;
        isDead = true;
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<FollowPath>().moveSpeed = 3;
        GameObject ghoul = ghoulView;
        NewConversionEvent?.Invoke(ghoul);
    }

    void AddToList()
    {
        NPCManager.Instance.Guards.Add(gameObject);
    }
}
