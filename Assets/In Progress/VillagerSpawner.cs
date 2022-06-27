using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerSpawner : SpawnerBase
{
    // Start is called before the first frame update
    void Start()
    {
        SpawnCharacter(thingToSpawn, amountToSpawn, spawnLocation);
        ThingSpawned += AddToList;
    }

    void AddToList(GameObject villager)
    {
        NightNPCManager.Instance.AddToVillagerList(villager);
    }
}
