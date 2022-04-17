using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Tanks;
using Tom;
using Unity.VisualScripting;
using UnityEngine;

public class GuardModel : MonoBehaviour
{
    public AntAIAgent antAIAgent;
    public NPCManager npcManager;
    public GameObject target;
    public Health health;
    public PathfindingAgent pathfindingAgent;
    public PathfindingGrid grid;

    public bool hasTarget;
    public bool isAlert;
    public bool inRange;
    public bool targetCaptured;

    public Vector3 patrolPointA;
    public Vector3 patrolPointB;

    // Start is called before the first frame update
    void Start()
    {
        antAIAgent = GetComponent<AntAIAgent>();
        antAIAgent.SetGoal("CaptureTarget");
        npcManager = FindObjectOfType<NPCManager>();
        health = GetComponentInParent<Health>();
        pathfindingAgent = GetComponent<PathfindingAgent>();
        grid = FindObjectOfType<PathfindingGrid>();

        transform.position = patrolPointA;

        health.DeathEvent += Reaction;
        
        npcManager.Guards.Add(this.GameObject());

        GetPatrolPoints();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reaction(GameObject deadThing)
    {
        isAlert = true;
        
        if (deadThing == this)
        {
            npcManager.Guards.Remove(this.gameObject);
            Destroy(this.gameObject);
        }

        else
        {
            target = deadThing;
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
}
