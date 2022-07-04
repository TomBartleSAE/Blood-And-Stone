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
    
    
    //FOR TURN TOWARDS
    private Vector3 cross;
    private Vector3 targetLocalPosition;
    public Vector3 target;
    public float turnSpeed = 5f;
    
    private void OnEnable()
    {
        agent.NewPathEvent += ResetPath;
    }

    public void FixedUpdate()
    {
        if (agent.path != null)
        {
            if (index < agent.path.Count)
            
            {
                rb.AddForce((targetNode.coordinates - transform.position).normalized * moveSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);
                //TODO Rotate towards
                if (Vector3.Distance(targetNode.coordinates, transform.position) < 0.5f)
                {
                    index++;
                    if (index < agent.path.Count)
                    {
                        targetNode = agent.path[index];
                        target = targetNode.coordinates;
                    }
                }
            }
        }
            
            
        
        
        //TURN TOWARDS
        targetLocalPosition = transform.InverseTransformPoint(target);
        float turnDirection = targetLocalPosition.x;
            
        // Prevents object going straight when facing the exact opposite direction
        // by checking if the target is directly behind it in the local Z axis
        if (turnDirection < 0.01f && turnDirection > -0.01f && targetLocalPosition.z < 0)
        {
            turnDirection = 1f;
        }

        rb.AddTorque(Vector3.up * turnDirection * turnSpeed * Time.fixedDeltaTime,
            ForceMode.VelocityChange);
    }
    

    public void ResetPath()
    {
        index = 0;
        targetNode = agent.path[0];
    }
}
