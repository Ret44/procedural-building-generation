using UnityEngine;
using Building;
using System.Collections;
using System.Collections.Generic;

public class BuildingBase : MonoBehaviour
{

    public bool DrawGizmos;

    [Header("Gizmos position")]
    public Vector3 PointA = new Vector3(-0.5f, 0f, 0.5f);
    public Vector3 PointB = new Vector3(0.5f, 0f, 0.5f);
    public Vector3 PointC = new Vector3(0.5f, 0f, -0.5f);
    public Vector3 PointD = new Vector3(-0.5f, 0f, -0.5f);

    [Header("Building settings")]
    public float height = 1;
    public float width = 1;
    public float depth = 1;

    public float windowOffset;
    public float windowFrameSize;

    public float foundationHeight;

    public float balconyLength;

    public long elapsedTime;

        public Material buildingMaterial;
    
    public Vector2 windowSize = new Vector2(0.5f, 2f);

    private Vector3 gizmoSize = Vector3.one * 0.25f;

    public Vector3 GlobalPosition(Vector3 point)
    {
        return this.transform.position + point;
    }

    public void UpdatePoints()
    {
        PointA = Vector3.zero;
        PointB = new Vector3(width, 0f, 0f);
        PointC = new Vector3(width, 0f, depth);
        PointD = new Vector3(0f, 0f, depth);
    }

    void OnDrawGizmosSelected()
    {
        if (DrawGizmos)
        {
            //Gizmos.DrawCube(GlobalPosition(PointA), gizmoSize);
            //Gizmos.DrawCube(GlobalPosition(PointB), gizmoSize);
            //Gizmos.DrawCube(GlobalPosition(PointC), gizmoSize);
            //Gizmos.DrawCube(GlobalPosition(PointD), gizmoSize);

            //Gizmos.DrawCube(GlobalPosition(PointA + new Vector3(0f,height,0f)), gizmoSize);
            //Gizmos.DrawCube(GlobalPosition(PointB + new Vector3(0f, height, 0f)), gizmoSize);
            //Gizmos.DrawCube(GlobalPosition(PointC + new Vector3(0f, height, 0f)), gizmoSize);
            //Gizmos.DrawCube(GlobalPosition(PointD + new Vector3(0f, height, 0f)), gizmoSize);


            Gizmos.DrawLine(GlobalPosition(PointA), GlobalPosition(PointB));
            Gizmos.DrawLine(GlobalPosition(PointB), GlobalPosition(PointC));
            Gizmos.DrawLine(GlobalPosition(PointC), GlobalPosition(PointD));
            Gizmos.DrawLine(GlobalPosition(PointD), GlobalPosition(PointA));

            Gizmos.DrawLine(GlobalPosition(PointA), GlobalPosition(PointA + new Vector3(0f, height, 0f)));
            Gizmos.DrawLine(GlobalPosition(PointB), GlobalPosition(PointB + new Vector3(0f, height, 0f)));
            Gizmos.DrawLine(GlobalPosition(PointC), GlobalPosition(PointC + new Vector3(0f, height, 0f)));
            Gizmos.DrawLine(GlobalPosition(PointD), GlobalPosition(PointD + new Vector3(0f, height, 0f)));

            Gizmos.DrawLine(GlobalPosition(PointA + new Vector3(0f, height, 0f)), GlobalPosition(PointB + new Vector3(0f, height, 0f)));
            Gizmos.DrawLine(GlobalPosition(PointB + new Vector3(0f, height, 0f)), GlobalPosition(PointC + new Vector3(0f, height, 0f)));
            Gizmos.DrawLine(GlobalPosition(PointC + new Vector3(0f, height, 0f)), GlobalPosition(PointD + new Vector3(0f, height, 0f)));
            Gizmos.DrawLine(GlobalPosition(PointD + new Vector3(0f, height, 0f)), GlobalPosition(PointA + new Vector3(0f, height, 0f)));

        }
    }


    public void KillAllChildren() // :D
    {
        int children = transform.childCount;
        for (int i = children - 1; i >= 0; i--)
        {             
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    public GameObject CombineMeshes(List<Drawable> objects)
    {
        GameObject walls = new GameObject();
        Drawable drawComponent = walls.AddComponent<Drawable>();
        for (int i = objects.Count-1; i >= 0; i--)
        {
            for (int x = 0; x < objects[i].vertices.Count; x++)
                drawComponent.vertices.Add(objects[i].vertices[x]);
            for (int x = 0; x < objects[i].tris.Count; x++)
                drawComponent.tris.Add(objects[i].tris[x]);
            DestroyImmediate(objects[i]);
        }

        drawComponent.Draw();
        return walls;
    }

    public void DrawBuilding()
    {
        KillAllChildren();
        System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
        List<Drawable> objects = new List<Drawable>();

        timer.Start();

        // North wall
        GameObject wallN = new GameObject();
        wallN.transform.parent = this.transform;
        wallN.name = "Generated North Wall";
        wallN.transform.localPosition = Vector3.zero;
        Wall wallComponent = wallN.AddComponent<Wall>();
        wallComponent.wallSize = new Vector2(Vector3.Distance(this.PointA, this.PointB), this.height - this.foundationHeight);
        wallComponent.windowOffset = this.windowOffset;
        wallComponent.startingPoint = this.PointA;
        wallComponent.windowSize = this.windowSize;
        wallComponent.windowFrameSize = this.windowFrameSize;
        wallComponent.foundationHeight = this.foundationHeight;
        wallComponent.material = this.buildingMaterial;
        wallComponent.Draw();
     // 

        objects.Add(wallComponent);

        // South wall
        GameObject wallS = new GameObject();
        wallS.name = "Generated South Wall";
        wallS.transform.parent = this.transform;
        wallS.transform.localPosition = Vector3.zero;       
        BalconyWall bWallComponent = wallS.AddComponent<BalconyWall>();
        bWallComponent.wallSize = new Vector2(Vector3.Distance(this.PointA, this.PointB), this.height - this.foundationHeight);
        bWallComponent.windowOffset = this.windowOffset;
        bWallComponent.startingPoint = this.PointA;
        bWallComponent.windowSize = this.windowSize;
        bWallComponent.windowFrameSize = this.windowFrameSize;
        bWallComponent.foundationHeight = this.foundationHeight;
        bWallComponent.balconyLength = this.balconyLength;
        bWallComponent.material = this.buildingMaterial;
        bWallComponent.Draw();

        wallS.transform.Rotate(new Vector3(0f, 180f, 0f));
        wallS.transform.localPosition = this.PointC;

        // GameObject wallS = new GameObject();
        //// wallS.transform.Rotate(new Vector3(0f, 180f, 0f));
        // wallS.transform.parent = this.transform;        
        // wallS.name = "Generated South Wall";
        // wallS.transform.localPosition = Vector3.zero;
        // wallComponent = wallS.AddComponent<Wall>();
        // wallComponent.wallSize = new Vector2(Vector3.Distance(this.PointA, this.PointB), this.height);
        // wallComponent.windowOffset = this.windowOffset;
        // wallComponent.startingPoint = this.PointD;
        // wallComponent.windowSize = this.windowSize;
        // wallComponent.windowFrameSize = this.windowFrameSize;
        // wallComponent.Draw();
        // wallComponent.MirrorTris();

        objects.Add(wallComponent);


        // West wall
        GameObject wallW = new GameObject();
        wallW.transform.parent = this.transform;
        wallW.name = "Generated West Wall";
        wallW.transform.localPosition = Vector3.zero;
        Panel panelComponent = wallW.AddComponent<Panel>();
        panelComponent.PointA = this.PointD;
        panelComponent.PointB = PointD + new Vector3(0f, height, 0f);
        panelComponent.PointC = PointA + new Vector3(0f, height, 0f); 
        panelComponent.PointD = this.PointA;
        panelComponent.Draw();

        objects.Add(panelComponent);

        // East wall
        GameObject wallE = new GameObject();
        wallE.transform.parent = this.transform;
        wallE.name = "Generated East Wall";
        wallE.transform.localPosition = Vector3.zero;
        panelComponent = wallE.AddComponent<Panel>();
        panelComponent.PointA = this.PointB;
        panelComponent.PointB = PointB + new Vector3(0f, height, 0f);
        panelComponent.PointC = PointC + new Vector3(0f, height, 0f);
        panelComponent.PointD = this.PointC;
        panelComponent.Draw();

        objects.Add(panelComponent);

        //Roof 
        GameObject roof = new GameObject();
        roof.transform.parent = this.transform;
        roof.name = "Generated Roof";
        roof.transform.localPosition = Vector3.zero;
        Roof roofComponent = roof.AddComponent<Roof>();
        roofComponent.depth = this.depth;
        roofComponent.height = this.height;
        roofComponent.wallSize = new Vector2(Vector3.Distance(this.PointA, this.PointB), this.height - this.foundationHeight);
        roofComponent.windowSize = this.windowSize;
        roofComponent.windowOffset = this.windowOffset;
        roofComponent.windowFrameSize = this.windowFrameSize;
        roofComponent.material = this.buildingMaterial;
        roofComponent.Draw();

        //GameObject roof = new GameObject();
        //    roof.transform.parent = this.transform;
        //    roof.name = "Generated Roof";
        //    roof.transform.localPosition = Vector3.zero;
        //    panelComponent = roof.AddComponent<Panel>();
        //    panelComponent.PointA = this.PointD + new Vector3(0f, height, 0f);
        //    panelComponent.PointB = PointC + new Vector3(0f, height, 0f);
        //    panelComponent.PointC = PointB + new Vector3(0f, height, 0f);
        //    panelComponent.PointD = this.PointA + new Vector3(0f, height, 0f);
        //    panelComponent.Draw();

        //  CombineMeshes(objects);

        timer.Stop();

        elapsedTime = timer.ElapsedMilliseconds;
    }

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
