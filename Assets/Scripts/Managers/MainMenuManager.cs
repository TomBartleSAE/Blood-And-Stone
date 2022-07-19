using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public TextMeshProUGUI continueText;
    
    public Slider volumeSlider;
    public Toggle muteToggle;

    public float previousVolume;
    
    private void Start()
    {
        if (SaveManager.Instance.SaveFileExists())
        {
            continueText.color = Color.black;
        }
        else
        {
            continueText.color = Color.gray;
        }
    }

    //should this be in here, or a property?
    private void FixedUpdate()
    {
        //changes volume according to slider
        AudioListener.volume = volumeSlider.value;
    }

    public void Play()
    {
        SceneManager.UnloadSceneAsync("MainMenu");
        SceneManager.LoadScene("NightTest", LoadSceneMode.Additive);
        GameManager.Instance.GetComponent<StateManager>().ChangeState(GameManager.Instance.nightPhaseState);
    }

    public void Load()
    {
        if (SaveManager.Instance.SaveFileExists())
        {
            PlayerManager.Instance.LoadSaveData(SaveManager.Instance.LoadGame());
            SceneManager.UnloadSceneAsync("MainMenu");
            SceneManager.LoadScene("DayTest", LoadSceneMode.Additive);
            GameManager.Instance.GetComponent<StateManager>().ChangeState(GameManager.Instance.nightPhaseState);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
