using System;
using System.Collections;
using System.Collections.Generic;
using Tom;
using UnityEngine;

public class VampireModel : MonoBehaviour
{
    public ClickMovement movement;
    public Blood blood;

    public float feedRange = 0.6f; // Slightly larger than distance when touching to ensure feeding is triggered
    public int bloodGain = 10;
    
    public void OnTriggerEnter(Collider other)
    {
        // Uses trigger zone to stun villagers and guards
        IStunnable stunnable = other.gameObject.GetComponentInParent<IStunnable>();

        if (stunnable != null)
        {
            stunnable.GetStunned();
        }
    }

    private void Update()
    {
        if (movement.target != null)
        {
            if (Vector3.Distance(transform.position, movement.target.position) < feedRange)
            {
                Feed(movement.target.gameObject);
            }
        }
    }

    public void Feed(GameObject victim)
    {
        // Play animations on vampire and victim
        blood.ChangeBlood(bloodGain);
        movement.target = null;
        victim.GetComponent<Health>().ChangeHealth(-1f, gameObject); // Kills the victim
    }
}
