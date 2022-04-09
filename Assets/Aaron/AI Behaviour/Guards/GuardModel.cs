using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardModel : MonoBehaviour
{

    public GameObject target;
    
    public enum AlertState
    {
        patrolling,
        investigating
    }

    public bool hasTarget;
    public bool isAlert;
    public bool inRange;
    public bool targetCaptured;
    
    
    // Start is called before the first frame update
    void Start()
    {
        AlertState alertState = AlertState.patrolling;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
