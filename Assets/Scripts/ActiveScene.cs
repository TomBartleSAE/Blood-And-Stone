using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveScene : MonoBehaviour
{
    // HACK: Script execution order set before default time
    // This code needs to run in Start function but also occur before any spawning code in Start
    void Start()
    {
        SceneManager.SetActiveScene(gameObject.scene);
    }
}
