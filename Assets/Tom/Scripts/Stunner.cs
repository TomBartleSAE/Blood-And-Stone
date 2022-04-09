using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunner : MonoBehaviour
{
    public Collider stunTrigger;

    public void OnTriggerEnter(Collider other)
    {
        IStunnable stunnable = other.gameObject.GetComponent<IStunnable>();

        if (stunnable != null)
        {
            stunnable.GetStunned();
        }
    }
}
