using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Tanks;
using Tom;
using UnityEngine;

public class VillagerModel : MonoBehaviour, IStunnable
{
    public PathfindingAgent pathfinding;

    public float moveSpeed;

    public float viewRange;
    
    public bool isScared;
    public bool isStunned;
    public bool isEaten;

    public float fleeTime;
    public float fleeSpeed;
    public float stunTime;

    public Animator anim;

    private void Start()
    {
        GetComponent<Health>().DeathEvent += Die;
        NightNPCManager.Instance.VillagerDeathEvent += Reaction;
    }

    private void OnDisable()
    {
        GetComponent<Health>().DeathEvent -= Die;
        NightNPCManager.Instance.VillagerDeathEvent -= Reaction;
    }

    void Die(GameObject me)
    {
        NightNPCManager.Instance.RemoveFromVillagerList(me);
        // Tom: Trying to get villager to play animation when dying
        GetComponent<Collider>().enabled = false;
        GetComponent<PathfindingAgent>().enabled = false;
        GetComponent<FollowPath>().enabled = false;
        anim.SetTrigger("Death");
        //this.enabled = false;
        //gameObject.SetActive(false);
    }

    //Reacting to other thing's death
    public void Reaction(GameObject deadThing)
    {
        Vector3 targetDirection = transform.position - deadThing.transform.position;
        
        RaycastHit hit;
        if (Physics.Raycast( new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), targetDirection, out hit, viewRange))
        {
            isScared = true;
        }
    }

    public void GetStunned()
    {
        isStunned = true;
    }
}
