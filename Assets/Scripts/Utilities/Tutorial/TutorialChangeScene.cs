using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialChangeScene : TutorialElementBase
{
    public string nextScene;

    public override void Activate()
    {
        base.Activate();

        GameManager.Instance.CallPhaseChange(nextScene, SceneManager.GetActiveScene().name,
            GameManager.Instance.tutorialState);
    }
}
