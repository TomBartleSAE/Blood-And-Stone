using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClickAudioEvent : MonoBehaviour
{
    public ClickMovement clickMovement;
    public string clipName;
    public AudioManager.ArrayName arrayName;

    public void Start()
    {
        clickMovement.StartedMoveEvent += PlayClickWorld;
        clickMovement.HasTargetEvent += PlayClickNPC;
    }

    public void OnDestroy()
    {
        clickMovement.StartedMoveEvent -= PlayClickWorld;
        clickMovement.HasTargetEvent -= PlayClickNPC;
    }

    public void PlayClickWorld()
    {
        AudioManager.Instance.Play(clipName, arrayName);
    }

    public void PlayClickNPC(bool hasTarget)
    {
        if (hasTarget)
        {
            AudioManager.Instance.Play(clipName, arrayName);
        }
    }


}
