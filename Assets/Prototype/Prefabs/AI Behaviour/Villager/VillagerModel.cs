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
    public AntAIAgent antAIAgent;
    public Health health;

    public RaycastHit hit;

    public float rayDistance = 20;
    public float moveSpeed;

    public float viewRange;
    
    public bool isScared;
    public bool isStunned;
    public bool isEaten;


    private void Awake()
    {
        health = GetComponent<Health>();
                
        health.DeathEvent += Reaction;
        health.DeathEvent += Die;
    }

    // Start is called before the first frame update
    void Start()
    {
        NPCManager.Instance.Villagers.Add(gameObject);

        foreach (var villager in NPCManager.Instance.Villagers)
        {
            villager.GetComponent<Health>().DeathEvent += Reaction;
        }
    }

    //Reacting to other thing's death
    public void Reaction(GameObject deadThing)
    {
        if (deadThing != gameObject)
        {
            Vector3 targetDirection = transform.position - deadThing.transform.position;
            
            RaycastHit hit;
            
            if (Physics.Raycast( new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), targetDirection, out hit, viewRange))
            {
                isScared = true;
            }   
        }
    }

    public void GetStunned()
    {
        isStunned = true;
    }

    //reacting to own death
    public void Die(GameObject me)
    {
        NPCManager.Instance.Villagers.Remove(gameObject);
        Destroy(gameObject);
    }
}
