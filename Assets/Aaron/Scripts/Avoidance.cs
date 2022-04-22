using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;

namespace Aaron
{
    public class Avoidance : MonoBehaviour
    {
        public Rigidbody rb;

        public float raycastDistance = 20f;

        // Update is called once per frame
        void Update()
        {
            var right45 = (transform.forward + transform.right).normalized;
            var left45 = (transform.forward + -transform.right).normalized;

            var origin = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
            
            //avoids collisions
            if (Physics.Raycast(origin, right45, raycastDistance))
            {
                rb.AddRelativeTorque(0, (1), 0);
            }
            
            if (Physics.Raycast(origin, left45, raycastDistance))
            {
                rb.AddRelativeTorque(0, (-1), 0);
            }

            Debug.DrawRay(origin, right45, Color.yellow);
            Debug.DrawRay(origin, left45, Color.yellow);
        }
    }
}