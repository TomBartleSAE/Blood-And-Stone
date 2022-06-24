using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GhoulSpawner : SpawnerBase
{
    private void Start()
    {
        //gets total number to spawn including additions from previous night phase
        int amount = NightNPCManager.Instance.ConvertedGhouls.Count + DayNPCManager.Instance.Ghouls.Count;

        //SpawnCharacter(thingToSpawn, amount, spawnLocation);
    }

    public override void SpawnCharacter(GameObject thing, int amount, Transform location)
    {
        base.SpawnCharacter(thing, amount, location);
    }
}
