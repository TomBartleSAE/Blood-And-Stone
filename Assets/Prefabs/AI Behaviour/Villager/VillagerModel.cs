using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Tanks;
using Tom;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class VillagerModel : MonoBehaviour, IStunnable
{
    public RaycastHit hit;

    public float rayDistance = 20;

    public float viewRange;
    
    public bool isScared;
    public bool isStunned;
    public bool isEaten;

    private void Start()
    {
        NPCManager.Instance.Villagers.Add(gameObject);

        GetComponent<Health>().DeathEvent += Die;
        
        NPCManager.Instance.VillagerDeathEvent += Reaction;
    }

    void Die(GameObject me)
    {
        Destroy(me);
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
