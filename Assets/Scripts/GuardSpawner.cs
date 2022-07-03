using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GuardSpawner : SpawnerBase

{
    private void Start()
    {
        SpawnCharacter(thingToSpawn, amountToSpawn, spawnLocation);
        ThingSpawned += AddToList;
    }

    void AddToList(GameObject guard)
    {
        NightNPCManager.Instance.AddToGuardList(guard);
    }
}