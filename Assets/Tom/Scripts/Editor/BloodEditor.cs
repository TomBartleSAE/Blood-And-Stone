using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Blood))]
public class BloodEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("+5 Blood"))
        {
            (target as Blood)?.ChangeBlood(5);
        }
        
        if (GUILayout.Button("-5 Blood"))
        {
            (target as Blood)?.ChangeBlood(-5);
        }
        
        if (GUILayout.Button("+5 Max Blood"))
        {
            (target as Blood)?.ChangeMaxBlood(5);
        }
        
        if (GUILayout.Button("-5 Max Blood"))
        {
            (target as Blood)?.ChangeMaxBlood(-5);
        }
    }
}
