using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Settlement", menuName = "Settlement Data", order = 1)]
public class SettlementData : ScriptableObject
{
    public string name;
    public Color borderColor;
    public string randomizationSeed;

    public Vector2 windowSize = new Vector2(1f, 1f);
    public float windowOffset = 0.4f;
    public float height = 15f;
    public float foundationHeight = 0.15f;
    public float balconyLength = 0.25f;

    public Material material;
    public Texture2D tex;
    public Material sideWallMaterial;
    public Texture2D sideTex;


    public Random windowRandom;

    public bool wallsFurbished;

    public Color mainColor;

    public int addColorCount;
    public Color[] addColor;


    public SettlementData()
    {
        string[] names = { "Bałuty", "Teofilów", "Radogoszcz", "Julianów", "Łagiewniki", "Marysin", "Rogi",
                                       "Nowosolna", "Stoki", "Mileszki", "Stary Widzew", "Zarzew", "Olechów", "Janów",
                                       "Andrzejów", "Wiskitno", "Chojny", "Dąbrowa", "Górniak", "Piastów", "Kurak",
                                       "Ruda", "Rokicie", "Lublinek", "Smulsko", "Retkinia", "Karolew", "Złotno",
                                       "Zdrowie", "Katedralna", "Śródmieście" };

        name = names[Random.Range(0, names.Length)];
        borderColor = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
        addColor = new Color[3];
        for (int i = 0; i < 3; i++)
            addColor[i] = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));

        material = new Material(Resources.Load("Materials/building") as Material);
    }


    public void GenerateMaterialAsset()
 //   public void GenerateMaterial(Vector2 sideWallSize)
    {
        
        //
        //Material baseMat = Resources.Load("Materials/building") as Material;
        //Texture2D baseTex = baseMat.mainTexture as Texture2D;
        //if (wallsFurbished)
        //{

        //}
        //else {
        //    material = baseMat;
        //}

        //sideTex = new Texture2D(512, 512);// (int)sideWallSize.x, (int)sideWallSize.y);

        //Vector2 offset = (wallsFurbished ? new Vector2(baseTex.width * 0.25f, 0f) : Vector2.zero);
        //int tx = (int)offset.x;
        //int ty = (int)offset.y;
        //for (int y = 0; y < sideTex.height; y++)
        //{
        //    for (int x = 0; x < sideTex.width; x++)
        //    {
        //        if (wallsFurbished)
        //        {
        //            sideTex.SetPixel(x, y, baseTex.GetPixel(tx++, ty) * mainColor);
        //        }
        //        else
        //        {
        //            sideTex.SetPixel(x, y, baseTex.GetPixel(tx++, ty));
        //        }
        //        if (tx >= baseTex.width) tx = 0;
        //    }
        //    ty++;
        //    if (ty >= baseTex.height) ty = 0;
        //}
        //sideWallMaterial = new Material(Shader.Find(" Diffuse"));
        //sideWallMaterial.SetTexture(0, sideTex as Texture);
    }
}

//public class SettlementData {

//    public string name;
//    public Color color;

//    public SettlementData()
//    {
//        string[] names = { "Bałuty", "Teofilów", "Radogoszcz", "Julianów", "Łagiewniki", "Marysin", "Rogi",
//                               "Nowosolna", "Stoki", "Mileszki", "Stary Widzew", "Zarzew", "Olechów", "Janów",
//                               "Andrzejów", "Wiskitno", "Chojny", "Dąbrowa", "Górniak", "Piastów", "Kurak",
//                               "Ruda", "Rokicie", "Lublinek", "Smulsko", "Retkinia", "Karolew", "Złotno",
//                               "Zdrowie", "Katedralna", "Śródmieście" };

//        name = names[Random.Range(0, names.Length)];
//        color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
//    }

//    public SettlementData(string name, Color color)
//    {
//        this.name = name;
//        this.color = color;
//    }
//}
