using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : ManagerBase<AudioManager>
{
	public AudioMixerGroup mixerGroup;

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

			s.source.outputAudioMixerGroup = mixerGroup;
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

			s.source.outputAudioMixerGroup = mixerGroup;
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

			s.source.outputAudioMixerGroup = mixerGroup;
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

			s.source.outputAudioMixerGroup = mixerGroup;
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

			s.source.outputAudioMixerGroup = mixerGroup;
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

			s.source.outputAudioMixerGroup = mixerGroup;
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

			s.source.outputAudioMixerGroup = mixerGroup;
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

			s.source.outputAudioMixerGroup = mixerGroup;
			if (s.playWhenPaused == true)
			{
				s.source.ignoreListenerPause = true;
			}
		}

	}

	// subscribing to event triggers found sounds

	void Start()
	{
		//GameManager.Instance.LoadingStartedEvent += PlayMusic;
		GameManager.Instance.LoadingFinishedEvent += PlayPhaseMusic;
		GameManager.Instance.dayPhaseState.GetComponent<DayPhaseState>().WaveEndedEvent += PlayMusic;
		GameManager.Instance.dayPhaseState.GetComponent<DayPhaseState>().WaveStartedEvent += PlayMusic;
		GameManager.Instance.GameOverEvent += PlayMusic;
		LevelTimer.Instance.TimerNearlyOverEvent += PlayAmbience;
	}


	void OnDestroy()
	{
		//GameManager.Instance.LoadingStartedEvent -= PlayMusic;
		GameManager.Instance.LoadingFinishedEvent -= PlayPhaseMusic;
		GameManager.Instance.dayPhaseState.GetComponent<DayPhaseState>().WaveEndedEvent -= PlayMusic;
		GameManager.Instance.dayPhaseState.GetComponent<DayPhaseState>().WaveStartedEvent -= PlayMusic;
		GameManager.Instance.GameOverEvent -= PlayMusic;
		LevelTimer.Instance.TimerNearlyOverEvent -= PlayAmbience;
	}


	// night phase almost ending: LevelTimer.Instance.TimerNearlyOverEvent (default to 10 seconds before end, can change this in Base scene > Level Timer object > nearlyOverEventTime)


	//Playing any sound on call

	public void Play(string sound, ArrayName arrayName)
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

		s.source.Play();
	}


	public void StopPlaying(string soundToStop) // this is ONLY stopping the pause music when it is currently playing and the game is resuming
	{
		SoundData s = new SoundData();
		s = Array.Find(musicSounds, item => item.soundName == soundToStop);
		if (s.source.isPlaying)
		{
			s.source.Stop();
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


	public void PlayPhaseMusic(string phaseName)
	{
		currentPhase = phaseName;
		string clipName = "LoadIn";
		ArrayName arrayName = ArrayName.music;

		Play(clipName, arrayName);
		//find a way to cycle through an if statement or wait until the end of 
		SoundData s = new SoundData();
		s = Array.Find(musicSounds, item => item.soundName == clipName);
		//while (s.source.isPlaying)
		//{
		//	Debug.Log(clipName);
		//}
		//delay
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
	}

	// tutorial sections are there loading screens between them?
	// DayTest, NightTest, Tutorial_Act1-1 1-2 2-1 2-2

	public void PlayAmbience()
	{
		//need to add functionality

	}

	public void PlayMusic()
	{
		//need to add functionality

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


