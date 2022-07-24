using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialArea : TutorialElementBase
{
    public TutorialAreaTrigger area;

    private void Awake()
    {
        area.gameObject.SetActive(false);
    }

    public override void Activate()
    {
        base.Activate();
        
        area.gameObject.SetActive(true);

        area.requiredObject.GetComponent<ClickMovement>().enabled = true; // HACK way to turn on player movement when needed
    }
}
