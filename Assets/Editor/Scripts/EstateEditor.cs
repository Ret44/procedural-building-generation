using UnityEngine;
using UnityEditor;
using System.Collections;
namespace Estate
{
    [CustomEditor(typeof(EstateBase))]
    public class EstateEditor : Editor
    {
        private bool showEstateSetting;
        EstateBase script;
        bool issues;

        void OnEnable()
        {
            script = target as EstateBase;
        }
        

        void OnSceneGUI()
        {
            Handles.Label(script.transform.position + new Vector3(script.outerBounds.width / 2, 0f, script.outerBounds.height / 2), script.name);
        }


        public bool CheckForIssues(out string issues)
        {
            bool issue = false;
            issues = "Found issues:";

            if(script.outerBounds.width < 15)
            {
                issue = true;
                issues = string.Format("{0}\n- Estate must be at least 15m wide", issues);
            }
            if(script.outerBounds.height < 15)
            {
                issue = true;
                issues = string.Format("{0}\n- Estate must be at least 15m long", issues);
            }

            if(script.buildingBounds.x < script.outerBounds.x || script.buildingBounds.x + script.buildingBounds.width > script.outerBounds.x + script.outerBounds.width
              || script.buildingBounds.y < script.outerBounds.y || script.buildingBounds.y + script.buildingBounds.height > script.outerBounds.y + script.outerBounds.height)
            {
                issue = true;
                issues = string.Format("{0}\n- Building is bigger than estate", issues);
            }

            return issue;
        }

        public override void OnInspectorGUI()
        {
     
            string info = "Procedural Building Generation Thesis. \n" +
                          "made by Bartłomiej Sieczka\n" +
                          "Technical University of Lodz, 2016";

            EditorGUILayout.HelpBox(info, MessageType.Info);

            
            
            EditorGUILayout.Space();
            showEstateSetting = EditorGUILayout.Foldout(showEstateSetting, "Estate Settings");

            if(showEstateSetting)
                DrawDefaultInspector();

            string issues;
            if (CheckForIssues(out issues))
            {
                EditorGUILayout.HelpBox(issues, MessageType.Error);
            }
            else
            {
                if(GUILayout.Button("Generate estate"))
                {
                    script.DrawEstate();
                }
            }
        }
    }
}