using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CastleIconUI : MonoBehaviour
{
    public TMP_Text castleText;
    private int castleLevel;
    
    private void Start()
    {
        castleLevel = PlayerManager.Instance.CastleLevel;
        UpdateCastleUI();
        
        PlayerManager.Instance.CastleLevelChangedEvent += UpdateCastleUI;
    }

    void UpdateCastleUI()
    {
        castleText.text = castleLevel + " / " + 4;
    }
}
