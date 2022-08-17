using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulAudioEvent : MonoBehaviour
{
    public ClickMovement clickMovement;
    public string clipName;
    public AudioManager.ArrayName arrayName;

    void OnEnable()
    {
        clickMovement.HasTargetEvent += PlayGhoulAttackAudio;
        clickMovement.StartedMoveEvent += PlayGhoulMoveAudio;
    }


    void OnDisable()
    {
        clickMovement.HasTargetEvent -= PlayGhoulAttackAudio;
        clickMovement.StartedMoveEvent -= PlayGhoulMoveAudio;
    }

    public void PlayGhoulMoveAudio()
    {
        int index = UnityEngine.Random.Range(1, 3);
        AudioManager.Instance.Play(clipName + index, arrayName);
    }
    public void PlayGhoulAttackAudio(bool hasTarget)
    {
        if (hasTarget)
        {
            int index = UnityEngine.Random.Range(1, 3);
            AudioManager.Instance.Play(clipName + index, arrayName);
        }
    }
}

