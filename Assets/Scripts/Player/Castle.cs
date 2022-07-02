using System;
using System.Collections;
using System.Collections.Generic;
using Tom;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    public Camera cam;
    public LayerMask castleLayer;
    
    public Health health;
    public int level = 1;
    
    [Tooltip("Use elements 1, 2 and 3 for upgrades to level 2, 3 and 4, leave element 0 at 0")]
    public int[] upgradeCosts = new int[4];

    public int[] maxHealths = new int[4];
    public int[] ghoulPopcaps = new int[4];
    public GameObject[] meshes = new GameObject[4];

    public GameObject tooltip;
    public GraphicRaycaster graphicRaycaster;

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

        PointerEventData data = new PointerEventData(EventSystem.current);
        data.position = args.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(data, results);
        
        if (Physics.Raycast(ray, Mathf.Infinity, castleLayer))
        {
            ShowTooltip(true);
        }
        else if (results.Count == 0)
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
            meshes[level - 1].SetActive(false);
            meshes[level].SetActive(true);
            
            // TODO: Find out where ghoul pop cap is and set it to the next level
            PlayerManager.Instance.ChangeBlood(-upgradeCosts[level]);
            health.MaxHealth = maxHealths[level];
            health.ChangeHealth(health.MaxHealth - health.currentHealth, gameObject);
            level++;
            ShowTooltip(false);
        }
    }
}
