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

    public float distance;
    public LayerMask obstacleLayer;
    
    private void OnEnable()
    {
        agent.NewPathEvent += ResetPath;
    }

    public void FixedUpdate()
    {
        
        if (agent.path != null && targetNode != null)
        {
            if (index < agent.path.Count)
            {
                rb.AddForce((targetNode.coordinates - transform.position).normalized * moveSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);
                
                if (Vector3.Distance(targetNode.coordinates, transform.position) < 0.25f)
                {
                    index++;
                    if (index < agent.path.Count)
                    {
                        targetNode = agent.path[index];
                        target = targetNode.coordinates;
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

                rb.AddTorque(Vector3.up * turnDirection  * turnSpeed * Time.fixedDeltaTime,
                    ForceMode.VelocityChange);
            }
        }
        
        /*//total angle of arc, divide arcAngle by 2 to get relative angle from forward
        float arcAngle = 90f;
        //number of rays to be dispersed
        int numLines = 90;

        float turnOffset;
        
        for (int l=0;l<numLines;l++) 
        {
            //establishes ray arc
            //(Line taken from website - AM)
            Vector3 shootVec = transform.rotation * Quaternion.AngleAxis(-1*arcAngle/2+(l*arcAngle/numLines), Vector3.up) * Vector3.forward;
            
            RaycastHit hit;
            
            if (Physics.Raycast(transform.position, shootVec, out hit, distance, obstacleLayer))
            {
                if (l == 0 || l == 44 || l == 89)
                {
                    Debug.DrawLine(transform.position, hit.point, Color.green);
                    Vector3 hitLocalPos = transform.InverseTransformPoint(hit.point);
                    turnOffset = hit.point.x;
                }
            }
        }*/
    }
    

    public void ResetPath()
    {
        index = 0;
        targetNode = agent.path[0];
    }
}
