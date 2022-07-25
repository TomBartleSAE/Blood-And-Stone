using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TutorialAreaTrigger : MonoBehaviour
{
    public GameObject requiredObject;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == requiredObject)
        {
            TutorialManager.Instance.Progress();
        }
        
        gameObject.SetActive(false);
    }
}
