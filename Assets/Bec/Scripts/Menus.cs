using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
     public void PlayGame ()
    {
        // just added the only other scene in the game - probs needs to be changed. 
        SceneManager.LoadScene(0);

    }
}
