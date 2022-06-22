using System;
using System.Collections;
using System.Collections.Generic;
using Tanks;
using Tom;
using UnityEngine;

public class DayNPCManager : ManagerBase<DayNPCManager>
{
    public List<GameObject> Ghouls = new List<GameObject>();
    public List<GameObject> Soldiers = new List<GameObject>();

    public event Action GhoulDeathEvent;
    public event Action SoldierDeathEvent;

    public int maxPop;
    public int currentPop;

    // Start is called before the first frame update
    void Start()
    {
        currentPop = Ghouls.Count;

        /*foreach (var soldier in Soldiers)
        {
            GetComponent<Health>().DeathEvent += RemoveFromSoldierList;
        }*/
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
        newSoldier.GetComponent<Health>().DeathEvent += RemoveFromSoldierList;
    }

    public void RemoveFromSoldierList(GameObject soldier)
    {
        SoldierDeathEvent?.Invoke();
        Soldiers.Remove(soldier);
    }
}
