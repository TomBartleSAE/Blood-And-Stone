using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSwap : MonoBehaviour
{
    public Transform newTransform;
    public GameObject newObject;
    public List<GameObject> ThingsToSwap = new List<GameObject>();
    
    public void SwapObject()
    {
        foreach (var thing in ThingsToSwap)
        {
            newTransform = thing.GetComponent<Transform>();
            GameObject copy = newObject;
            Instantiate(newObject, newTransform.position, newTransform.rotation);
            Destroy(thing);
        }
    }
}
