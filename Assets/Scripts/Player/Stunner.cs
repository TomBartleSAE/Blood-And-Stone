using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunner : MonoBehaviour
{
    public ClickMovement movement;

    private void Start()
    {
        movement = GetComponentInParent<ClickMovement>();
    }

    // Moved this to a separate component to prevent collider issues with other trigger zones
    public void OnTriggerEnter(Collider other)
    {
        // Uses trigger zone to stun villagers and guards
        IStunnable stunnable = other.gameObject.GetComponentInParent<IStunnable>();

        //isolating effect to targeted object only
        //if (movement.target != null && movement.target.gameObject == other.gameObject)
        {
            if (stunnable != null)
            {
                stunnable.GetStunned();
            }
        }
    }
}
