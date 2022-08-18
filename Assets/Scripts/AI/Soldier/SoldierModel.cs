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
    public Animator anim;
    public Rigidbody rb;
    public PathfindingAgent pathfinding;
    public Health health;

    public Transform target;
    public Transform castle;

    public List<Transform> GhoulsInRange = new List<Transform>();

    public bool hasTarget = false;
    public bool attackedByGhoul = false;
    public bool attackedByTower = false;
    public bool castleStanding = true;
    public bool inRange = false;
    public bool targetAlive = false;
    public bool castlePathBlocked;

    public float attackCooldown;
    public float range = 1f;
    public float damage;

    public LayerMask buildingLayer;

    public void OnEnable()
    {
        health.DeathEvent += Die;
        health.DamageChangeEvent += Retaliate;
    }

    public void OnDisable()
    {
        health.DeathEvent -= Die;
        health.DamageChangeEvent += Retaliate;
        pathfinding.grid.GridGeneratedEvent -= FindNewTarget;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pathfinding = GetComponent<PathfindingAgent>();
        health = GetComponent<Health>();
    }

    private void Start()
    {
        pathfinding.grid.GridGeneratedEvent += FindNewTarget;
    }

    private void Update()
    {
	    if (target != null)
	    {
		    if (Vector3.Distance(transform.position, target.position) < range)
		    {
			    inRange = true;
		    }
		    else
		    {
			    inRange = false;
		    }
	    }
    }

    private void OnTriggerEnter(Collider other)
    {
	    if (other.GetComponent<GhoulModel>() && !GhoulsInRange.Contains(other.transform))
        {
            GhoulsInRange.Add(other.transform);
            target = other.transform;
            attackedByGhoul = true;
            hasTarget = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<GhoulModel>())
        {
            GhoulsInRange.Remove(other.transform);
        }
        
        if (other.gameObject == target.gameObject)
        {
            ChangeTarget();
        }
    }

    public void ChangeTarget()
    {
        if (GhoulsInRange.Count > 0)
        {
            target = GhoulsInRange[Random.Range(0, GhoulsInRange.Count)];
        }
        else
        {
	        hasTarget = false;
	        attackedByGhoul = false;
        }
    }

    void Die(GameObject deadGuy)
    {
        DayNPCManager.Instance.RemoveFromSoldierList(deadGuy);
        // Tom: Turning off all gameplay components instead of hiding object entirely
        anim.SetTrigger("Death");
        pathfinding.enabled = false;
        GetComponent<FollowPath>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Health>().enabled = false;
        enabled = false;
        //gameObject.SetActive(false);
    }

    void Retaliate(GameObject newTarget)
    {
        if (newTarget.GetComponent<GhoulModel>())
        {
	        target = newTarget.transform;
	        attackedByGhoul = true;
	        hasTarget = true;
        }
    }

    public void FindNewTarget()
    {
        hasTarget = false;
        target = null;
        inRange = false;
        castlePathBlocked = false;
    }
}
