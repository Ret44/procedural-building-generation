using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(StyleManager))]
public class StyleManagerEditor : Editor {

    public override void OnInspectorGUI()
    {
        StyleManager script = (StyleManager)target;
        string info = "Procedural Building Generation Thesis. \n" +
                      "made by Bartłomiej Sieczka\n" +
                      "Technical University of Lodz, 2016";

        EditorGUILayout.HelpBox(info, MessageType.Info);
        EditorGUILayout.Space();
        
        if(GUILayout.Button("Add new style"))
        {
            script.createNewStyle();
        }
        EditorGUILayout.Space();
   
        if (script.styles != null)
        {
            for (int i = 0; i < script.styles.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(script.styles[i].name);

                 if(GUILayout.Button("Edit"))
                 {
                    Selection.activeGameObject = script.styles[i].gameObject;
                 }
                
                 if (GUILayout.Button("Clone"))
                 {
                    script.cloneStyle(script.styles[i]);                   
                 }
                 if (GUILayout.Button("Delete"))
                 {
                     script.deleteStyle(script.styles[i]);
                 }

                 EditorGUILayout.EndHorizontal();
            }
        }



        //EditorGUILayout.Space();
        //DrawDefaultInspector();
    }
}
