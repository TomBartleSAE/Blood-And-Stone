using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public Tom.Health health;

    private void Awake()
    {
        health.DeathEvent += Die;
    }

    public void Die(GameObject go)
    {
        Destroy(go);
    }
}
