using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAudioEvent : MonoBehaviour
{
	[HideInInspector]
	public AudioManager.ArrayName arrayName;

	[HideInInspector]
	public string clipName;


	public void PlayFootstep(string character)
	{
		int index = 0;
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

		AudioManager.Instance.Play(clipName, arrayName);

	}

	public void PlaySFX(string sound)
	{
		int index = 0;
		arrayName = AudioManager.ArrayName.sfx;
		switch (sound)
		{
			case "BodyImpact":
				clipName = sound;
				break;
			case "Attack":
				index = UnityEngine.Random.Range(1, 3);
				clipName = "NPCAttack" + index;
				break;
			case "PCAttack":
				clipName = sound;
				break;
			case "VDeath":
				//	if day,
				// 	clipName = "VDeathDay";
				//	else night
				clipName = "VDeathNight";
				break;
			case "VScream":
				index = UnityEngine.Random.Range(1, 3);
				clipName = sound + index;
				break;
		}

		AudioManager.Instance.Play(clipName, arrayName);

	}


	//20 - 22 = npc attack sounds

}