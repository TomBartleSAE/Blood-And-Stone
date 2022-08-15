using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldClickAudioEvent : MonoBehaviour
{
    public ClickMovement clickMovement;
    public string clipName;
    public AudioManager.ArrayName arrayName;

    public void Start()
    {
        clickMovement.StartedMoveEvent += PlayClick;
    }

    public void OnDestroy()
    {
        clickMovement.StartedMoveEvent -= PlayClick;
    }

    public void PlayClick()
    {
        AudioManager.Instance.Play(clipName, arrayName);
    }


}
