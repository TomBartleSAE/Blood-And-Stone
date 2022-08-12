using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;

public class AudioManager : ManagerBase<AudioManager>
{
	public AudioMixerGroup mixerGroupMaster;
	public AudioMixer audioMixer;
	public AudioMixerSnapshot loadingAMSnapshot;

	public float maxDistanceAttenuation = 4;

	public SoundData[] ambienceSounds;

	public SoundData[] menuSounds;

	public SoundData[] musicSounds;

	public SoundData[] sfxSounds;

	public SoundData[] playerFootstepsSounds;

	public SoundData[] villagerFootstepsSounds;

	public SoundData[] guardFootstepsSounds;

	public SoundData[] ghoulFootstepsSounds;

	[HideInInspector]
	public string currentPhase;

	public enum ArrayName {
	
	ambience, 
	ui,
	music,
	sfx,
	playerfootsteps,
	villagerfootsteps,
	guardfootsteps,
	ghoulfootsteps
	}


	public override void Awake()
	{
		base.Awake();

		foreach (SoundData s in ambienceSounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.source.playOnAwake = s.playOnAwake;

			s.source.outputAudioMixerGroup = s.mixerGroup;
			
			if (s.playWhenPaused == true)						// this is going to make sure a sound can play when the game is paused :)
			{
				s.source.ignoreListenerPause = true;
			}
		}

		foreach (SoundData s in menuSounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.source.playOnAwake = s.playOnAwake;

			s.source.outputAudioMixerGroup = s.mixerGroup;

			if (s.playWhenPaused == true)
			{
				s.source.ignoreListenerPause = true;
			}
		}

		foreach (SoundData s in musicSounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.source.playOnAwake = s.playOnAwake;

			s.source.outputAudioMixerGroup = s.mixerGroup;

			if (s.playWhenPaused == true)
			{
				s.source.ignoreListenerPause = true;
			}
		}

		foreach (SoundData s in sfxSounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.source.playOnAwake = s.playOnAwake;

			s.source.outputAudioMixerGroup = s.mixerGroup;

			if (s.playWhenPaused == true)
			{
				s.source.ignoreListenerPause = true;
			}
		}

		foreach (SoundData s in playerFootstepsSounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.source.playOnAwake = s.playOnAwake;

			s.source.outputAudioMixerGroup = s.mixerGroup;

			if (s.playWhenPaused == true)
			{
				s.source.ignoreListenerPause = true;
			}
		}

		foreach (SoundData s in guardFootstepsSounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.source.playOnAwake = s.playOnAwake;

			s.source.outputAudioMixerGroup = s.mixerGroup;

			if (s.playWhenPaused == true)
			{
				s.source.ignoreListenerPause = true;
			}
		}

		foreach (SoundData s in villagerFootstepsSounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.source.playOnAwake = s.playOnAwake;

			s.source.outputAudioMixerGroup = s.mixerGroup;

			if (s.playWhenPaused == true)
			{
				s.source.ignoreListenerPause = true;
			}
		}

		foreach (SoundData s in ghoulFootstepsSounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.source.playOnAwake = s.playOnAwake;

			s.source.outputAudioMixerGroup = s.mixerGroup;

			if (s.playWhenPaused == true)
			{
				s.source.ignoreListenerPause = true;
			}
		}

	}

	// subscribing to event triggers found sounds

	void Start()
	{
		GameManager.Instance.LoadingStartedEvent += LoadingStart;
		GameManager.Instance.LoadingFinishedEvent += PlayPhaseMusic;
		GameManager.Instance.dayPhaseState.GetComponent<DayPhaseState>().WaveStartedEvent += PlayWaveMusic;
		GameManager.Instance.dayPhaseState.GetComponent<DayPhaseState>().WaveEndedEvent += PlayWaveOverMusic;
		GameManager.Instance.GameOverEvent += PlayGOMusic;
		LevelTimer.Instance.TimerNearlyOverEvent += PlayAmbience;
	}


	void OnDestroy()
	{
		GameManager.Instance.LoadingStartedEvent -= StopPlaying;
		GameManager.Instance.LoadingFinishedEvent -= PlayPhaseMusic;
		GameManager.Instance.dayPhaseState.GetComponent<DayPhaseState>().WaveStartedEvent -= PlayWaveMusic;
		GameManager.Instance.dayPhaseState.GetComponent<DayPhaseState>().WaveEndedEvent -= PlayWaveOverMusic;
		GameManager.Instance.GameOverEvent -= PlayGOMusic;
		LevelTimer.Instance.TimerNearlyOverEvent -= PlayAmbience;
	}


	// night phase almost ending: LevelTimer.Instance.TimerNearlyOverEvent (default to 10 seconds before end, can change this in Base scene > Level Timer object > nearlyOverEventTime)


	//Playing any sound on call

	public void Play(string sound, ArrayName arrayName, float? playerDistance = null) // questionmark on float is allowing the playpoint to be null. making this optional avoids the need for an overload, but an overload is a bit more standard in coding
	{
		SoundData s=new SoundData();
		switch (arrayName)
		{
			case ArrayName.ambience:
				s = Array.Find(ambienceSounds, item => item.soundName == sound);
				break;
			case ArrayName.ui:
				s = Array.Find(menuSounds, item => item.soundName == sound);
				break;
			case ArrayName.music:
				s = Array.Find(musicSounds, item => item.soundName == sound);
				break;
			case ArrayName.sfx:
				s = Array.Find(sfxSounds, item => item.soundName == sound);
				break;
			case ArrayName.playerfootsteps:
				s = Array.Find(playerFootstepsSounds, item => item.soundName == sound);
				break;
			case ArrayName.villagerfootsteps:
				s = Array.Find(villagerFootstepsSounds, item => item.soundName == sound);
				break;
			case ArrayName.guardfootsteps:
				s = Array.Find(guardFootstepsSounds, item => item.soundName == sound);
				break;
			case ArrayName.ghoulfootsteps:
				s = Array.Find(ghoulFootstepsSounds, item => item.soundName == sound);
				break;
		}
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));  //This is randomising the volume/pitch by allocating a random number from the volume variance number -/+ 2
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		if (playerDistance != null)
		{
			// basic distance assumptions to fake attenuation 
			// Set volume to a certain level depending on distance parsed in from sfx/animation
			float playerDistanceToUse = playerDistance ?? 0; // this is because its upset at being okay for null above. the ?? is the overload, its saying if it's = null, then set it to 0, but it should never be null because of the if statement.
			s.source.volume *= 1f - (playerDistanceToUse / maxDistanceAttenuation);
		}

		s.source.Play(); 


	}

	public void StopPlaying(string soundToStop) // this is ONLY stopping the pause music when it is currently playing and the game is resuming
	{
		SoundData s = new SoundData();
		switch (soundToStop)
		{
			case "DayPhasePause":
				s = Array.Find(musicSounds, item => item.soundName == soundToStop);
				if (s.source.isPlaying)
				{
					s.source.Stop();
				}
				break;
			case "NightPhasePause":
				s = Array.Find(musicSounds, item => item.soundName == soundToStop);
				if (s.source.isPlaying)
				{
					s.source.Stop();
				}
				break;
			case "LoadingStarted":
				s = Array.Find(musicSounds, item => item.soundName == "MainMenuMusic");
				if (s.source.isPlaying)
				{
					s.source.Stop();
				}
				s = Array.Find(musicSounds, item => item.soundName == "NightPhaseLoop");
				if (s.source.isPlaying)
				{
					s.source.Stop();
				}
				s = Array.Find(musicSounds, item => item.soundName == "DayPhaseStart");
				if (s.source.isPlaying)
				{
					s.source.Stop();
				}
				s = Array.Find(musicSounds, item => item.soundName == "DayPhaseWave");
				if (s.source.isPlaying)
				{
					s.source.Stop();
				}
				s = Array.Find(musicSounds, item => item.soundName == "DayPhaseEnd");
				if (s.source.isPlaying)
				{
					s.source.Stop();
				}
				s = Array.Find(musicSounds, item => item.soundName == "GameOverStart");
				if (s.source.isPlaying)
				{
					s.source.Stop();
				}
				s = Array.Find(musicSounds, item => item.soundName == "GameOverLoop");
				if (s.source.isPlaying)
				{
					s.source.Stop();
				}
				s = Array.Find(musicSounds, item => item.soundName == "NightPhasePause");
				if (s.source.isPlaying)
				{
					s.source.Stop();
				}
				s = Array.Find(musicSounds, item => item.soundName == "DayPhasePause");
				if (s.source.isPlaying)
				{
					s.source.Stop();
				}
				break;
			case "PlayWaveMusic":
				s = Array.Find(musicSounds, item => item.soundName == "DayPhaseStart");
				if (s.source.isPlaying)
				{
					s.source.Stop();
				}
				break;
			case "PlayWaveOverMusic":
				s = Array.Find(musicSounds, item => item.soundName == "DayPhaseWave");
				if (s.source.isPlaying)
				{
					s.source.Stop();
				}
				break;
			case "PlayGOMusic":
				s = Array.Find(musicSounds, item => item.soundName == "DayPhaseStart");
				if (s.source.isPlaying)
				{
					s.source.Stop();
				}
				s = Array.Find(musicSounds, item => item.soundName == "DayPhaseWave");
				if (s.source.isPlaying)
				{
					s.source.Stop();
				}
				s = Array.Find(musicSounds, item => item.soundName == "DayPhaseEnd");
				if (s.source.isPlaying)
				{
					s.source.Stop();
				}
				s = Array.Find(musicSounds, item => item.soundName == "NightPhaseLoop");
				if (s.source.isPlaying)
				{
					s.source.Stop();
				}
				break;
		}

	}

	public void PausePlaying()  // so we need this to pause ALL currently playing sounds 
	{
		foreach (SoundData s in ambienceSounds)
		{
			if (s.source.isPlaying)
			{
				s.source.Pause();
			}
		}
		foreach (SoundData s in menuSounds)
		{
			if (s.source.isPlaying)
			{
				s.source.Pause();
			}
		}
		foreach (SoundData s in musicSounds)
		{
			if (s.source.isPlaying)
			{
				s.source.Pause();
			}
		}
		foreach (SoundData s in sfxSounds)
		{
			if (s.source.isPlaying)
			{
				s.source.Pause();
			}
		}
		foreach (SoundData s in playerFootstepsSounds)
		{
			if (s.source.isPlaying)
			{
				s.source.Pause();
			}
		}
		foreach (SoundData s in guardFootstepsSounds)
		{
			if (s.source.isPlaying)
			{
				s.source.Pause();
			}
		}
		foreach (SoundData s in villagerFootstepsSounds)
		{
			if (s.source.isPlaying)
			{
				s.source.Pause();
			}
		}
		foreach (SoundData s in ghoulFootstepsSounds)
		{
			if (s.source.isPlaying)
			{
				s.source.Pause();
			}
		}
	}

	public void ResumePlaying()  // resumes any sound paused by the PausePlaying function
	{
		foreach (SoundData s in ambienceSounds)
		{
			s.source.UnPause();
		}
		foreach (SoundData s in menuSounds)
		{
			s.source.UnPause();
		}
		foreach (SoundData s in musicSounds)
		{
			s.source.UnPause();
		}
		foreach (SoundData s in sfxSounds)
		{
			s.source.UnPause();
		}
		foreach (SoundData s in playerFootstepsSounds)
		{
			s.source.UnPause();
		}
		foreach (SoundData s in guardFootstepsSounds)
		{
			s.source.UnPause();
		}
		foreach (SoundData s in villagerFootstepsSounds)
		{
			s.source.UnPause();
		}
		foreach (SoundData s in ghoulFootstepsSounds)
		{
			s.source.UnPause();
		}
	}

	

	public void LoadingStart(string soundToStop) // while loading screen is up, stop all sound with the master mixer group getting ducked OR A SNAPSHOT
	{ 
	//	audioMixerSnapshot.TransitionTo notif: if !()
	}

	public void PlayPhaseMusic(string phaseName)
	{
		StartCoroutine(PhaseMusicCoroutine(phaseName));
	}

	IEnumerator PhaseMusicCoroutine(string phaseName)
	{
		currentPhase = phaseName;
		string clipName = "LoadIn";
		ArrayName arrayName = ArrayName.music;

		Play(clipName, arrayName);

		SoundData s = new SoundData();
		s = Array.Find(musicSounds, item => item.soundName == clipName);
		yield return new WaitForSeconds(s.clip.length);

		switch (phaseName)
		{
			case "NightTest":
				clipName = "NightPhaseLoop";
				break;
			case "DayTest":
				clipName = "DayPhaseStart";
				break;
			case "Tutorial_Act1-1":
				clipName = "DayPhaseStart";
				break;
			case "Tutorial_Act1-2":
				clipName = "NightPhaseLoop";
				break;
			case "Tutorial_Act2-1":
				clipName = "DayPhaseStart";
				break;
			case "Tutorial_Act2-2":
				clipName = "DayPhaseStart";
				break;
			case "Credits":
				clipName = "NightPhasePause";
				break;
		}
		Play(clipName, arrayName);
		yield return null;
	}


	//Get reference to ClickMovement script on Vampire object, subscribe to StartedMoveEvent
	//Get reference to ClickMovement script on Vampire object, subscribe to HasTargetEvent(function requires bool variable), check if bool variable is true
	//If you need a different sound for villagers and guards, add “if (clickMovementReference.target.GetComponent<VillagerModel>()” for villagers or “…GetComponent<GuardModel>()” for guards
	//Get reference to TooltipObject script in each Tower prefab, subscribe to SelectedObjectEvent

	public void PlayAmbience()
	{
		//need to add functionality

	}

	public void PlayWaveMusic()
	{
		string functionName = "PlayWaveMusic";
		string clipName = "DayPhaseWave";
		ArrayName arrayName = ArrayName.music;


		StopPlaying(functionName);
		Play(clipName, arrayName);

	}
	public void PlayWaveOverMusic()
	{
		string functionName = "PlayWaveOverMusic";
		string clipName = "DayPhaseEnd";
		ArrayName arrayName = ArrayName.music;


		StopPlaying(functionName);
		Play(clipName, arrayName);

	}
	public void PlayGOMusic()
	{
		string functionName = "PlayGOMusic";
		string clipName = "GameOverStart";
		string clipName2 = "GameOverLoop";
		ArrayName arrayName = ArrayName.music;


		StopPlaying(functionName);
		Play(clipName, arrayName);
		Play(clipName2, arrayName);
	}
	// GameManager.Instance.LoadingStartedEvent
	// GameManager.Instance.LoadingFinishedEvent   if (myScene == passedStringVariable)
	// GameManager.Instance.dayPhaseState.WaveEndedEvent
	// GameManager.Instance.dayPhaseState.WaveStartedEvent
	// night phase almost ending: LevelTimer.Instance.TimerNearlyOverEvent (default to 10 seconds before end, can change this in Base scene > Level Timer object > nearlyOverEventTime)
	// GameManager.Instance.GameOverEvent
	// Get reference to Pause System object in scene, subscribe to GamePausedEvent/GameResumedEvent




	//look for anything currently playing, fade it out, then fade in the new music



	//   the following code is what needs to be placed within other scripts to call the play function in the audio manager
	//
	//   public void TestSound()
	//   {
	//       FindObjectOfType<AudioManager>().Play("Insert clip name in audio manager inspector", AudioManager.ArrayName.insert arrayname);
	//   }




	//        myAudioSource.PlayDelayed(Random.Range(minDelay, maxDelay)); <<<< This element will be good only for ambience, will need mindelay/maxdelay variables, maybe add those to array for ambience?
}