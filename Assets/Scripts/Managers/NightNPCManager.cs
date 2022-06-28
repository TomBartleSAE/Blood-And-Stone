using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightNPCManager : ManagerBase<NightNPCManager>
{
    public List<GameObject> Villagers;
    public List<GameObject> Guards;
    public List<GameObject> ConvertedGhouls;
    
    public event Action GhoulDeathEvent;
    public event Action GameOverCaptureEvent;
    public event Action<GameObject> VillagerDeathEvent;

    [SerializeField]
    private int popCap;
    public int currentPop;
    
    // Start is called before the first frame update
    void Start()
    {
        Villagers = new List<GameObject>();
        Guards = new List<GameObject>();
        ConvertedGhouls = new List<GameObject>();

        currentPop = DayNPCManager.Instance.Ghouls.Count;
        popCap = DayNPCManager.Instance.maxPop;
        
        foreach (var guard in Guards)
        {
            guard.GetComponent<GuardModel>().NewConversionEvent += AddToConvertedGhoulList;
            guard.GetComponent<GuardModel>().CapturedVampireEvent += VampireCapture;
        }

        foreach (var villager in Villagers)
        {
            villager.GetComponent<Tom.Health>().DeathEvent += RemoveFromVillagerList;
        }
    }

    public void AddToVillagerList(GameObject newVillager)
    {
        Villagers.Add(newVillager);
    }

    public void RemoveFromVillagerList(GameObject villager)
    {
        //Lets other subs know about dead villager; removes from list
        VillagerDeathEvent?.Invoke(villager);
        Villagers.Remove(villager);
    }

    public void AddToGuardList(GameObject newGuard)
    {
        Guards.Add(newGuard);
    }

    public void RemoveFromGuardList(GameObject guard)
    {

        Guards.Remove(guard);
    }

    public void AddToConvertedGhoulList(GameObject newGhoul)
    {
        ConvertedGhouls.Add(newGhoul);
        currentPop += 1;
    }

    public void RemoveFromConvertedGhoulList(GameObject ghoul)
    {
        ConvertedGhouls.Remove(ghoul);
        currentPop -= 1;
    }

    public void VampireCapture()
    {
        GameOverCaptureEvent?.Invoke();
    }
}
