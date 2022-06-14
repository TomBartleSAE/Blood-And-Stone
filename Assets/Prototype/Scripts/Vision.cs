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

    public void OnDrawGizmosSelected()
    {
        Quaternion leftRayRotation = Quaternion.AngleAxis(-angle, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(angle, Vector3.up);

        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;
        
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, leftRayDirection * distance);
        Gizmos.DrawRay(transform.position, rightRayDirection * distance);
        Gizmos.DrawLine(transform.position + leftRayDirection * distance, transform.position + rightRayDirection * distance);
    }
}
