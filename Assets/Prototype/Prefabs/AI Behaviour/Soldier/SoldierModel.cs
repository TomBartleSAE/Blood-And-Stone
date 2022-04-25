using System;
using System.Collections;
using System.Collections.Generic;
using Tom;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngineInternal;

public class SoldierModel : EnemyBase
{
    public Rigidbody rb;
    public PathfindingAgent pathfinding;
    public Health health;

    public Transform target;
    public Transform castle;

    public bool hasTarget = false;
    public bool attackedByGhoul = false;
    public bool attackedByTower = false;
    public bool castleStanding = true;
    public bool inRange = false;
    public bool targetAlive = false;

    public LayerMask buildingLayer;

    public void OnEnable()
    {
        health.DamageChangeEvent += ChangeTarget;
        health.DeathEvent += Die;
        pathfinding.PathFailedEvent += BreakThroughWall;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pathfinding = GetComponent<PathfindingAgent>();
        health = GetComponent<Health>();
        
        NPCManager.Instance.Soldiers.Add(gameObject);
    }

    public void ChangeTarget(GameObject newTarget)
    {
        target = newTarget.transform;

        if (target.GetComponent<GhoulModel>())
        {
            attackedByGhoul = true;
        }

        if (target.GetComponent<TowerBase>())
        {
            attackedByTower = true;
        }
    }

    void Die(GameObject me)
    {
        NPCManager.Instance.Soldiers.Remove(gameObject);
        
        Destroy(gameObject);
    }
    
    public void BreakThroughWall()
    {

        Collider[] towers = Physics.OverlapSphere(transform.position, 100, buildingLayer);

        foreach (var building in towers)
        {
            float shortestDistance = 100000;
            float distance = Vector3.Distance(transform.position, building.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                target = building.transform;
            }
        }
        
        //will change to AttackingDefensesState
        attackedByTower = true;
    }
}