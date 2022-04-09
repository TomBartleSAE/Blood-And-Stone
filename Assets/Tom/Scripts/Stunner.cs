using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunner : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        IStunnable stunnable = other.gameObject.GetComponentInParent<IStunnable>();

        if (stunnable != null)
        {
            stunnable.GetStunned();
        }
    }
}
