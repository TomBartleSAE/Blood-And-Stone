using System;
using System.Collections;
using System.Collections.Generic;
using Tanks;
using Tom;
using Unity.VisualScripting;
using UnityEngine;

public class GhoulModel : MonoBehaviour
{
    public AttackingState attacking;
    public Health health;
    private PathfindingAgent pathfinding;

    public bool hasTarget;
    public bool targetAlive;
    public bool castleStanding = true;
    public bool inRange;
    public bool isIdle = true;
    public bool autoAttack;

    public bool isSelected;

    public int damage;

    public Transform target;
    public Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        pathfinding = GetComponent<PathfindingAgent>();
        //Put this into NPCManager?
        DayNPCManager.Instance.AddToGhoulList(gameObject);
        pathfinding.grid = FindObjectOfType<PathfindingGrid>();
    }

    // Update is called once per frame
    void Update()
    {
        if (autoAttack)
        {
            isIdle = false;
        }
        
        if (target != null)
        {
            hasTarget = true;
            targetPos = target.position;
            pathfinding.destination = target;
        }
        else if (target == null)
        {
            hasTarget = false;
        }
    }

    
    //will switch to AttackState when in range to attack
    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<SoldierModel>())
        {
            inRange = true;
            other.GetComponent<Health>().DeathEvent += TargetDeath;
        }
    }

    //will go back to MoveToTargetState and find path again to get into range
    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject == target.gameObject)
        {
            inRange = false;
        }
    }

    //will return to FindTargetState and look for new target
    void TargetDeath(GameObject deadThing)
    {
        hasTarget = false;
        inRange = false;
    }
}
