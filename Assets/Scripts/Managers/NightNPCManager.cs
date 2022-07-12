using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightNPCManager : ManagerBase<NightNPCManager>
{
    public List<GameObject> Villagers = new List<GameObject>();
    public List<GameObject> Guards = new List<GameObject>();
    public List<GameObject> ConvertedGhouls = new List<GameObject>();

    public event Action GhoulDeathEvent;
    public event Action GameOverCaptureEvent;
    public event Action<GameObject> VillagerDeathEvent;
    public event Action GuardAlertEvent;
    public event Action GuardNotAlertEvent;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var guard in Guards)
        {
            guard.GetComponent<GuardModel>().VampireCapturedEvent += VampireCapture;
            guard.GetComponent<GuardModel>().AlertedEvent += GuardAlert;
            guard.GetComponent<GuardModel>().NotAlertedEvent += GuardNotAlert;
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

    //removes from guard list if killed/converted
    public void RemoveFromGuardList(GameObject guard)
    {
        Guards.Remove(guard);
    }
    
    //Fires off game over event
    public void VampireCapture()
    {
        GameOverCaptureEvent?.Invoke();
    }

    public void GuardAlert()
    {
        GuardAlertEvent?.Invoke();
    }

    public void GuardNotAlert()
    {
        GuardNotAlertEvent?.Invoke();
    }
}
