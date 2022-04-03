using System.Collections;
using System.Collections.Generic;
using Tanks;
using Unity.VisualScripting;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    public GameObject itemToSpawn;
    public int amountToSpawn;
    public NPCManager manager;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            Instantiate(itemToSpawn);
            itemToSpawn.transform.position = new Vector3(i, 0.5f, this.transform.position.z);
            manager.Soldiers.Add(itemToSpawn);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
