using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEvents
{
    public static event Action<GameObject> DeathEvent;


    public static void TriggerDeathEvent(GameObject thingThatDied)
    {
        DeathEvent?.Invoke(thingThatDied);
    }
}
