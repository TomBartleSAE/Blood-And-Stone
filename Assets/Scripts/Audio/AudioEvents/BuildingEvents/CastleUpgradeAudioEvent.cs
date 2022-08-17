using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleUpgradeAudioEvent : MonoBehaviour
{
    public Castle castleObject;
    public string clipName;
    public AudioManager.ArrayName arrayName;

    void OnEnable()
    {
        castleObject.CastleUpgradedEvent += PlayUpgradeAudio;
    }


    void OnDisable()
    {
        castleObject.CastleUpgradedEvent -= PlayUpgradeAudio;
    }

    public void PlayUpgradeAudio()
    {
        AudioManager.Instance.Play(clipName, arrayName);
    }
}
