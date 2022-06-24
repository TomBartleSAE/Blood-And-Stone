using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerBase : MonoBehaviour
{
    public GameObject thingToSpawn;
    public int amountToSpawn;
    public Transform spawnLocation;

    public event Action spawnFinish;

    public virtual void SpawnCharacter(GameObject thing, int amount, Transform location)
    {
        GameObject go = thing;

        for (int i = 0; i < amountToSpawn; i++)
        {
            Vector3 spawnPointOffset = new Vector3(spawnLocation.position.x + Random.Range(-0.5f, 0.5f), spawnLocation.position.y, spawnLocation.position.z + Random.Range(-0.5f, 0.5f));
            Instantiate(go, location.position = spawnPointOffset, thing.transform.rotation);
        }

        spawnFinish?.Invoke();
    }
}
