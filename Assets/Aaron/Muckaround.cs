using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Muckaround : MonoBehaviour
{
    public List<GameObject> Targets = new List<GameObject>();

    private StateBase stateBase;

    // Start is called before the first frame update
    void Start()
    {
        stateBase = new AttackingState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        other = GetComponentInChildren<SphereCollider>();
        
        //Need to swap out for a more precise identifier
        if (other.GetComponent<Rigidbody>())
        {
            Debug.Log("Muckaround Test");
            Targets.Add(other.gameObject);
            
            GetComponent<StateManager>().ChangeState(stateBase);
        }
    }
}
