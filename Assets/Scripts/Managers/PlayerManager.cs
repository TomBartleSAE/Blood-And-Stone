using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : ManagerBase<PlayerManager>
{
    private SaveData saveData = new SaveData();
    
    [Header("Stats")]
    public int soldiersKilled;
    public int villagersDrained;
    public int ghoulsCreated;

    [Header("Blood")] 
    public int currentBlood;
    public int maxBlood = 50;
    
    [Header("Castle")]
    public int castleLevel = 1;
    public float castleHealth;

    [Header("Ghouls")] 
    public int currentGhouls;
    public int ghoulPopcap;
    
    public int[,] towerLayout = new int[11, 11];
    
    /// <summary>
    /// Sends out how much the blood has changed by
    /// </summary>
    public event Action<int> BloodChangedEvent;

    /// <summary>
    /// Sends out how much the max blood has changed by
    /// </summary>
    public event Action<int> MaxBloodChangedEvent;

    private void Start()
    {
        DayNPCManager.Instance.SoldierDeathEvent += AddSoldierKilled;
        NightNPCManager.Instance.VillagerDeathEvent += AddVillagerDrained;
    }

    public void ChangeBlood(int amount)
    {
        if (currentBlood + amount > maxBlood)
        {
            amount = maxBlood - currentBlood;
        }

        currentBlood += amount;

        BloodChangedEvent?.Invoke(amount);
    }

    public void ChangeMaxBlood(int amount)
    {
        maxBlood += amount;

        Mathf.Clamp(currentBlood, 0, maxBlood);

        MaxBloodChangedEvent?.Invoke(amount);
    }

    public void AddSoldierKilled()
    {
        soldiersKilled++;
    }

    public void AddVillagerDrained(GameObject a)
    {
        villagersDrained++;
    }

    public void ResetStats()
    {
        soldiersKilled = 0;
        villagersDrained = 0;
        ghoulsCreated = 0;
    }

    public void SetSaveData()
    {
        saveData.day = GameManager.Instance.currentDay;

        saveData.soldiersKilled = soldiersKilled;
        saveData.villagersDrained = villagersDrained;
        saveData.ghoulsCreated = ghoulsCreated;
        
        saveData.currentBlood = currentBlood;
        saveData.maxBlood = maxBlood;

        saveData.castleLevel = castleLevel;
        saveData.castleHealth = castleHealth;

        saveData.ghoulCount = currentGhouls;
        saveData.maxGhouls = ghoulPopcap;

        saveData.towerLayout = towerLayout;
    }
}