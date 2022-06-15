using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    public int currentBlood;
    public int maxBlood = 50;
    
    /// <summary>
    /// Sends out how much the blood has changed by
    /// </summary>
    public event Action<int> BloodChangedEvent;

    /// <summary>
    /// Sends out how much the max blood has changed by
    /// </summary>
    public event Action<int> MaxBloodChangedEvent;

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
}
