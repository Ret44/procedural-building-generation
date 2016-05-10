//using UnityEngine;
//using UnityEditor;
//using System.Collections;
//using System.Collections.Generic;

//[CustomEditor(typeof(DistrictManager))]
//public class DistrictManagerEditor : Editor {

//    DistrictManager script;

//    public bool showDefaultInspector;

//    public override void OnInspectorGUI()
//    {
//        script = (DistrictManager)target;

//        string Info = "Procedural Building Generation Thesis.\n" +
//                      "made by Bartłomiej Sieczka\n" +
//                      "Technical University of Lodz, 2016";

//        EditorGUILayout.HelpBox(Info, MessageType.Info);
//        EditorGUILayout.Space();

//        if(GUILayout.Button("Add District"))
//        {
//            DistrictManager.AddDistrict();
//        }

//        for (int i = 0; i < DistrictManager.instance.districts.Count; i++)
//        {
//            EditorGUILayout.LabelField(string.Format("District no. {0}", i + 1));
//            DistrictManager.GetDistrict(i).name = EditorGUILayout.TextField("Name", DistrictManager.GetDistrict(i).name);
//            DistrictManager.GetDistrict(i).borderColor = EditorGUILayout.ColorField("Color", DistrictManager.GetDistrict(i).color);
//            if (GUILayout.Button("Remove District"))
//            {
//                DistrictManager.RemoveDistrict(i);
//            }
//        }

//        showDefaultInspector = EditorGUILayout.Foldout(showDefaultInspector,"Default Inspector");
//        if (showDefaultInspector)
//        {
//            DrawDefaultInspector();
//        }
//    }
//}
