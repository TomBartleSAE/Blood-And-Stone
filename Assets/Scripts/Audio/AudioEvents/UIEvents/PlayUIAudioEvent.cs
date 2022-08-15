using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayUIAudioEvent : MonoBehaviour
{

    public AudioManager.ArrayName arrayName;



    public void PlaySound(string clipName)
    {
        AudioManager.Instance.Play(clipName, arrayName);
    }

}