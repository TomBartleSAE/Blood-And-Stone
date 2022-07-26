using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObjectSwap))]
public class ObjectSwapEditor :Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ObjectSwap objectSwap = (ObjectSwap)target;
        if (GUILayout.Button("Swap"))
        {
            objectSwap.SwapObject();
        }
    }
}
