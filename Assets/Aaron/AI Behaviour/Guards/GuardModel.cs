using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Anthill.AI;
using Tanks;
using Tom;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class GuardModel : MonoBehaviour
{
    public AntAIAgent antAIAgent;
    public NPCManager npcManager;
    public Health health;
    public PathfindingAgent pathfindingAgent;
    public PathfindingGrid grid;
    
    public GameObject InvestigateTarget;
    public GameObject chaseTarget;

    public event Action VampireCapturedEvent;

    public bool hasTarget;
    public bool isAlert;
    public bool inRange;
    public bool targetCaptured;

    public float guardRange;
    public float captureTime;
    public float viewRange;
    public float searchCooldown;
    public float investigationTime;

    public Vector3 patrolPointA;
    public Vector3 patrolPointB;

    // Start is called before the first frame update
    void Start()
    {
        antAIAgent = GetComponent<AntAIAgent>();
        antAIAgent.SetGoal("CaptureTarget");
        npcManager = FindObjectOfType<NPCManager>();
        pathfindingAgent = GetComponent<PathfindingAgent>();
        grid = FindObjectOfType<PathfindingGrid>();

        //need to sub to all health death events
//        health.DeathEvent += Reaction;

        npcManager.Guards.Add(gameObject);

        GetPatrolPoints();
    }

    public void Reaction(GameObject deadThing)
    {
        Debug.Log("Guard Reacting ok");
        isAlert = true;
        
        //TODO clear this out maybe
        if (deadThing == this)
        {
            npcManager.Guards.Remove(this.gameObject);
            Destroy(this.gameObject);
        }

        else
        {
            InvestigateTarget = deadThing;
            Investigate(deadThing);
        }
    }

    public void GetPatrolPoints()
    {
        int gridRange = grid.gridSize.x;

        Vector3 pointA;
        Vector3 pointB;
        
        pointA = new Vector3(Random.Range(0, gridRange), 0.2f, Random.Range(0, gridRange));
        pointB = new Vector3(Random.Range(pointA.x - 10, pointA.x +10), 0.2f, Random.Range(pointA.z - 10, pointA.z + 10));

        patrolPointA = pointA;
        patrolPointB = pointB;
    }
    
    public void Investigate(GameObject target)
    {
        pathfindingAgent.FindPath(this.transform.position, target.transform.position);
    }

    public void VampireCaptured()
    {
        VampireCapturedEvent?.Invoke();
    }

    public void OnTriggerStay(Collider other)
    {
        //do we need a better identifying ref for the vampire?
        if (other.GetComponent<ClickMovement>())
        {
            //direction to Vampire
            Vector3 targetDirection = transform.position - other.transform.position;
            
            RaycastHit hit;
            
            if (Physics.Raycast( new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), targetDirection, out hit, viewRange))
            {
                hasTarget = true;
                chaseTarget = other.gameObject;
            }
        }
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
