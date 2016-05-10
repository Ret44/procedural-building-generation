using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Building
{
    public class Roof : Drawable
    {
        public float depth;
        public float height;

        public Vector3 PointA;
        public Vector3 PointB;
        public Vector3 PointC;
        public Vector3 PointD;


        public Vector2 wallSize;
        public Vector2 windowSize;
        public float windowOffset;
        public float windowFrameSize;

        public Vector3 startingPoint;

        public List<int> CalculateStairways(int windowCount)
        {
            List<int> stairways = new List<int>();
            List<int> tmp = new List<int>();

            for (int i = 0; i < windowCount; i++)
                tmp.Add(i);


            while (tmp.Count >= 5)
            {
                if (tmp.Count == 5)
                {
                    stairways.Add(tmp[tmp.Count / 2]);
                    return stairways;
                }

                if (tmp.Count == 6)
                {
                    stairways.Add(tmp[1]);
                    stairways.Add(tmp[tmp.Count - 2]);
                    return stairways;
                }

                stairways.Add(tmp[2]);
                for (int i = 2; i >= 0; i--)
                    tmp.RemoveAt(i);

                stairways.Add(tmp[tmp.Count - 3]);
                for (int i = 0; i < 3; ++i)
                    tmp.RemoveAt(tmp.Count - 1);
            }

            return stairways;
        }

        public void Draw()
        {
            this.vertices = new List<Vector3>();
            this.tris = new List<int>();
            this.uvs = new List<Vector2>();

            int windowCount = Mathf.FloorToInt(wallSize.x / (windowSize.x + windowOffset));
            int floorCount = Mathf.FloorToInt(wallSize.y / (windowSize.y * 2));
            //float singlePanelWidth = (wallSize.x - (windowSize.x * windowCount)) / (windowCount + 1);
                        
            float singlePanelWidth = windowSize.x * 2;
            float singlePanelHeight = windowSize.y * 2;
            float cols = (this.PointD.z - this.PointA.z) / singlePanelWidth;
            float rows = wallSize.x / singlePanelHeight;


            bool generateShafts = (floorCount >= 5 ? true : false);

            List<int> stairways = CalculateStairways(windowCount);

            Vector2 offset = Vector2.zero;
            int vertOffset = 0;

            GameObject shaft = new GameObject();
            shaft.name = this.name + " Elevator Shafts";
            shaft.transform.parent = this.transform;
            shaft.transform.localPosition = Vector3.zero;
            ElevatorShaft shafts = shaft.AddComponent<ElevatorShaft>();
            shafts.doorSize = ((wallSize.y / floorCount) - windowSize.y);
            shafts.windowFrameSize = this.windowFrameSize;
            shafts.windowSize = new Vector2(this.windowSize.x * 0.75f, this.windowSize.y);
            shafts.material = this.material;
            shafts.Init();

            for (int col = 0; col < cols; col++)
                
            {
                for (int row = 0; row < rows; row++)
                {
                    offset = new Vector2(row * singlePanelHeight, startingPoint.x + (col * singlePanelWidth));
                    this.vertices.Add(new Vector3(offset.x, height, offset.y));
                    this.vertices.Add(new Vector3(offset.x, height, offset.y + singlePanelWidth));
                    this.vertices.Add(new Vector3(offset.x + singlePanelHeight, height, offset.y + singlePanelWidth));
                    this.vertices.Add(new Vector3(offset.x + singlePanelHeight, height, offset.y));

                    this.AddUVs(UV.Roof);

                    this.tris.Add(vertOffset);
                    this.tris.Add(vertOffset + 1);
                    this.tris.Add(vertOffset + 2);

                    this.tris.Add(vertOffset + 2);
                    this.tris.Add(vertOffset + 3);
                    this.tris.Add(vertOffset);
                    vertOffset += 4;
                }
            }

            //for (int i = 0; i < windowCount; i++)
            //{
            //    offset = new Vector2(i * (singlePanelWidth + windowSize.x), 0f);
            //    this.vertices.Add(new Vector3(offset.x, height, offset.y));
            //    this.vertices.Add(new Vector3(offset.x + singlePanelWidth + windowSize.x, height, offset.y));
            //    this.vertices.Add(new Vector3(offset.x + singlePanelWidth + windowSize.x, height, offset.y + depth));
            //    this.vertices.Add(new Vector3(offset.x, height, offset.y + depth));

            //    this.AddUVs(UV.Roof);

            //    this.tris.Add(vertOffset + 2);
            //    this.tris.Add(vertOffset + 1);
            //    this.tris.Add(vertOffset);

            //    this.tris.Add(vertOffset);
            //    this.tris.Add(vertOffset + 3);
            //    this.tris.Add(vertOffset + 2);
            //    vertOffset += 4;

            //    if (stairways.Count != 0 && stairways[0] == i && generateShafts) // stairway?
            //    {
            //        stairways.RemoveAt(0);
            //        stairways.Add(i);

            //        Rect singleShaft = new Rect(new Vector3(offset.x + (singlePanelWidth * 2) * 0.15f, height, offset.y),
            //                                    new Vector3(offset.x + (singlePanelWidth * 2) * 0.75f + windowSize.x , height, offset.y),
            //                                    new Vector3(offset.x + (singlePanelWidth * 2) * 0.75f + windowSize.x , height, offset.y + (depth * 0.75f)),
            //                                    new Vector3(offset.x + (singlePanelWidth * 2) * 0.15f, height, offset.y + (depth * 0.75f)));
                    

            //        shafts.AddShaft(singleShaft, (wallSize.y / floorCount) * 0.75f);
                    
            //    }               

            //}
            //offset = new Vector2(windowCount * (singlePanelWidth + windowSize.x),0f);
            //this.vertices.Add(new Vector3(offset.x, height, offset.y));
            //this.vertices.Add(new Vector3(offset.x + singlePanelWidth, height, offset.y));
            //this.vertices.Add(new Vector3(offset.x + singlePanelWidth, height, offset.y+ depth));
            //this.vertices.Add(new Vector3(offset.x, height, offset.y + depth));

            //this.AddUVs(UV.Roof);

            //this.tris.Add(vertOffset + 2);
            //this.tris.Add(vertOffset + 1);
            //this.tris.Add(vertOffset);

            //this.tris.Add(vertOffset);
            //this.tris.Add(vertOffset + 3);
            //this.tris.Add(vertOffset + 2);

            //shafts.Draw();
            base.Draw();
        }
    }
}