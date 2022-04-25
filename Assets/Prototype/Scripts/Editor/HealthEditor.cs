using System.Collections;
using System.Collections.Generic;
using Tom;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Health))]
public class HealthEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("+5 Health"))
        {
            (target as Health)?.ChangeHealth(5f);
        }
        
        if (GUILayout.Button("-5 Health"))
        {
            (target as Health)?.ChangeHealth(-5f);
        }
    }
}
