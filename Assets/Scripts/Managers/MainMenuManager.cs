using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.UnloadSceneAsync("MainMenu");
        SceneManager.LoadScene("NightTest", LoadSceneMode.Additive);
        GameManager.Instance.GetComponent<StateManager>().ChangeState(GameManager.Instance.nightPhaseState);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
