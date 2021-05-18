using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Grid))]
public class GridEditor : Editor
{
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        Grid gridScript = (Grid)target;
        if (GUILayout.Button("Create Map")) {
            gridScript.CreateGridFromMapFile();
        }

        if (GUILayout.Button("Delete Map")) {
            gridScript.DeleteMap();
        }

    }
}
