using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NightPhaseState : StateBase
{
    public LevelTimer timer;
    [Tooltip("Total duration of the Night Phase in seconds")]
    public float nightPhaseTime = 60f;

    public ParticleSystem burnParticle;
    
    public override void Enter()
    {
        base.Enter();

        timer.StartTimer(nightPhaseTime);
        timer.TimerFinishedEvent += GameOver;
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
        GameObject vampire = FindObjectOfType<VampireModel>().gameObject; // HACK: Find vampire object another way
        ParticleSystem newParticle = Instantiate(burnParticle, vampire.transform.position + Vector3.up * 0.5f, Quaternion.identity);
        newParticle.Play();
        Destroy(vampire);
        MessageManager.Instance.ShowMessage("The sun has risen and you are burnt to ashes...", 3f);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("NightTest");
    }
    
    public void GameOverCapture()
    {
        StartCoroutine(CapturedSequence());
    }
    
    //HACK C&P for now
    //TODO merge both coroutines; hook up to UI for messages etc
     private  IEnumerator CapturedSequence()
    {
        Destroy(FindObjectOfType<VampireModel>().gameObject); // HACK: Find vampire object another way
        MessageManager.Instance.ShowMessage("You were captured by the town guard and taken into custody...", 3f);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("NightTest");
    }
}
