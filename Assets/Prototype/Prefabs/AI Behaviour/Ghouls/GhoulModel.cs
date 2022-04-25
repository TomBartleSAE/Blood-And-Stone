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
    public NPCManager manager;
    private PathfindingAgent pathfinding;

    public bool hasTarget;
    public bool targetAlive;
    public bool castleStanding = true;
    public bool inRange;
    public bool isIdle;

    public int damage;

    public GameObject target;
    public Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        pathfinding = GetComponent<PathfindingAgent>();
        manager.Ghouls.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            hasTarget = true;
            targetPos = target.transform.position;
            pathfinding.destination = target.transform;
        }
    }

    public void OnEnable()
    {
       health.DeathEvent += RemoveTarget;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Health>())
        {
            inRange = true;
            attacking.Targets.Add(other.GameObject());
        }
    }

    public void RemoveTarget(GameObject deadVillager)
    {
        attacking.Targets.Remove(deadVillager);
    }
}
