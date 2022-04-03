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
    public GameObject target;
    public NPCManager manager;

    public bool hasTarget = false;
    public bool attackedByGhoul = false;
    public bool attackedByTower = false;
    public bool castleStanding = true;
    public bool inRange = false;
    public bool targetAlive = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pathfinding = GetComponent<PathfindingAgent>();
        health = GetComponent<Health>();
    }

    private void Update()
    {
    
    }
}
