using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NightPhaseState : StateBase
{
    public LevelTimer timer;
    [Tooltip("Total duration of the Night Phase in seconds")]
    public float nightPhaseTime = 60f;
    
    public override void Enter()
    {
        base.Enter();
        
        timer.TimerFinishedEvent += GameOver;
        NightNPCManager.Instance.GameOverCaptureEvent += GameOverCapture;
        
        timer.StartTimer(nightPhaseTime);
    }

    public override void Exit()
    {
        base.Exit();
        
        timer.TimerFinishedEvent -= GameOver;
    }

    private void GameOver()
    {
        StartCoroutine(GameOverSequence());
    }
    
    private IEnumerator GameOverSequence()
    {
        Destroy(FindObjectOfType<VampireModel>().gameObject); // HACK: Find vampire object another way
        MessageManager.Instance.ShowMessage("The sun has risen and you are burnt to ashes...", 3f);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");
    }
    
    private void GameOverCapture()
    {
        StartCoroutine(CapturedSequence());
    }
    
    //HACK C&P for now
    private IEnumerator CapturedSequence()
    {
        Destroy(FindObjectOfType<VampireModel>().gameObject); // HACK: Find vampire object another way
        MessageManager.Instance.ShowMessage("You were captured by the town guard and taken into custody...", 3f);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");
    }
}
