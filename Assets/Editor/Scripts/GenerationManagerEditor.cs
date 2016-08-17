using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace Settlement
{

    [CustomEditor(typeof(GenerationManager))]
    public class GenerationManagerEditor : Editor
    {
        public int styleInd = 0;

        private GenerationManager script;

        public void OnEnable()
        {
            script = (GenerationManager)target;
            if (script.defaultStyle != null)
                script.defaultStyle.defaultStyle = true; //TODO: DO ZMIANY!
        }

        public override void OnInspectorGUI()
        {
            string info = "Procedural Building Generation Thesis. \n" +
                          "made by Bartłomiej Sieczka\n" +
                          "Technical University of Lodz, 2016";

            EditorGUILayout.HelpBox(info, MessageType.Info);
            EditorGUILayout.Space();

            DrawDefaultInspector();

            if (script.styleManager == null)
                script.styleManager = script.gameObject.GetComponent<StyleManager>();
              
            if(GUILayout.Button("Create new settlement with default style"))
            {
                GameObject newObj = script.createNewSettlement(script.defaultStyle);
                Vector3 newPos = newObj.transform.position;
                newPos.z -= 10f;
                SceneView.lastActiveSceneView.pivot = newPos;
                SceneView.lastActiveSceneView.Repaint();
                //sceneViewCam.transform.position = new Vector3(newObj.transform.position.x, newObj.transform.position.y, newObj.transform.position.z - 10f);
            }

            EditorGUILayout.BeginHorizontal();
            if(GUILayout.Button("Create new settlement with style"))
            {
                GameObject newObj = script.createNewSettlement(script.styleManager.styles[styleInd]);
                Vector3 newPos = newObj.transform.position;
                newPos.z -= 10f;
                SceneView.lastActiveSceneView.pivot = newPos;
                SceneView.lastActiveSceneView.Repaint();
            }
            string[] styles = script.styleManager.getStyleNames().ToArray();
            styleInd = EditorGUILayout.Popup(styleInd, styles);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Name [Style]");            
            EditorGUILayout.LabelField("Options");
            EditorGUILayout.EndHorizontal();
            if(script.settlements.Count == 0)
                EditorGUILayout.LabelField("There are no generated settlements.");
            for(int i = 0 ; i < script.settlements.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(string.Format("{0} [{1}]", script.settlements[i].gameObject.name, script.settlements[i].data.name != null ? script.settlements[i].data.name : "!NULL!"));
                if(GUILayout.Button("Edit"))
                {
                    Selection.activeGameObject = script.settlements[i].gameObject;
                    Vector3 newPos = script.settlements[i].transform.position;
                    newPos.z -= 10f;
                    SceneView.lastActiveSceneView.pivot = newPos;
                    SceneView.lastActiveSceneView.Repaint();
                }
                if(GUILayout.Button("Clone"))
                {
                    GameObject cloned = script.cloneSettlement(script.settlements[i]);
                    Selection.activeGameObject = cloned;
                    Vector3 newPos = cloned.transform.position;
                    newPos.z -= 10f;
                    SceneView.lastActiveSceneView.pivot = newPos;
                    SceneView.lastActiveSceneView.Repaint();
                }
                if(GUILayout.Button("Delete"))
                {
                    script.deleteSettlement(script.settlements[i]);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
