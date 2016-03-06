using UnityEngine;
using UnityEditor;
using System.Collections;

public enum EditorMode {
    Chunk,
    Window,
    Foundation,
    Balcony
}


[CustomEditor(typeof(BuildingBase))]
public class BuildingEditor : Editor {

    bool showBuildingSettings;

    private float upFace;
    private float bottomFace;
    private float forwardFace;
    private float backFace;
    private float leftFace;
    private float rightFace;
    private string report;

    private EditorMode currentMode;

    public void OnSceneGUI()
    {
        BuildingBase script = (BuildingBase)target;

        if (script.DrawGizmos)
        {
            Handles.color = Color.white;

            Handles.Label(script.GlobalPosition(script.PointA), "A" + script.PointA.ToString());
            Handles.Label(script.GlobalPosition(script.PointB), "B" + script.PointB.ToString());
            Handles.Label(script.GlobalPosition(script.PointC), "C" + script.PointC.ToString());
            Handles.Label(script.GlobalPosition(script.PointD), "D" + script.PointD.ToString());

            Handles.Label(script.GlobalPosition(script.PointA + new Vector3(0f, script.height, 0f)), "A'" + (script.PointA + new Vector3(0f, script.height, 0f)).ToString());
            Handles.Label(script.GlobalPosition(script.PointB + new Vector3(0f, script.height, 0f)), "B'" + (script.PointB + new Vector3(0f, script.height, 0f)).ToString());
            Handles.Label(script.GlobalPosition(script.PointC + new Vector3(0f, script.height, 0f)), "C'" + (script.PointC + new Vector3(0f, script.height, 0f)).ToString());
            Handles.Label(script.GlobalPosition(script.PointD + new Vector3(0f, script.height, 0f)), "D'" + (script.PointD + new Vector3(0f, script.height, 0f)).ToString());

            switch (currentMode)
            {
                case EditorMode.Chunk:
                    EditorGUI.BeginChangeCheck();
                    upFace = Handles.ScaleSlider(script.height, script.transform.position, Vector3.up, Quaternion.identity, 2, 0.15f);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(target, "Gizmo Size");
                        script.height = upFace;
                        script.UpdatePoints();
                        script.DrawBuilding();
                    }
                    EditorGUI.BeginChangeCheck();
                    leftFace = Handles.ScaleSlider(script.width, script.transform.position, Vector3.right, Quaternion.identity, 2, 0.15f);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(target, "Gizmo Size");
                        script.width = leftFace;
                        script.UpdatePoints();
                        script.DrawBuilding();
                    }
                    EditorGUI.BeginChangeCheck();
                    forwardFace = Handles.ScaleSlider(script.depth, script.transform.position, Vector3.forward, Quaternion.identity, 2, 0.15f);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(target, "Gizmo Size");
                        script.depth = forwardFace;
                        script.UpdatePoints();
                        script.DrawBuilding();
                    }
                    break;
                case EditorMode.Window:
                    EditorGUI.BeginChangeCheck();
                    forwardFace = Handles.ScaleSlider(script.windowFrameSize, script.transform.position, Vector3.forward, Quaternion.identity, 2, 0.15f);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(target, "Gizmo Size");
                        script.windowFrameSize = forwardFace;
                        script.UpdatePoints();
                        script.DrawBuilding();
                    }
                  
                    EditorGUI.BeginChangeCheck();
                    leftFace = Handles.ScaleSlider(script.windowOffset, script.transform.position, Vector3.right, Quaternion.identity, 2, 0.15f);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(target, "Gizmo Size");
                        script.windowOffset = leftFace;
                        script.UpdatePoints();
                        script.DrawBuilding();
                    }
                    break;
                case EditorMode.Foundation:
                    EditorGUI.BeginChangeCheck();
                    upFace = Handles.ScaleSlider(script.foundationHeight, script.transform.position, Vector3.up, Quaternion.identity, 2, 0.15f);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(target, "Gizmo Size");
                        script.foundationHeight = upFace;
                        script.UpdatePoints();
                        script.DrawBuilding();
                    }
                    break;
                case EditorMode.Balcony:
                    EditorGUI.BeginChangeCheck();
                    forwardFace = Handles.ScaleSlider(script.balconyLength, script.transform.position, Vector3.forward, Quaternion.identity, 2, 0.15f);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(target, "Gizmo Size");
                        script.balconyLength = forwardFace;
                        script.UpdatePoints();
                        script.DrawBuilding();
                    }
                    break;
            }        
    }
    }

    public override void OnInspectorGUI()
    {
        BuildingBase script = (BuildingBase)target;

        string Info = "Procedural Building Generation Thesis.\n" +
                      "made by Bartłomiej Sieczka\n" +
                      "Technical University of Lodz, 2016";

        EditorGUILayout.HelpBox(Info, MessageType.Info);
        EditorGUILayout.Space();

        showBuildingSettings = EditorGUILayout.Foldout(showBuildingSettings, "Building settings");
        if (showBuildingSettings)
        {
            EditorGUILayout.LabelField("Building settings goes here");
            DrawDefaultInspector();
        }

        currentMode = (EditorMode)EditorGUILayout.EnumPopup("Editor mode",currentMode);

        if (GUILayout.Button("Generate object"))
        {

            if (script.windowSize.x > Vector3.Distance(script.PointA, script.PointB) || (script.windowSize.y * 2) > script.height)
                EditorUtility.DisplayDialog("Error", "Window size is bigger than whole building", "ok");
            else {
                script.DrawBuilding();                             
            }

        }

        EditorGUILayout.HelpBox("Elapsed time: " + script.elapsedTime + " ms", MessageType.None);

    }
}
