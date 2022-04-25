using System;
using System.Collections;
using System.Collections.Generic;
using Tanks;
using UnityEngine;

public class NPCManager : ManagerBase<NPCManager>
{
    public List<GameObject> Villagers = new List<GameObject>();
    public List<GameObject> Ghouls = new List<GameObject>();
    public List<GameObject> Soldiers = new List<GameObject>();
    public List<GameObject> Guards = new List<GameObject>();
}
