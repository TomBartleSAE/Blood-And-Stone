using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunner : MonoBehaviour
{
    // Moved this to a separate component to prevent collider issues with other trigger zones
    public void OnTriggerEnter(Collider other)
    {
        // Uses trigger zone to stun villagers and guards
        IStunnable stunnable = other.gameObject.GetComponentInParent<IStunnable>();

        if (stunnable != null)
        {
            stunnable.GetStunned();
        }
    }
}
