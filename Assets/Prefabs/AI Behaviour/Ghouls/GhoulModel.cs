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
    public float attackCooldown;

    public Transform target;
    public Vector3 targetPos;

    //not sure about this working
    public bool IsSelected
    {
        get
        {
            return isSelected;
        }
        set
        {
            if (isSelected == false)
            {
                isSelected = true;
                DayNPCManager.Instance.GhoulSelected();
            }

            if (isSelected == true)
            {
                isSelected = false;
                DayNPCManager.Instance.GhoulNotSelected();
            }

        }
    }
    
    void Start()
    {
        pathfinding = GetComponent<PathfindingAgent>();
        pathfinding.grid = FindObjectOfType<PathfindingGrid>();
    }
    
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

    public void SetLevel(int level)
    {
        // Use this to set the ghoul's stats to the respective values outlined in the DayNPCManager
        // They're set to "level - 1" to adjust for array element order (Level 1 is element 0)
        damage = DayNPCManager.Instance.ghoulDamageLevels[level - 1];
        health.MaxHealth = DayNPCManager.Instance.ghoulHealthLevels[level - 1];
        attackCooldown = DayNPCManager.Instance.ghoulAttackRateLevels[level - 1];
        GetComponent<FollowPath>().moveSpeed = DayNPCManager.Instance.ghoulMovementSpeedLevels[level - 1];
    }

    
}
