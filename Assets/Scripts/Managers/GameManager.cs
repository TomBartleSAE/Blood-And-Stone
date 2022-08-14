using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : ManagerBase<GameManager>
{
    public int currentDay;
    public StateBase mainMenuState, dayPhaseState, nightPhaseState, tutorialState;
    public StateManager stateManager;
    public Image loadingImage;

    public DeathScreenUI deathScreenUI;
    
    public LevelTimer levelTimer;

    private bool levelChanging = false;

    public GameObject baseCamera;

    public event Action GameOverEvent;

    public event Action<string> LoadingStartedEvent;
    public event Action<string> LoadingFinishedEvent;
    
    public override void Awake()
    {
        base.Awake();
        
        // Build only loads the Base scene, need to load the menu separately
        // The #if allows you to use the Base scene to test with the Day or Night scenes
        #if !UNITY_EDITOR
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        #endif
    }
    
    public IEnumerator ChangePhase(string newSceneName, string oldSceneName, StateBase newState)
    {
        levelChanging = true;
        loadingImage.DOFade(1, 1);
        yield return new WaitForSeconds(1);
        baseCamera.SetActive(true);
        LoadingStartedEvent?.Invoke("LoadingStarted");

        AsyncOperation unload = SceneManager.UnloadSceneAsync(oldSceneName);
        yield return unload;
        AsyncOperation load = SceneManager.LoadSceneAsync(newSceneName, LoadSceneMode.Additive);
        yield return load;
        
        levelChanging = false;
        baseCamera.SetActive(false);
        LoadingFinishedEvent?.Invoke(newSceneName);
        loadingImage.DOFade(0, 1);
        yield return new WaitForSeconds(1);
        stateManager.ChangeState(newState);
    }

    public void CallPhaseChange(string newSceneName, string oldSceneName, StateBase newState)
    {
        if (!levelChanging)
        {
            // Need to start coroutine here so it doesn't get interrupted by objects being destroyed
            StartCoroutine(ChangePhase(newSceneName, oldSceneName, newState));
        }
    }

    public void GameOverMessage(string message)
    {
		deathScreenUI.ShowScreen(message);
        GameOverEvent?.Invoke();
    }
}
