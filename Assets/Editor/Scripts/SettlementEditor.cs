//using UnityEngine;
//using UnityEditor;
//using System.Collections;

//namespace Settlement
//{
//    [CustomEditor(typeof(SettlementBase))]
//    public class SettlementEditor : Editor
//    {
//        SettlementBase script;

//        public void OnEnable()
//        {
//            script = target as SettlementBase;

//            //Vector2 mousePos = GUIToGridPoint()
//        }
//        Vector3 GUIToPlanePoint(Vector2 guiPoint)
//        {
//            Plane plane = new Plane(-Vector3.forward, Vector3.zero);
//            Ray ray = HandleUtility.GUIPointToWorldRay(guiPoint);
//            float dist = 0;
//            plane.Raycast(ray, out dist);
//            Vector2 point = ray.GetPoint(dist);

//            return new Vector3(point.x, script.transform.position.y, point.y);
//        }

//        public void DrawCursor(Vector2 pos)
//        {
//            Handles.DrawSolidDisc(new Vector3(pos.x, script.transform.position.y, pos.y), Vector3.up, 10f);
//            Debug.Log(new Vector3(pos.x, script.transform.position.y, pos.y).ToString());
//        }


//        void OnSceneGUI()
//        {
//            int controlID = GUIUtility.GetControlID(FocusType.Passive);
            
//            Vector3 mousePos = GUIToPlanePoint(Event.current.mousePosition);
//            if (Event.current.shift)
//            {
//                Handles.DrawWireDisc(mousePos, Vector3.up, 10f);
//            }
//            switch(Event.current.type)
//            {
//                case EventType.MouseDown:
//                    if (Event.current.shift)
//                    {
//                        GUIUtility.hotControl = controlID;
//                      //  Event.current.Use();
//                    }
//                    break;
//                case EventType.MouseMove:
//                        Event.current.Use();
//                    break;
                 
//            }
//            if(Event.current.type == EventType.MouseMove)
//            {
//                GUIUtility.hotControl = controlID;
//            }

//        }

//        public override void OnInspectorGUI()
//        {
//            SettlementBase script = (SettlementBase)target;
//            string info = "Procedural Building Generation Thesis. \n" +
//                          "made by Bartłomiej Sieczka\n" +
//                          "Technical University of Lodz, 2016";

//            EditorGUILayout.HelpBox(info, MessageType.Info);
//            EditorGUILayout.Space();
//        }
       
//    }
//}