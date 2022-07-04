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
        //current population of ghouls to spawn
        int amount = PlayerManager.Instance.currentGhouls;
        
        SpawnCharacter(thingToSpawn, amount, spawnLocation);
    }
}
