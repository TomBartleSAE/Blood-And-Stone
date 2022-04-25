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
    public NPCManager manager;
    public Health health;

    public RaycastHit hit;

    public float rayDistance = 20;
    public float moveSpeed;

    public float viewRange;
    
    public bool isScared;
    public bool isStunned;
    public bool isEaten;

    public bool testBool;

    public event Action<GameObject> HackTestDeathEvent;

    // Start is called before the first frame update
    void Start()
    {
        antAIAgent = GetComponent<AntAIAgent>();
        antAIAgent.SetGoal("Survive");
        manager = FindObjectOfType<NPCManager>();
        health = GetComponentInParent<Health>();
        
        manager.Villagers.Add(gameObject);

        
        health.DeathEvent += Reaction;
        health.DeathEvent += Die;


        //GlobalEvents.DeathEvent += Reaction;

        foreach (var villager in manager.Villagers)
        {
            villager.GetComponent<Health>().DeathEvent += Reaction;
        }

        //for test
        if (testBool)
        {
            StartCoroutine(CertainDeath());
        }

    }

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
    
    //hacked in test
    public IEnumerator CertainDeath()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1);
        }
        
        health.ChangeHealth(-20, gameObject);
        Debug.Log(gameObject);
    }

    public void Die(GameObject me)
    {
        manager.Villagers.Remove(gameObject);
        Destroy(gameObject);
    }
}
