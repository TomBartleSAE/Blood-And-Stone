using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Tanks;
using Tom;
using UnityEngine;
using Random = UnityEngine.Random;

public class VillagerModel : MonoBehaviour, IStunnable
{
    public RaycastHit hit;
    public Rigidbody rb;

    public float rayDistance = 20;

    public float viewRange;
    
    public bool isScared;
    public bool isStunned;
    public bool isEaten;

    private void Start()
    {
        NightNPCManager.Instance.Villagers.Add(gameObject);

        rb = GetComponent<Rigidbody>();
        GetComponent<Health>().DeathEvent += Die;
        NightNPCManager.Instance.VillagerDeathEvent += Reaction;
    }
    void Die(GameObject me)
    {
        NightNPCManager.Instance.RemoveFromVillagerList(me);
        gameObject.SetActive(false);
    }

    //Reacting to other thing's death
    public void Reaction(GameObject deadThing)
    {
        Vector3 targetDirection = transform.position - deadThing.transform.position;
        
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
