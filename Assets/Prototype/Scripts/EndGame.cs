using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public void ReturnToMenu()
    {
        //TODO: need to keep an eye on this scene ref
        SceneManager.LoadScene(1);
        
        //TODO also: Clear World functionality, or is that GameManager stuff?
    }
}
