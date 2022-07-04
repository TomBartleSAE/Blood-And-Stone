using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerSpawner : SpawnerBase
{
    public PathfindingGrid thisGrid;

    void Start()
    {
        List<GameObject> villagerList = SpawnCharacter(thingToSpawn, amountToSpawn, spawnLocation);

        foreach (var villager in villagerList)
        {
            villager.GetComponent<PathfindingAgent>().grid = thisGrid;
        }
    }
}
