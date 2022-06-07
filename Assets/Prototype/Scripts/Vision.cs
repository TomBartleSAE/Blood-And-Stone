using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    public float angle;
    public float distance;
    public LayerMask obstacleLayers;

    public bool CanSeeObject(Transform target)
    {
        float angleToTarget = Vector3.Angle(transform.forward, target.position);

        if (angleToTarget < angle)
        {
            if (!Physics.Raycast(transform.position, target.position, distance, obstacleLayers))
            {
                return true;
            }
        }

        return false;
    }
}
