using System;
using System.Collections;
using System.Collections.Generic;
using Tanks;
using Tom;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCManager : ManagerBase<NPCManager>
{
    public List<GameObject> Villagers = new List<GameObject>();
    public List<GameObject> Ghouls = new List<GameObject>();
    public List<GameObject> Soldiers = new List<GameObject>();
    public List<GameObject> Guards = new List<GameObject>();

    public Transform[] SpawnPoints;

    public GameObject villagerToSpawn;
    
    public int amountToSpawn;

    public GhoulSpawner ghoulSpawner;

    public event Action<GameObject> VillagerDeathEvent;


    private void OnEnable()
    {
        if (ghoulSpawner != null)
        {
            ghoulSpawner.GhoulSpawnedEvent += AddGhoulToList;
        }
    }

    private void Start()
    {
        SpawnVillagers();
    }

    //adds to list when villager spawned
    void AddVillagerToList(GameObject villager)
    {
        Villagers.Add(villager);
        //villager.GetComponent<Tom.Health>().DeathEvent += RemoveVillagerFromList;
    }

    //removes from list when villager dies; lets everyone know about the death
    void RemoveVillagerFromList(GameObject villager)
    {
        VillagerDeathEvent?.Invoke(villager);
        Villagers.Remove(villager);
    }
    
    //temp spawning code
    void SpawnVillagers()
    {
        GameObject go = villagerToSpawn;
        
        for (int i = 0; i < amountToSpawn; i++)
        {
            int randomSpawn = Mathf.RoundToInt(Random.Range(0, SpawnPoints.Length));
            Instantiate(go, SpawnPoints[randomSpawn].position, villagerToSpawn.transform.rotation);
            AddVillagerToList(go);
            Sub(go);
        }
    }

    void Sub(GameObject villager)
    {
        villager.GetComponent<Health>().DeathEvent += RemoveVillagerFromList;
    }

    void AddGhoulToList(GameObject ghoulToAdd)
    {
        Ghouls.Add(ghoulToAdd);
    }
}
