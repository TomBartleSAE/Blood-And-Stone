using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoAttackUI : MonoBehaviour
{
    public GhoulModel ghoulModel;
    public GameObject autoAttackPanel;

    public BoxSelection boxSelection;

    public bool isDayPhase;

    public void Start()
    {
        boxSelection.GhoulSelectedEvent += SetAutoAttackPanel;
    }

    public void SetAutoAttackPanel(bool value)
    {
        foreach (var unit in boxSelection.units)
        {
            ghoulModel = unit.GetComponent<GhoulModel>();
        }
        autoAttackPanel.SetActive(value);
        autoAttackPanel.GetComponentInChildren<Toggle>().isOn = ghoulModel.LocalAutoAttack;
    }

    public void ChangeAutoAttackBool(bool value)
    {
        if (boxSelection.units.Count > 1)
        {
            foreach (var unit in boxSelection.units)
            {
                unit.GetComponent<GhoulModel>().LocalAutoAttack = value;
            }
        }
        
        else
        {
            ghoulModel.LocalAutoAttack = value;
        }
    }
}
