using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionIndicator : MonoBehaviour
{
    public Material ghoulMaterial;
    // Start is called before the first frame update
    public void EnableIndicator()
    {
        enabled = true;
        //GetComponentInChildren<MeshRenderer>().material.color = Color.red;
    }

    public void DisableIndicator()
    {
        enabled = false;
        //GetComponentInChildren<MeshRenderer>().material = ghoulMaterial;
    }
}