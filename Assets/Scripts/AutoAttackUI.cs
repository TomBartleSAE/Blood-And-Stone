using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttackUI : MonoBehaviour
{
    public GhoulModel ghoulModel;

    public void ChangeAutoAttack(bool newValue)
    {
        ghoulModel.autoAttack = newValue;
    }
}
