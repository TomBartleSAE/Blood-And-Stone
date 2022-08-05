using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonUI : MonoBehaviour
{
    public DayPhaseState dayPhase;

    private bool wavesActive = false;
    
    // Start is called before the first frame update
    void Start()
    {
        dayPhase = GameManager.Instance.GetComponentInChildren<DayPhaseState>();
    }

    public void StartWaves()
    {
	    if (!wavesActive)
	    {
		    wavesActive = true;
		    dayPhase.StartWave();
	    }
    }
}
