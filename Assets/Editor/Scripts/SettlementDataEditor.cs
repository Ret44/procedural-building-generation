using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using Settlement;

[CustomEditor(typeof(SettlementData))]
public class SettlementDataEditor : Editor
{

    SettlementData script;
    bool defaultInspector;
    bool showPalette;

    public void OnEnable()
    {
        script = target as SettlementData;
        if (script.defaultStyle)
            script.manager = script.GetComponent<StyleManager>();
    }
   
    public override void OnInspectorGUI()
    {
        SettlementData script = (SettlementData)target;
        string info = "Procedural Building Generation Thesis. \n" +
                      "made by Bartłomiej Sieczka\n" +
                      "Technical University of Lodz, 2016";

        EditorGUILayout.HelpBox(info, MessageType.Info);
        EditorGUILayout.Space();


        if (!script.defaultStyle)
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

        int count = 0;

        if (script.manager.generationManager.settlements != null)
        {
            EditorGUILayout.LabelField("List of connected settlements:");

            for (int i = 0; i < script.manager.generationManager.settlements.Count; i++)
            {
                SettlementBase settlement = script.manager.generationManager.settlements[i];
                if (settlement.data != null && settlement.data == script)
                {
                    EditorGUILayout.LabelField(string.Format(" - {0}", settlement.name));
                    count++;
                }
            }
            if (count == 0)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("   None");
                EditorGUILayout.Space();
            }
            else
            {
                if (GUILayout.Button("Refresh all"))
                {
                    script.manager.generationManager.refreshStyle(script);
                }
            }
        }

        EditorGUILayout.Space();
        DrawDefaultInspector();
    }
}
