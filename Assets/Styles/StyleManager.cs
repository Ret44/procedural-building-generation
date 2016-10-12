using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Settlement;

[RequireComponent(typeof(SettlementData))]
public class StyleManager : MonoBehaviour {

    public static StyleManager instance;

    private SettlementData _defaultStyle;
    public SettlementData defaultStyle
    {
        get
        {
            if(_defaultStyle == null)
            {
                _defaultStyle = GetComponent<SettlementData>();
            }
            return _defaultStyle;
        }
    }

    private GenerationManager _generationManager;
    public GenerationManager generationManager
    {
        get
        {
            if(_generationManager == null)
            {
                _generationManager = GetComponent<GenerationManager>();
            }
            return _generationManager;
        }
    }

    public List<SettlementData> styles;

    public StyleManager editorInstance;

    public List<string> getStyleNames()
    {
        List<string> names = new List<string>();
        for (int i = 0; i < styles.Count; i++)
        {
            names.Add(styles[i].name);
        }
        names.Add(string.Format("Default ({0})", defaultStyle.name));
        return names;
    }
	private static void instanceCheck()
    {
        if(instance == null)
        {
            instance = GameObject.FindObjectOfType(typeof(StyleManager)) as StyleManager;

        }
    }


    public GameObject cloneStyle(SettlementData style)
    {
        GameObject newStyleObj = Instantiate(style.gameObject) as GameObject;
        SettlementData newData = newStyleObj.GetComponent<SettlementData>();
        newData.name = string.Format("{0} [Clone]", newData.name);
        newData.material = new Material(style.material);
        newStyleObj.transform.parent = this.transform;

        styles.Add(newData);

        return newStyleObj;
    }
  
    public void createNewStyle()
    {
        if (styles == null)
            styles = new List<SettlementData>();


        GameObject newStyleObj = new GameObject();
        SettlementData newData = newStyleObj.AddComponent<SettlementData>();
        newStyleObj.name = newData.name;
        newStyleObj.transform.parent = this.transform;
        newData.material = new Material(Shader.Find("Custom/Wall"));
        newData.material.SetTexture(0, Resources.Load("Textures/building-texture") as Texture);
        newData.manager = this;

        styles.Add(newData);
    }
	
    public void deleteStyle(SettlementData style)
    {
        //TODO: Upewnij się że nie ma połączeń
        styles.Remove(style);
        DestroyImmediate(style.gameObject);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
