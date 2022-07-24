using System;
using System.Collections;
using System.Collections.Generic;
using Tanks;
using Tom;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GhoulModel : MonoBehaviour
{
    public Health health;
    private PathfindingAgent pathfinding;
    public ClickMovement clickMovement;
    public BoxSelection boxSelection;

    public bool hasTarget;
    public bool targetAlive;
    public bool castleStanding = true;
    public bool inRange;
    public bool isIdle = true;
    public bool autoAttack;
    
    [SerializeField]
    private bool localAutoAttack;

    public bool LocalAutoAttack
    {
        get
        {
            return localAutoAttack;
        }

        set
        {
            localAutoAttack = value;
            autoAttack = localAutoAttack;
        }
    }

    public int damage;
    public float attackCooldown;
    public float attackRange;

    void Start()
    {
        pathfinding = GetComponent<PathfindingAgent>();
        health = GetComponent<Health>();
        health.DeathEvent += Die;
        clickMovement.HasTargetEvent += HasTargetBoolChange;
    }

    private void OnDestroy()
    {
        DayNPCManager.Instance.RemoveFromGhoulList(gameObject);
    }

    void Update()
    {
        if (autoAttack)
        {
            isIdle = false;
        }
    }

    void Die(GameObject thisDeadThing)
    {
        DayNPCManager.Instance.GhoulDeath();
        Destroy(gameObject);
    }

    //will return to FindTargetState and look for new target
    void TargetDeath(GameObject deadThing)
    {
        hasTarget = false;
        inRange = false;
    }

    public void SetLevel(int level)
    {
        // Use this to set the ghoul's stats to the respective values outlined in the DayNPCManager
        // They're set to "level - 1" to adjust for array element order (Level 1 is element 0)
        damage = DayNPCManager.Instance.ghoulDamageLevels[level - 1];
        health.MaxHealth = DayNPCManager.Instance.ghoulHealthLevels[level - 1];
        attackCooldown = DayNPCManager.Instance.ghoulAttackRateLevels[level - 1];
        GetComponent<FollowPath>().moveSpeed = DayNPCManager.Instance.ghoulMovementSpeedLevels[level - 1];
    }

    public void HasTargetBoolChange(bool value)
    {
        hasTarget = value;
    }
}
