using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenUI : MonoBehaviour
{
    public GameObject deathScreen;
    public TMP_Text GameOverMessage;
    
    private void Start()
    {
        NightNPCManager.Instance.GameOverEvent += ShowScreen;
    }
    private void OnDisable()
    {
        NightNPCManager.Instance.GameOverEvent -= ShowScreen;
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

    public void ShowMessage(string message)
    {
        GameOverMessage.text = message;
    }
}
