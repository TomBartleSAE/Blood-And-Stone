using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GhoulSpawner : SpawnerBase
{
    //public Transform spawnWaypoint;
    public PathfindingGrid thisGrid;
    public GraphicRaycaster graphicRaycaster;
    
    private void Start()
    {
        //current population of ghouls to spawn
        int amount = PlayerManager.Instance.currentGhouls;
        List<GameObject> Ghouls = SpawnCharacter(thingToSpawn, amount, spawnLocation);

        foreach (var ghoul in Ghouls)
        {
            ghoul.GetComponent<PathfindingAgent>().grid = thisGrid;
            ghoul.GetComponent<GhoulClickMovement>().graphicRaycaster = graphicRaycaster;
            DayNPCManager.Instance.AddToGhoulList(ghoul);
        }
    }
}
