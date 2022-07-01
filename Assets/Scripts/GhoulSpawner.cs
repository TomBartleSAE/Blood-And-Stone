using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GhoulSpawner : SpawnerBase
{
    //public Transform spawnWaypoint;
    
    private void Start()
    {
        //gets total number to spawn including additions from previous night phase
        int amount = NightNPCManager.Instance.ConvertedGhouls.Count + DayNPCManager.Instance.Ghouls.Count;

        SpawnCharacter(thingToSpawn, amount, spawnLocation);
        ThingSpawned += AddToList;
    }

    void AddToList(GameObject ghoul)
    {
        DayNPCManager.Instance.AddToGhoulList(ghoul);
    }
}
