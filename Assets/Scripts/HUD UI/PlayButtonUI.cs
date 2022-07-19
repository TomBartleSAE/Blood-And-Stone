using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonUI : MonoBehaviour
{
    public DayPhaseState dayPhase;
    
    // Start is called before the first frame update
    void Start()
    {
        dayPhase = GameManager.Instance.GetComponentInChildren<DayPhaseState>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartWaves()
    {
        dayPhase.StartWave();
    }
}
