using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAudioEvent : MonoBehaviour
{
    [HideInInspector]
    public AudioManager.ArrayName arrayName;

    [HideInInspector]
    public string clipName;

    private GameObject playerCharacter;

    public bool isPlayerCharacter = false;

    private bool isNightPhase;

    // The player character variables are just to find the attenuation for the night time phase, as in day time the is no vampire character to find.

    public void Start()
    {

        isNightPhase = AudioManager.Instance.currentPhase == "nightTest";
        if (isNightPhase)
        {
            if (!isPlayerCharacter)
            {
                playerCharacter = FindObjectOfType<VampireModel>().gameObject;
            }
        }
    }




    public void PlayFootstep(string character)
    {
        int index;
        switch (character)
        {
            case "Player":
                arrayName = AudioManager.ArrayName.playerfootsteps;
                index = UnityEngine.Random.Range(0, AudioManager.Instance.playerFootstepsSounds.Length);
                clipName = AudioManager.Instance.playerFootstepsSounds[index].soundName;
                break;
            case "Villager":
                arrayName = AudioManager.ArrayName.villagerfootsteps;
                index = UnityEngine.Random.Range(0, AudioManager.Instance.villagerFootstepsSounds.Length);
                clipName = AudioManager.Instance.villagerFootstepsSounds[index].soundName;
                break;
            case "Ghoul":
                arrayName = AudioManager.ArrayName.ghoulfootsteps;
                index = UnityEngine.Random.Range(0, AudioManager.Instance.ghoulFootstepsSounds.Length);
                clipName = AudioManager.Instance.ghoulFootstepsSounds[index].soundName;
                break;
            case "Guard":
                arrayName = AudioManager.ArrayName.guardfootsteps;
                index = UnityEngine.Random.Range(0, AudioManager.Instance.guardFootstepsSounds.Length);
                clipName = AudioManager.Instance.guardFootstepsSounds[index].soundName;
                break;
        }

        if (playerCharacter != null && isNightPhase) //this is determining the attenuation
        {
            AudioManager.Instance.Play(clipName, arrayName, Vector3.Distance(transform.position, playerCharacter.transform.position));
        }
        else
        {
            AudioManager.Instance.Play(clipName, arrayName);
        }
    }


    public void PlaySFX(string sound)
    {
        int index;
        arrayName = AudioManager.ArrayName.sfx;
        switch (sound)
        {
            case "BodyImpact":
                clipName = sound;
                break;
            case "NPCAttack":
                index = UnityEngine.Random.Range(1, 3);
                clipName = sound + index;
                break;
            case "PCAttack":
                clipName = sound;
                break;
            case "VDeath":
                if (isNightPhase)
                {
                    clipName = "VDeathDay";
                }
                else
                {
                    clipName = "VDeathNight";
                }
                break;
            case "VScream":
                index = UnityEngine.Random.Range(1, 3);
                clipName = sound + index;
                break;
            case "TowerCatapultAttack":
                clipName = sound;
                break;
            case "TowerCatapultLoad":
                clipName = sound;
                break;
            case "TowerCrossbow":
                clipName = sound;
                break;
        }

        if (playerCharacter != null && isNightPhase) //this is determining the attenuation
        {
            AudioManager.Instance.Play(clipName, arrayName, Vector3.Distance(transform.position, playerCharacter.transform.position));
        }
        else
        {
            AudioManager.Instance.Play(clipName, arrayName);
        }
    }

}