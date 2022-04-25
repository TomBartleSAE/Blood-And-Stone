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
    
    private void OnEnable()
    {
        agent.NewPathEvent += ResetPath;
    }

    public void FixedUpdate()
    {
        if (agent.path != null && index < agent.path.Count)
        {
            rb.AddForce((targetNode.coordinates - transform.position).normalized * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);

            if (Vector3.Distance(targetNode.coordinates, transform.position) < 0.1f)
            {
                index++;
                if (index < agent.path.Count)
                {
                    targetNode = agent.path[index];
                }
                print(index + "/" + agent.path.Count);
            }
        }
    }

    public void ResetPath()
    {
        index = 0;
        targetNode = agent.path[0];
    }
}
