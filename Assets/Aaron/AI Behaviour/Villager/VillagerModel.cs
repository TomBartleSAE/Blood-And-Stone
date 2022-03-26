using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Tom;
using Unity.VisualScripting;
using UnityEngine;

public class VillagerModel : MonoBehaviour
{
    public AntAIAgent antAIAgent;
    public VillagerModel villager;
    public VillagerManager manager;

    public event Action<GameObject> PersonEatenEvent;

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
        manager = FindObjectOfType<VillagerManager>();

        PersonEatenEvent += Reaction;
        manager.Villagers.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //Will add/finetune when Health Component Added
        if (GetComponent<Health>().currentHealth <= 0)
        {
            PersonEatenEvent?.Invoke(this.gameObject);

            manager.Villagers.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    public void Reaction(GameObject deadThing)
    {
        
        //logic to check if in sight
        
        
        villager.isScared = true;
    }
    
    /*public void FireDeathEvent()
    {
        PersonEatenEvent?.Invoke(this.transform.position);
        
        Debug.Log("Event Fired");
    }*/
}
