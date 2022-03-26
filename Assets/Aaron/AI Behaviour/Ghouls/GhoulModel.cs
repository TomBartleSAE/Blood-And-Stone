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
    public VillagerModel villager;

    public bool hasTarget;
    public bool targetAlive;
    public bool castleStanding = true;
    public bool inRange;
    public bool isIdle;

    public int damage;

    public GameObject target;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnable()
    {
       villager.PersonEatenEvent += RemoveTarget;
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
