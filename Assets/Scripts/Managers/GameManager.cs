using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : ManagerBase<GameManager>
{
    public int currentDay;
    public StateBase dayPhaseState, nightPhaseState;
    public StateManager stateManager;
    public Image loadingImage;

    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    
    public IEnumerator ChangePhase(string newSceneName, string oldSceneName, StateBase newState)
    {
        loadingImage.DOFade(1, 1);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(newSceneName, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(oldSceneName);
        yield return new WaitForSeconds(3);
        loadingImage.DOFade(0, 1);
        stateManager.ChangeState(newState);
    }

    public void CallPhaseChange(string newSceneName, string oldSceneName, StateBase newState)
    {
        // Need to start coroutine here so it doesn't get interrupted by objects being destroyed
        StartCoroutine(ChangePhase(newSceneName, oldSceneName, newState));
    }
}
