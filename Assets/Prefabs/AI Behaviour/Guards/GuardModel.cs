using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Anthill.AI;
using DG.Tweening;
using Tom;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class GuardModel : MonoBehaviour
{
    public Health health;
    public PathfindingAgent pathfindingAgent;
    public Vision vision;
    public Rigidbody rb;

    public Transform investigateTarget;
    public Transform vampire;

    public GameObject guardView;
    public GameObject ghoulView;
    public GameObject lightCone;
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
    public float searchCooldown;
    public float investigationTime;
    public float hearingRange;
    
    public event Action<GameObject> NewConversionEvent;
    public event Action CapturedVampireEvent;

    // Start is called before the first frame update
    void Start()
    {
        NightNPCManager.Instance.AddToGuardList(gameObject);
        
        pathfindingAgent = GetComponent<PathfindingAgent>();
        health = GetComponent<Health>();
        vision = GetComponent<Vision>();
        //Would love to not have FindObject
        mapExit = GameObject.Find("Return to Castle Trigger");
        vampire = FindObjectOfType<VampireModel>().transform;
        rb = GetComponent<Rigidbody>();
        
        isPatrolling = true;
        NightNPCManager.Instance.VillagerDeathEvent += Reaction;

        GetComponent<Health>().DeathEvent += CheckGhoulCapacity;

        //TODO get reference to own Guard and Ghoul objects
    }

    public void Update()
    {
        //TODO sort this out
        //if they see the vampire, will straight away enter chase state
        if (vision.CanSeeObject(vampire))
        {
            hasTarget = true;
        }

        if (isDead)
        {
            NightNPCManager.Instance.VillagerDeathEvent -= Reaction;
        }

        if (investigateTarget == null)
        {
            return;
        }
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
    
    public void VampireCaptured()
    {
        VampireCapturedEvent?.Invoke();
    }

    #region Guard to Ghoul Conversion

    public void CheckGhoulCapacity(GameObject thing)
    {
        NightNPCManager.Instance.Guards.Remove(gameObject);
        //gets current population capacity situation
        int ghoulMax = DayNPCManager.Instance.maxPop;
        int ghoulCurrent = NightNPCManager.Instance.currentPop;

        //creates a ghoul if space; otherwise gets rid of the guard
        if (ghoulCurrent < ghoulMax)
        {
            GhoulConversion();
        }

        else
        {
            gameObject.SetActive(false);
            NightNPCManager.Instance.RemoveFromGuardList(gameObject);
        }
    }
    
    public void GhoulConversion()
    {
        //bools change the state
        isPatrolling = false;
        isDead = true;
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<FollowPath>().moveSpeed = 3;
        NightNPCManager.Instance.AddToConvertedGhoulList(gameObject);
    }

    #endregion


    public void CapturedVampire()
    {
        CapturedVampireEvent?.Invoke();
    }
    
}
