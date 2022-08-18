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
    [Header("Object References")]
    public Health health;
    public PathfindingAgent pathfindingAgent;
    public Vision vision;
    public Rigidbody rb;
    public Transform mapExit;
    public GameObject lightConeObject;

    [Header("Investigation")]
    public Transform investigateTarget;
    public Transform vampire;
    
    [Header("View Objects")]
    public GameObject guardView;
    public GameObject ghoulView;
    public VisionLightCone lightCone;
    
    public event Action GetPatrolPointsEvent;
    public event Action AlertedEvent;
    public event Action NotAlertedEvent;
    public event Action CapturedVampireEvent;
    public event Action GhoulConversionEvent;

    [Header("State Bools")]
    public bool hasTarget = false;
    public bool isAlert;
    public bool inRange;
    public bool targetCaptured;
    public bool isPatrolling;
    public bool isDead = false;

    [Header("Various Floats")] 
    public float patrolSpeed = 2.5f;
    public float investigatingSpeed = 5f;
    public float chasingSpeed = 8f;
    public float guardRange;
    public float captureTime;
    public float searchCooldown;
    public float investigationTime;
    public float hearingRange;

    [Header("Patrol Waypoints")]
    public GameObject[] waypoints;
    
    // Start is called before the first frame update
    void Start()
    {
        lightCone = GetComponentInChildren<VisionLightCone>();
        lightConeObject = GetComponentInChildren<VisionLightCone>().gameObject;

        isPatrolling = true;
        
        //reaction to villager dying
        NightNPCManager.Instance.VillagerDeathEvent += Reaction;
        
        //reaction to own death event
        GetComponent<Health>().DeathEvent += CheckGhoulCapacity;

        mapExit.GetComponent<ReturnToCastleTrigger>().VampireExitEvent += StopMoving;
    }

    private void OnDisable()
    {
        NightNPCManager.Instance.VillagerDeathEvent -= Reaction;
        GetComponent<Health>().DeathEvent -= CheckGhoulCapacity;
        mapExit.GetComponent<ReturnToCastleTrigger>().VampireExitEvent -= StopMoving;
    }

    #region Investigation
    
    //reacting to villager/guard death; will go to investigate
    public void Reaction(GameObject deadThing)
    {
        if (Vector3.Distance(transform.position, deadThing.transform.position) < hearingRange)
        {
            isAlert = true;
            isPatrolling = false;
        
            investigateTarget = deadThing.transform;
        }
    }
    
    #endregion
    
    #region Guard to Ghoul Conversion

    public void CheckGhoulCapacity(GameObject thing)
    {
        NightNPCManager.Instance.Guards.Remove(gameObject);
        //gets current population capacity situation
        int ghoulMax = PlayerManager.Instance.GhoulPopcap;
        int ghoulCurrent = PlayerManager.Instance.CurrentGhouls;

        //creates a ghoul if space; otherwise gets rid of the guard
        if (ghoulCurrent < ghoulMax)
        {
            GhoulConversion();
            PlayerManager.Instance.CurrentGhouls++;
        }

        else
        {
            gameObject.SetActive(false);
        }
    }
    
    public void GhoulConversion()
    {
        //bools change the state
        isPatrolling = false;
        isDead = true;
        GetComponent<FollowPath>().moveSpeed = 3;
        //adds to list; unsubs from death event to prevent reacting while dead
        NightNPCManager.Instance.RemoveFromGuardList(gameObject);
        NightNPCManager.Instance.VillagerDeathEvent -= Reaction;
        //Destroy(gameObject.GetComponent<GuardModel>());
        GhoulConversionEvent?.Invoke();
        GetComponent<Health>().DeathEvent -= CheckGhoulCapacity;
    }

    #endregion
    
    public void CapturedVampire()
    {
	    GameManager.Instance.GameOverMessage("You were captured by the town guard...");
        vampire.GetComponent<VampireModel>().Captured();
        CapturedVampireEvent?.Invoke();
    }

    //Alert events for UI interaction
    public void IsAlerted()
    {
        NightNPCManager.Instance.GuardAlert(true);
        AlertedEvent?.Invoke();
    }
    
    public void NotAlertedAnymore()
    {
        NightNPCManager.Instance.GuardAlert(false);
        NotAlertedEvent?.Invoke();
    }

    //hack
    void StopMoving()
    {
	    Rigidbody rb = GetComponent<Rigidbody>();
	    rb.isKinematic = true;
    }
}
