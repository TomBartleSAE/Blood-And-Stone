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
    public BoxSelection boxSelection;
    
    private void Start()
    {
        //current population of ghouls to spawn
        amountToSpawn = PlayerManager.Instance.CurrentGhouls;
        List<GameObject> Ghouls = SpawnCharacter(thingToSpawn);

        foreach (var ghoul in Ghouls)
        {
            ghoul.GetComponent<PathfindingAgent>().grid = thisGrid;
            ghoul.GetComponent<ClickMovement>().graphicRaycaster = graphicRaycaster;
            ghoul.GetComponent<GhoulModel>().boxSelection = boxSelection;
            DayNPCManager.Instance.AddToGhoulList(ghoul);
        }
    }
}
