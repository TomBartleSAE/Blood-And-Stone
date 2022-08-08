using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkipTutorial : MonoBehaviour
{
    public void Skip()
    {
        TutorialManager.Instance.LoadNewGameData();
        GameManager.Instance.CallPhaseChange("NightTest", SceneManager.GetActiveScene().name,
            GameManager.Instance.nightPhaseState);
        GetComponent<Button>().interactable = false;
    }
}
