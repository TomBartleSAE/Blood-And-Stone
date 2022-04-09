using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Tom;
using Unity.VisualScripting;
using UnityEngine;

public class VillagerModel : MonoBehaviour, IStunnable
{
    public AntAIAgent antAIAgent;
    public VillagerModel villager;
    public NPCManager manager;
    public Health health;

    public RaycastHit hit;

    public float rayDistance = 20;
    public float moveSpeed;
    
    public bool isScared;
    public bool isStunned;
    public bool isEaten;

    // Start is called before the first frame update
    void Start()
    {
        villager = GetComponent<VillagerModel>();
        antAIAgent = GetComponent<AntAIAgent>();
        antAIAgent.SetGoal("Survive");
        manager = FindObjectOfType<NPCManager>();
        health = GetComponentInParent<Health>();

        health.DeathEvent += Reaction;
        manager.Villagers.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //Will add/finetune when Health Component Added
        if (GetComponent<Health>().currentHealth <= 0)
        {
            manager.Villagers.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    public void Reaction(GameObject deadThing)
    {
        villager.isScared = true;
    }

    public void GetStunned()
    {
        isStunned = true;
    }
    
    
}
