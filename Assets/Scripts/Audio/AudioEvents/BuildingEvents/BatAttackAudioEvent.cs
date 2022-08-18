using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAttackAudioEvent : MonoBehaviour
{
    public WeakeningTower weakeningTower;
    public string clipName;
    public AudioManager.ArrayName arrayName;

    void OnEnable()
    {
 //       weakeningTower.EnemyBeingWeakened += PlayAttackAudio;
    }


    void OnDisable()
    {
  //      weakeningTower.EnemyBeingWeakened -= PlayAttackAudio;
    }

    public void PlayAttackAudio()
    {
        int index = UnityEngine.Random.Range(1, 3);
        AudioManager.Instance.Play(clipName + index, arrayName);
    }
}
