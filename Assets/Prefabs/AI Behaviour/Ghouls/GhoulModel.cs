using System;
using System.Collections;
using System.Collections.Generic;
using Tanks;
using Tom;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GhoulModel : MonoBehaviour
{
    public Health health;
    private PathfindingAgent pathfinding;
    public GameObject toggle;

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
        pathfinding.grid = FindObjectOfType<PathfindingGrid>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected)
        {
            GetComponent<GhoulClickMovement>().enabled = true;
            toggle.SetActive(true);
            isIdle = true;
        }
        else
        {
            GetComponent<GhoulClickMovement>().enabled = false;
            toggle.SetActive(false); 
        }
        
        if (autoAttack)
        {
            isIdle = false;
        }
        
        if (target != null)
        {
            isIdle = false;
            hasTarget = true;
            targetPos = target.position;
            
            //TODO GetDistance for target range etc
        }
        else if (target == null)
        {
            hasTarget = false;
        }
    }

    
    //will switch to AttackState when in range to attack
    /*public void OnTriggerEnter(Collider other)
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
    }*/

    //will return to FindTargetState and look for new target
    void TargetDeath(GameObject deadThing)
    {
        hasTarget = false;
        inRange = false;
    }
}
