using System;
using System.Collections;
using System.Collections.Generic;
using Tanks;
using UnityEngine;

public class DayNPCManager : ManagerBase<DayNPCManager>
{
    public List<GameObject> Ghouls;
    public List<GameObject> Soldiers;

    public event Action GhoulDeathEvent;

    public int maxPop;
    public int currentPop;

    // Start is called before the first frame update
    void Start()
    {
        Ghouls = new List<GameObject>();
        Soldiers = new List<GameObject>();

        currentPop = Ghouls.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToGhoulList(GameObject newGhoul)
    {
        Ghouls.Add(newGhoul);
    }

    public void RemoveFromGhoulList(GameObject ghoul)
    {
        GhoulDeathEvent?.Invoke();
        Ghouls.Remove(ghoul);
    }

    public void AddToSoldierList(GameObject newSoldier)
    {
        Soldiers.Add(newSoldier);
    }

    public void RemoveFromSoldierList(GameObject soldier)
    {
        Soldiers.Remove(soldier);
    }
}
