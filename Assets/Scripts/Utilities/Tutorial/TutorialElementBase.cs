using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialElementBase : MonoBehaviour
{
    public UnityEvent tutorialEvent;
    
    public virtual void Activate()
    {
        tutorialEvent.Invoke();
    }
}
