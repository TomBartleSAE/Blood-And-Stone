using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Object levelToLoad;
    
    public void Play()
    {
        SceneManager.UnloadSceneAsync("MainMenu");
        SceneManager.LoadScene("NightTest", LoadSceneMode.Additive);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
