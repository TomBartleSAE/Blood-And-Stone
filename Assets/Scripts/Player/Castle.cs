using System;
using System.Collections;
using System.Collections.Generic;
using Tom;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Castle : MonoBehaviour
{
    public Health health;
    
    [Tooltip("Use elements 1, 2 and 3 for upgrades to level 2, 3 and 4, leave element 0 at 0")]
    public int[] upgradeCosts = new int[4];
    public int[] maxHealths = new int[4];
    public int[] ghoulPopcaps = new int[4];
    public GameObject[] meshes = new GameObject[4];

    private void OnEnable()
    {
        health.DeathEvent += DestroyCastle;
        health.DamageChangeEvent += UpdateCastleHealth;
        
        UpdateCastleHealth(gameObject);
    }

    private void OnDisable()
    {
        health.DeathEvent -= DestroyCastle;
        health.DamageChangeEvent -= UpdateCastleHealth;
    }

    public void DestroyCastle(GameObject castle)
    {
        StartCoroutine(ReturnToMenu());
    }

    public void UpdateCastleHealth(GameObject a)
    {
        // HACK: Not ideal having 2 variables that point to the same value, potential for mismatch
        PlayerManager.Instance.castleHealth = health.currentHealth;
    }

    // TODO: Move this to the GameManager
    public IEnumerator ReturnToMenu()
    {
        MessageManager.Instance.ShowMessage("The villagers destroyed your castle!", 3f);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");
    }

    public void Upgrade()
    {
        int level = PlayerManager.Instance.castleLevel;
        
        if (PlayerManager.Instance.currentBlood >= upgradeCosts[level])
        {
            meshes[level - 1].SetActive(false);
            meshes[level].SetActive(true);
            
            // TODO: Find out where ghoul pop cap is and set it to the next level
            PlayerManager.Instance.ChangeBlood(-upgradeCosts[level]);
            health.MaxHealth = maxHealths[level];
            health.ChangeHealth(health.MaxHealth - health.currentHealth, gameObject);
            PlayerManager.Instance.castleLevel++;
        }
    }
}
