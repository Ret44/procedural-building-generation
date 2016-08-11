using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SettlementData))]
public class SettlementDataEditor : Editor
{

    SettlementData script;
    bool defaultInspector;
    bool showPalette;

    public void OnEnable()
    {
        script = target as SettlementData;
    }


    public override void OnInspectorGUI()
    {
        SettlementData script = (SettlementData)target;
        string info = "Procedural Building Generation Thesis. \n" +
                      "made by Bartłomiej Sieczka\n" +
                      "Technical University of Lodz, 2016";

        EditorGUILayout.HelpBox(info, MessageType.Info);
        EditorGUILayout.Space();

        if (script.manager != null)
        {
            if (GUILayout.Button("Back to Style Manager"))
            {
                Selection.activeGameObject = script.manager.gameObject;
            }

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Clone"))
            {
                Selection.activeGameObject = script.manager.cloneStyle(script);
            }

            if (GUILayout.Button("Delete"))
            {
                Selection.activeGameObject = script.manager.gameObject;
                script.manager.deleteStyle(script);
            }
            EditorGUILayout.EndHorizontal();
        }
        else
            EditorGUILayout.LabelField("Default Settlement style");

        EditorGUILayout.Space();
        DrawDefaultInspector();
    }
}
