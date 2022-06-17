using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerManager))]
public class BloodEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("+5 Blood"))
        {
            (target as PlayerManager)?.ChangeBlood(5);
        }
        
        if (GUILayout.Button("-5 Blood"))
        {
            (target as PlayerManager)?.ChangeBlood(-5);
        }
        
        if (GUILayout.Button("+5 Max Blood"))
        {
            (target as PlayerManager)?.ChangeMaxBlood(5);
        }
        
        if (GUILayout.Button("-5 Max Blood"))
        {
            (target as PlayerManager)?.ChangeMaxBlood(-5);
        }
    }
}
