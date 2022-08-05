using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenUI : MonoBehaviour
{
    public GameObject deathScreen;
    public TextMeshProUGUI gameOverMessage;

    public TextMeshProUGUI villagersDrainedText;
    public TextMeshProUGUI ghoulsCreatedText;
    public TextMeshProUGUI soldiersKilledText;

    public void ReturnToMainMenu()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
    }

    public void ShowScreen(string message)
    {
        deathScreen.SetActive(true);

        gameOverMessage.text = message;

        villagersDrainedText.text = PlayerManager.Instance.villagersDrained.ToString();
        ghoulsCreatedText.text = PlayerManager.Instance.ghoulsCreated.ToString();
        soldiersKilledText.text = PlayerManager.Instance.soldiersKilled.ToString();
    }
}
