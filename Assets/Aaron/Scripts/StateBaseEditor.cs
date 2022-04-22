using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StateBase), true)]
public class StateBaseEditor : Editor
{
    public override void OnInspectorGUI()
    {

        if(GUILayout.Button("Enter State"))
        {
            ((StateBase)target).GetComponent<StateManager>().ChangeState((StateBase)target);
        }
        
        if(GUILayout.Button("Execute State"))
        {
            ((StateBase)target).GetComponent<StateManager>().ChangeState((StateBase)target);
        }

        if (GUILayout.Button("Exit State"))
        {
            ((StateBase)target).GetComponent<StateManager>().ChangeState((StateBase)target);
        }
    }
}