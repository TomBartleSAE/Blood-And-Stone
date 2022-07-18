using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAction : TutorialElementBase
{
    public RectTransform clickArea;
    public GameObject clickVisual;

    public override void Activate()
    {
        base.Activate();
        
        TutorialManager.Instance.textBox.SetActive(false);
        clickVisual?.SetActive(true);
        TutorialManager.Instance.activeClickArea = clickArea;
    }
}
