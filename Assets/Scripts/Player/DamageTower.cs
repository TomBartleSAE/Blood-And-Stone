using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Tom;

public class DamageTower : TowerBase
{
    public float damage = 1f;
    [Tooltip("Numbers of seconds between firing")]
    public float delay = 1f;
    private float attackTimer;

    public GameObject projectilePrefab;
    public Transform projectileSpawn;
    
    private void Update()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f && targets.Count > 0)
        {
            Attack();
        }
    }

    public void Attack()
    {
        Collider target = targets[Random.Range(0, targets.Count)];
        target.GetComponent<Health>().ChangeHealth(-damage, gameObject);

        GameObject newProjectile = Instantiate(projectilePrefab, projectileSpawn.position, Quaternion.identity);
        newProjectile.GetComponent<Rigidbody>().AddForce((target.transform.position - projectileSpawn.position) * 100f);

        attackTimer = delay;
    }
}
