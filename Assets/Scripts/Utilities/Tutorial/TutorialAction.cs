using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAction : TutorialElementBase
{
    public TutorialManager.MouseButton mouseButton;
    
    public RectTransform clickArea;
    public GameObject clickVisual;

    public override void Activate()
    {
        base.Activate();

        if (clickVisual != null)
        {
            clickVisual.SetActive(true);
        }
        TutorialManager.Instance.activeClickArea = clickArea;
        TutorialManager.Instance.mouseButton = mouseButton;
    }
}
