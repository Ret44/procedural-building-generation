using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Settlement
{


    [CustomEditor(typeof(SettlementBase))]
    public class SettlementEditor : Editor
    {
        private bool showSettlementSettings;
        private int selectedAnchor = -1;
        private float tempValX;
        private float tempValY;
        private Vector3 tempVal;
        private Vector3 tempPos;

        public void OnSceneGUI()
        {
            SettlementBase script = (SettlementBase)target;
                        
            for(int i=0; i<script.Anchors.Count; ++i)
            {
                tempPos = script.transform.position + new Vector3(script.Anchors[i].x, 0f, script.Anchors[i].y);
                Handles.Label(tempPos, (selectedAnchor==i?"SELECTED ID: ":"ID: ")+i+" "+script.Anchors[i].ToString());
                EditorGUI.BeginChangeCheck();
                tempVal = Handles.FreeMoveHandle(tempPos, Quaternion.Euler(90f, 0f, 0f), 2.5f, new Vector3(1f, 1f, 1f), Handles.RectangleCap);
                //tempValX = Handles.tempPos.x, tempPos, Vector3.left, 2f, Handles.CircleCap, 0.5f);               
                if (EditorGUI.EndChangeCheck())
                {
                    tempVal -= script.transform.position;
                    Undo.RecordObject(target, "Move Anchor X");
                    script.Anchors[i] = new Vector2(tempVal.x,tempVal.z);
                    Debug.Log("Changed " + i);
                    selectedAnchor = i;
                    script.OnUpdateAnchors();
                    //t.Update();
                }
            }

        

            Handles.BeginGUI();
            GUILayout.Window(1, new Rect(Screen.width - 300, Screen.height - 180, 290, 150), (id) =>
            {
                if (selectedAnchor == -1)
                {
                    if (GUILayout.Button("Add anchor"))
                    {
                        script.AddAnchor();
                    }
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("No anchor is selected");
                }
                else
                {
                    if (GUILayout.Button("Add anchor after selected"))
                    {
                    script.AddAnchorAtPos(selectedAnchor);
                    }
                    if (GUILayout.Button("Add anchor at end"))
                    {
                        script.AddAnchor();
                    }
                    EditorGUILayout.IntField("Selected anchor ID:", selectedAnchor);
                    EditorGUILayout.Vector2Field("Anchor position", script.Anchors[selectedAnchor]);
                    if (GUILayout.Button("Remove anchor"))
                    {
                        script.RemoveAnchor(selectedAnchor);
                    }
                }
            }, "Anchor Editor");
            Handles.EndGUI();

        }

        public override void OnInspectorGUI()
        {
            SettlementBase script = (SettlementBase)target;
            string info = "Procedural Building Generation Thesis. \n" +
                          "made by Bartłomiej Sieczka\n" +
                          "Technical University of Lodz, 2016";

            EditorGUILayout.HelpBox(info, MessageType.Info);
            EditorGUILayout.Space();

            //EditorGUILayout.LabelField("Regulations");
            //EditorGUILayout.FloatField("Road width:",script.RoadWidth);
            //EditorGUILayout.FloatField("Road<->Building:", script.RoadWidth);
            //EditorGUILayout.FloatField("MAX(Building<->Building)", script.RoadWidth);
            //EditorGUILayout.LabelField("Distance between buildings depends on building height.");

            if(GUILayout.Button("Generate settlement"))
            {
                System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
                Debug.Log("Settlement generation started at "+System.DateTime.Now.ToString());
                timer.Start();
                script.GenerateSettlement();
                timer.Stop();
                Debug.Log("Settlement generation finished after " + timer.ElapsedMilliseconds + "ms at " + System.DateTime.Now.ToString());
            }

            showSettlementSettings = EditorGUILayout.Foldout(showSettlementSettings, "Settlement Settings");
            if (showSettlementSettings)
            {
                DrawDefaultInspector();
            }

        }
    }
}