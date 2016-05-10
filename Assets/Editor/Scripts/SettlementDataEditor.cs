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

        script.name = EditorGUILayout.TextField("Style name ", script.name);
        EditorGUILayout.LabelField("Editor outline color");
        script.borderColor = EditorGUILayout.ColorField(script.borderColor);
        script.randomizationSeed = EditorGUILayout.TextField("Randomization seed", script.randomizationSeed);

        EditorGUILayout.Space();

        //if (GUILayout.Button("Generate materials and textures"))
        //{
        //  //  script.GenerateMaterial(new Vector2(30, 30));
        //}

        EditorGUILayout.Space();

        script.wallsFurbished = EditorGUILayout.Toggle("Walls furbished", script.wallsFurbished);

        if (script.wallsFurbished)
        {
            script.mainColor = EditorGUILayout.ColorField("Main color", script.mainColor);
            script.material.SetColor("_Color", script.mainColor);
            script.addColorCount = EditorGUILayout.IntSlider("Additional colors",script.addColorCount, 0, 3);

            for (int i = 0; i < script.addColorCount; i++)
            {
                script.addColor[i] = EditorGUILayout.ColorField(string.Format("Color {0}", i + 1), script.addColor[i]);
                script.material.SetColor(string.Format("_Color{0}",i+1), script.addColor[i]);
            }
        }

        defaultInspector = EditorGUILayout.Foldout(defaultInspector, "Default Inspector");
        if (defaultInspector) DrawDefaultInspector();

        EditorUtility.SetDirty(script);
    }
}
