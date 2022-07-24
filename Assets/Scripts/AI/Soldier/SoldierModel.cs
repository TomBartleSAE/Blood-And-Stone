using System;
using System.Collections;
using System.Collections.Generic;
using Tom;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngineInternal;
using Random = UnityEngine.Random;

public class SoldierModel : EnemyBase
{
    public Rigidbody rb;
    public PathfindingAgent pathfinding;
    public Health health;

    public Transform target;
    public Transform castle;

    public List<Transform> TargetsInRange = new List<Transform>();

    public bool hasTarget = false;
    public bool attackedByGhoul = false;
    public bool attackedByTower = false;
    public bool castleStanding = true;
    public bool inRange = false;
    public bool targetAlive = false;

    public float attackCooldown;

    public LayerMask buildingLayer;

    public void OnEnable()
    {
        health.DeathEvent += Die;
        pathfinding.PathFailedEvent += BreakThroughWall;
        health.DamageChangeEvent += AttackTower;
    }

    public void OnDisable()
    {
        health.DeathEvent -= Die;
        pathfinding.PathFailedEvent -= BreakThroughWall;
        health.DamageChangeEvent += AttackTower;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pathfinding = GetComponent<PathfindingAgent>();
        health = GetComponent<Health>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GhoulModel>()) ;
        {
            TargetsInRange.Add(other.transform);
            target = other.transform;
            attackedByGhoul = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<GhoulModel>())
        {
            TargetsInRange.Remove(other.transform);
        }
        
        if (other.gameObject == target.gameObject)
        {
            ChangeTarget();
        }
    }

    public void ChangeTarget()
    {
        if (TargetsInRange.Count > 0)
        {
            target = TargetsInRange[Random.Range(0, TargetsInRange.Count - 1)];
        }

        else
        {
            attackedByGhoul = false;
        }
    }

    void Die(GameObject deadGuy)
    {
        DayNPCManager.Instance.RemoveFromSoldierList(gameObject);
        gameObject.SetActive(false);
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

    void AttackTower(GameObject newTarget)
    {
        if (newTarget.GetComponent<TowerBase>())
        {
            if (!attackedByTower || !attackedByGhoul)
            {
                target = newTarget.transform;
                attackedByTower = true;
            }
        }
    }
}
