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
    }

    public override void SpawnCharacter(GameObject thing, int amount, Vector3 location)
    {
        base.SpawnCharacter(thing, amount, location);
    }
}
