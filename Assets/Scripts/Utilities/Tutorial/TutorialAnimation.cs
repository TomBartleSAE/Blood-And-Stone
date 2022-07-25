using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAnimation : TutorialElementBase
{
    [System.Serializable]
    public class Animation
    {
        public GameObject character;
        public string animation;
        public Transform positioning;
    }

    public Animation[] animations;
    public float waitTime = 1f;
    
    public override void Activate()
    {
        base.Activate();

        foreach (Animation anim in animations)
        {
            anim.character.transform.position = anim.positioning.position;
            anim.character.transform.rotation = anim.positioning.rotation;
            anim.character.GetComponentInChildren<Animator>().Play(anim.animation);
        }

        StartCoroutine(TutorialManager.Instance.ProgressAfterTime(waitTime));
    }
}
