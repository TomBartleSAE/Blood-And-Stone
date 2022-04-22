using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;

namespace Aaron
{
    public class Reverse : MonoBehaviour
    {
        public Rigidbody rb;

        public float raycastDistance = 20f;
        public float turn;

        // Update is called once per frame
        void Update()
        {
            //avoids collisions
            if (Physics.Raycast(transform.position, transform.forward, raycastDistance))
            {
                
            }

            Debug.DrawRay(transform.position, transform.forward, Color.yellow);
        }
    }
}