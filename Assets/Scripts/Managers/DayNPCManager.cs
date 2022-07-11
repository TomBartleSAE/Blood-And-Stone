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

    public int[] ghoulDamageLevels = new int[4];
    public float[] ghoulHealthLevels = new float[4];
    public float[] ghoulAttackRateLevels = new float[4];
    public float[] ghoulMovementSpeedLevels = new float[4];

    
    // Start is called before the first frame update
    void Start()
    {
        /*foreach (var soldier in Soldiers)
        {
            GetComponent<Health>().DeathEvent += RemoveFromSoldierList;
        }*/
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

    public void AddToGhoulList(GameObject ghoul)
    {
        ghoul.GetComponent<GhoulModel>().SetLevel(PlayerManager.Instance.castleLevel);
        Ghouls.Add(ghoul);
    }

    public void RemoveFromGhoulList(GameObject ghoul)
    {
        Ghouls.Remove(ghoul);
    }
}
