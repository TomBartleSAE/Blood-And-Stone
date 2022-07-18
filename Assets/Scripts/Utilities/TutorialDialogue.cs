using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDialogue : TutorialElementBase
{
    [TextArea(3,10)]
    public string dialogue;

    public Sprite portrait;
    public float typingDelay = 0.1f;

    public override void Activate()
    {
        base.Activate();
        
        TutorialManager.Instance.ShowText(dialogue, portrait, typingDelay);
    }
}
