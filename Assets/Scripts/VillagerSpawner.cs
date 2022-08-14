using System.Collections;
using System.Collections.Generic;
using Tom;
using UnityEngine;

public class VillagerSpawner : SpawnerBase
{
    public PathfindingGrid thisGrid;

    void Start()
    {
        List<GameObject> villagerList = SpawnCharacter(thingToSpawn);

        foreach (var villager in villagerList)
        {
            villager.GetComponent<PathfindingAgent>().grid = thisGrid;
            NightNPCManager.Instance.AddToVillagerList(villager);
        }
    }

    public void SpawnVillager(GameObject thing)
    {
        SpawnCharacter(thingToSpawn);
    }
}
