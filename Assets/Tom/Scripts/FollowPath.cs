using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public PathfindingAgent agent;
    public Rigidbody rb;

    public float moveSpeed = 1f;

    private Node targetNode;
    private int index;

    private bool init = false;

    public void FixedUpdate()
    {
        if (agent.path != null)
        {
            // HACK
            if (!init)
            {
                index = 0;
                targetNode = agent.path[index];
                init = true;
            }
            
            rb.AddForce((targetNode.coordinates - transform.position) * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);

            if (Vector3.Distance(targetNode.coordinates, transform.position) < 0.5f)
            {
                index++;
                targetNode = agent.path[index];
            }
        }
    }
}
