using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public Health health;

    private void Awake()
    {
        health.DeathEvent += Die;
    }

    public void Die(GameObject go)
    {
        Destroy(go);
    }
}
