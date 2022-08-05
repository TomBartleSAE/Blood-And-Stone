using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightNPCManager : ManagerBase<NightNPCManager>
{
    public List<GameObject> Villagers = new List<GameObject>();
    public List<GameObject> Guards = new List<GameObject>();
    public event Action<GameObject> VillagerDeathEvent;
    public event Action<GameObject> GuardDeathEvent;
    public event Action<bool> GuardAlertEvent;

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

    //removes from guard list if killed/converted
    public void RemoveFromGuardList(GameObject guard)
    {
        GuardDeathEvent?.Invoke(guard);
        Guards.Remove(guard);
    }

    public void GuardAlert(bool value)
    {
        foreach (var guard in Guards)
        {
            if (guard.GetComponent<GuardModel>().isAlert)
            {
                GuardAlertEvent?.Invoke(true);
                return;
            }
        }
        
        GuardAlertEvent?.Invoke(false);
    }
}
