using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerBase : MonoBehaviour
{
    public GameObject thingToSpawn;
    public int amountToSpawn;
    public Transform[] SpawnPoints;
    public Vector3 spawnLocation;

    public event Action<GameObject> ThingSpawned;

    public event Action spawnFinish;

    public virtual List<GameObject> SpawnCharacter(GameObject thing, int amount, Vector3 location)
    {
        List<GameObject> listOfThings = new List<GameObject>();

        for (int i = 0; i < amountToSpawn; i++)
        {
            //Get random waypoint from array; set spawn location
            Transform spawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length - 1)];
            spawnLocation = spawnPoint.position;
            Vector3 spawnPointOffset = new Vector3(spawnLocation.x + Random.Range(-0.5f, 0.5f), 
                spawnLocation.y, spawnLocation.z + Random.Range(-0.5f, 0.5f));
            //Spawn thing
            GameObject thingSpawned = Instantiate(thing, location = spawnPointOffset, thing.transform.rotation);
            listOfThings.Add(thingSpawned);
        }

        spawnFinish?.Invoke();

        return listOfThings;
    }
}