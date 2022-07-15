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
    public Vision vision;
    public Rigidbody rb;

    public Transform investigateTarget;
    public Transform vampire;

    public GameObject guardView;
    public GameObject ghoulView;
    public VisionLightCone lightCone;
    public GameObject mapExit;
    public GameObject lightConeObject;
    
    public event Action GetPatrolPointsEvent;
    public event Action AlertedEvent;
    public event Action NotAlertedEvent;

    public bool hasTarget = false;
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

    public GameObject[] waypoints;
    
    // Start is called before the first frame update
    void Start()
    {
        lightCone = GetComponentInChildren<VisionLightCone>();
        lightConeObject = GetComponentInChildren<VisionLightCone>().gameObject;

        //TODO assign variable in guard Spawner and get from there in place of FindObject
        mapExit = GameObject.Find("Return to Castle Trigger");
        vampire = FindObjectOfType<VampireModel>().transform;
        
        isPatrolling = true;
        
        //reaction to villager dying
        NightNPCManager.Instance.VillagerDeathEvent += Reaction;
        
        //reaction to own death event
        GetComponent<Health>().DeathEvent += CheckGhoulCapacity;
        
        //TODO get reference to own Guard and Ghoul objects
    }

    public void Update()
    {
        //if they see the vampire, will straight away enter chase state
        if (vision.CanSeeObject(vampire))
        {
            hasTarget = true;
            isAlert = true;
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
            PlayerManager.Instance.CurrentGhouls += 1;
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
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<FollowPath>().moveSpeed = 3;
        //adds to list; unsubs from death event to prevent reacting while dead
        NightNPCManager.Instance.RemoveFromGuardList(gameObject);
        NightNPCManager.Instance.VillagerDeathEvent -= Reaction;
    }

    #endregion
    
    public void CapturedVampire()
    {
        GameManager.Instance.GetComponentInChildren<NightPhaseState>().GameOverCapture();
    }

    //Alert events for UI interaction
    public void IsAlerted()
    {
        NightNPCManager.Instance.GuardAlert(true);
    }
    
    public void NotAlertedAnymore()
    {
        NightNPCManager.Instance.GuardAlert(false);
    }
}
