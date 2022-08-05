using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsButton : MonoBehaviour
{

    public void ReturnToMainMenu()
    {
        GameManager.Instance.CallPhaseChange("MainMenu", SceneManager.GetActiveScene().name, GameManager.Instance.mainMenuState);
    }

}
