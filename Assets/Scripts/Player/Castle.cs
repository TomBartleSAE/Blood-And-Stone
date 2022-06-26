using System;
using System.Collections;
using System.Collections.Generic;
using Tom;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Castle : MonoBehaviour
{
    public Camera cam;
    public LayerMask castleLayer;
    
    public Health health;
    public int level = 1;
    
    [Tooltip("Use elements 1, 2 and 3 for upgrades to level 2, 3 and 4, leave element 0 at 0")]
    public int[] upgradeCosts = new int[4];

    public int[] ghoulPopcaps = new int[4];
    public GameObject[] meshes = new GameObject[4];
    
    private MainControls controls;
    private InputAction leftClick;

    private void Awake()
    {
        controls = new MainControls();

        health.DeathEvent += DestroyCastle;
    }


    private void OnEnable()
    {
        controls.Enable();

        leftClick = controls.Day.LeftClick;
        leftClick.Enable();
        leftClick.performed += PerformLeftClick;
    }

    private void OnDisable()
    {
        controls.Disable();
        
        leftClick.Disable();
        leftClick.performed += PerformLeftClick;
    }

    private void PerformLeftClick(InputAction.CallbackContext obj)
    {
        Vector2 mousePosition = controls.Day.MousePosition.ReadValue<Vector2>();
        Ray ray = cam.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, Mathf.Infinity, castleLayer))
        {
            Upgrade();
        }
    }

    public void DestroyCastle(GameObject castle)
    {
        StartCoroutine(ReturnToMenu());
    }

    public IEnumerator ReturnToMenu()
    {
        MessageManager.Instance.ShowMessage("The villagers destroyed your castle!", 3f);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");
    }

    public void Upgrade()
    {
        if (PlayerManager.Instance.currentBlood >= upgradeCosts[level])
        {
            // TODO: Uncomment this and assign meshes when they are done
            //meshes[level - 1].SetActive(false);
            //meshes[level].SetActive(true);
            
            // TODO: Find out where ghoul pop cap is and set it to the next level
            PlayerManager.Instance.currentBlood -= upgradeCosts[level];
            level++;
        }
    }
}
