using System;
using System.Collections;
using System.Collections.Generic;
using Tom;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngineInternal;

public class SoldierModel : MonoBehaviour
{
    public Rigidbody rb;
    public PathfindingAgent pathfinding;
    public Health health;
    public NPCManager manager;

    public Transform target;
    public Transform castle;

    public bool hasTarget = false;
    public bool attackedByGhoul = false;
    public bool attackedByTower = false;
    public bool castleStanding = true;
    public bool inRange = false;
    public bool targetAlive = false;


    public void OnEnable()
    {
        health.DamageChangeEvent += ChangeTarget;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pathfinding = GetComponent<PathfindingAgent>();
        health = GetComponent<Health>();

        manager.Soldiers.Add(this.gameObject);
    }

    private void Update()
    {
        
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
}
