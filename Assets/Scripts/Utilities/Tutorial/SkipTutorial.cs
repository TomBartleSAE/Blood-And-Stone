using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipTutorial : MonoBehaviour
{
    public void Skip()
    {
        GameManager.Instance.CallPhaseChange("NightTest", SceneManager.GetActiveScene().name,
            GameManager.Instance.nightPhaseState);
    }
}
