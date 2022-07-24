using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAnimation : MonoBehaviour
{
    public Rigidbody rb;
    public Animator anim;

    public void Update()
    {
        anim.SetFloat("Velocity", rb.velocity.magnitude);
    }
}
