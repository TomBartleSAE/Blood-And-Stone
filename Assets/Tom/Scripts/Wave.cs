using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Wave : ScriptableObject
{
    [Serializable]
    public class Group
    {
        public GameObject enemy;
        public int numberToSpawn;
        public float timeToNextGroup;
    }

    public Group[] groups = new Group[1];
}
