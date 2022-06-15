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
    public List<GameObject> ConvertedGuards = new List<GameObject>();

    public Transform[] SpawnPoints;

    //ghoul pop cap
    public int maxGhoulCapacity;
    public int currentGhoulAmount;

    //temp variables
    public GameObject villagerToSpawn;
    public GameObject guardToSpawn;
    public int amountToSpawn;
    public GhoulSpawner ghoulSpawner;

    public event Action<GameObject> VillagerDeathEvent;
    public event Action GhoulDeathEvent;

    private void Start()
    {
        foreach (var guard in Guards)
        {
            guard.GetComponent<GuardModel>().NewConversionEvent += AddConversion;
        }
    }

    private void Update()
    {
        currentGhoulAmount = Ghouls.Count;
    }

    #region Villagers
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
    
    void Sub(GameObject villager)
    {
        villager.GetComponent<Health>().DeathEvent += RemoveVillagerFromList;
    }
    #endregion

    void AddGhoulToList(GameObject newGhoul)
    {
        Ghouls.Add(newGhoul);
    }

    void AddGuardToList(GameObject newGuard)
    {
        Guards.Add(newGuard);
    }

    void RemoveGhoulFromList(GameObject ghoul)
    {
        GhoulDeathEvent?.Invoke();
        Ghouls.Remove(ghoul);
    }

    //temp list for guards converted through the night phase
    void AddConversion(GameObject newConversion)
    {
        ConvertedGuards.Add(newConversion);
    }
}
