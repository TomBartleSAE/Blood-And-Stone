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

    public GameObject tooltip;

    private void OnEnable()
    {
        health.DeathEvent += DestroyCastle;
        InputManager.Instance.OnLeftClickEvent += PerformLeftClick;
        ShowTooltip(false);
    }

    private void OnDisable()
    {
        health.DeathEvent -= DestroyCastle;
        InputManager.Instance.OnLeftClickEvent -= PerformLeftClick;
    }

    private void PerformLeftClick(ClickEventArgs args)
    {
        Ray ray = cam.ScreenPointToRay(args.mousePosition);

        if (Physics.Raycast(ray, Mathf.Infinity, castleLayer))
        {
            ShowTooltip(true);
        }
        else
        {
            ShowTooltip(false);
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

    public void ShowTooltip(bool active)
    {
        tooltip.SetActive(active);
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
