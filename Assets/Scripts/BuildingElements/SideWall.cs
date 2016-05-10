using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Building
{
    public class SideWall : Drawable 
    {
        public Vector3 PointA;
        public Vector3 PointB;
        public Vector3 PointC;
        public Vector3 PointD;
        public Texture2D tex;

        public Vector3 startingPoint;
        public float foundationHeight;
        public Vector2 windowSize;
        public float wallHeight;

        public SettlementData data;

        public bool eastSide;

        public void CreateWriting(int nr)
        {
            

        }

        public void Draw()
        {
              
            this.vertices = new List<Vector3>();
            this.tris = new List<int>();
            this.uvs = new List<Vector2>();

            int vertOffset = 0;

            int floorsCount = Mathf.FloorToInt(wallHeight / (windowSize.y * 2));
            float singlePanelWidth = windowSize.x * 2;
            float singlePanelHeight = Mathf.Abs(wallHeight / floorsCount);
            int panelsCount = Mathf.FloorToInt(Mathf.Abs(PointA.z - PointD.z) / singlePanelWidth); //Mathf.FloorToInt(Mathf.Abs(PointA.z - PointD.z) / singlePanelWidth);


            Vector2 offset = Vector2.zero;

            if (foundationHeight > 0)
            {
                for (int i = 0; i < panelsCount; i++)
                {
                    offset = new Vector2(startingPoint.z + (i * singlePanelWidth * (eastSide ? 1 : -1)), 0);
                    this.vertices.Add(new Vector3(startingPoint.x, offset.y, offset.x));
                    this.vertices.Add(new Vector3(startingPoint.x, offset.y + foundationHeight, offset.x));
                    this.vertices.Add(new Vector3(startingPoint.x, offset.y + foundationHeight, offset.x + singlePanelWidth * (eastSide?1:-1)));
                    this.vertices.Add(new Vector3(startingPoint.x, offset.y, offset.x + singlePanelWidth * (eastSide ? 1 : -1)));

                    if (data.wallsFurbished)
                    {
                        this.uvs.Add(new Vector2(0.5f, 0.75f + (singlePanelHeight - foundationHeight) * 0.25f / singlePanelHeight));
                        this.uvs.Add(new Vector2(0.5f, 1f));
                        this.uvs.Add(new Vector2(0.75f, 1f));
                        this.uvs.Add(new Vector2(0.75f, 0.75f + (singlePanelHeight - foundationHeight) * 0.25f / singlePanelHeight));
                    }
                    else
                    {
                        this.uvs.Add(new Vector2(0.75f, (singlePanelHeight - foundationHeight) * 0.25f / singlePanelHeight));
                        this.uvs.Add(new Vector2(0.75f, 0.25f));
                        this.uvs.Add(new Vector2(1f, 0.25f));
                        this.uvs.Add(new Vector2(1f,  (singlePanelHeight - foundationHeight) * 0.25f / singlePanelHeight));
                    }
                    this.tris.Add(vertOffset);
                    this.tris.Add(vertOffset + 1);
                    this.tris.Add(vertOffset + 2);

                    this.tris.Add(vertOffset + 2);
                    this.tris.Add(vertOffset + 3);
                    this.tris.Add(vertOffset);
                    vertOffset += 4;
                }

            }

            for (int floor = 0; floor < floorsCount; ++floor)
            {
                for (int i = 0; i < panelsCount; i++)
                {
                    offset = new Vector2(startingPoint.z + (i * singlePanelWidth * (eastSide ? 1 : -1)), floor * singlePanelHeight + foundationHeight);
                    this.vertices.Add(new Vector3(startingPoint.x, offset.y, offset.x));
                    this.vertices.Add(new Vector3(startingPoint.x, offset.y + singlePanelHeight, offset.x));
                    this.vertices.Add(new Vector3(startingPoint.x, offset.y + singlePanelHeight, offset.x + singlePanelWidth * (eastSide ? 1 : -1)));
                    this.vertices.Add(new Vector3(startingPoint.x, offset.y, offset.x + singlePanelWidth * (eastSide ? 1 : -1)));
                    
                    Vector2 UVHandle = UV.BareWall;
                    if (data.wallsFurbished)
                    {
                        if (floor < 2)
                            UVHandle = UV.AdditionalWall2;
                        else UVHandle = UV.MainWall;

                            if(i==1)
                            {
                                UVHandle = UV.AdditionalWall1;
                            }
                    }
                        

          

                    this.AddUVs(UVHandle);

                    this.tris.Add(vertOffset);
                    this.tris.Add(vertOffset + 1);
                    this.tris.Add(vertOffset + 2);

                    this.tris.Add(vertOffset + 2);
                    this.tris.Add(vertOffset + 3);
                    this.tris.Add(vertOffset);
                    vertOffset += 4;

                   
                }
            }

            //if (floorsCount >= 5 && panelsCount >= 5)
            //{
            //    GameObject textMeshObj = new GameObject();
            //    textMeshObj.name = "Building Number";
            //    textMeshObj.transform.parent = this.transform;
            //    TextMesh textMesh = textMeshObj.
            //}

            base.Draw();
        }
    }
}