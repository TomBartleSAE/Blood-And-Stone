using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenUI : ManagerBase<DeathScreenUI>
{
    public GameObject deathScreen;

    private void Start()
    {
        NightNPCManager.Instance.GameOverNightEvent += ShowScreen;
        DayNPCManager.Instance.GameOverDayEvent += ShowScreen;
    }
    
    private void OnDisable()
    {
        NightNPCManager.Instance.GameOverNightEvent -= ShowScreen;
        DayNPCManager.Instance.GameOverDayEvent -= ShowScreen;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
    }

    public void ShowScreen()
    {
        deathScreen.SetActive(true);
    }
}
