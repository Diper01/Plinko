using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PinGridGenerator))]
public class PinGridGeneratorEditor : Editor
{
    private int rowCount = 12;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.BeginHorizontal();
        rowCount = EditorGUILayout.IntField("row", rowCount);
        
        if (GUILayout.Button("Generate  "))
        {
            ((PinGridGenerator)target).rows = rowCount % 2 == 0 ? rowCount : rowCount+1;
            ((PinGridGenerator)target).GeneratePins();
        }
        EditorGUILayout.EndHorizontal();
    }
}