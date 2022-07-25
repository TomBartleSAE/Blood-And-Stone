using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GuardSpawner : SpawnerBase
{
    public PathfindingGrid thisGrid;
    public GameObject[] waypoints;
    public Transform vampire;
    public Transform villageExit;

    private void Start()
    {
        List<GameObject> guardList = SpawnCharacter(thingToSpawn);

        foreach (var guard in guardList)
        {
            guard.GetComponent<PathfindingAgent>().grid = thisGrid;
            guard.GetComponent<GuardModel>().waypoints = waypoints;
            guard.GetComponent<GuardModel>().vampire = vampire;
            guard.GetComponent<GuardModel>().mapExit = villageExit;
            NightNPCManager.Instance.AddToGuardList(guard);
        }
    }
}
