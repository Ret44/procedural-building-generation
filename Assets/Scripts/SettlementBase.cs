using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Settlement
{
    public enum DrawAnchorsMode
    {
        Always,
        WhenSelected,
        Never
    }


    public class SettlementBase : MonoBehaviour
    {

        public Vector4 borders;
        public Vector2 blockSize;
        private Vector2 tempBlockSize;

        public Vector2 blockCount;
        public Vector2 rectSize;
        
        public DrawAnchorsMode DrawAnchors;
        


        //[HideInInspector]
        //public float RoadWidth = 6;
        //[HideInInspector]
        //public float DistanceFromRoad = 5;
        //[HideInInspector]
        //public float MinDistanceBetweenBuildings =  8;
        //public float MaxDistanceBetweenBuildings = 35;

        public List<Vector2> Anchors = new List<Vector2>
        {
          Vector2.zero + new Vector2(0, 0),
          Vector2.zero + new Vector2(0, 15),
          Vector2.zero + new Vector2(15, 15),
          Vector2.zero + new Vector2(15, 0)
        };

       public void OnUpdateAnchors()
        {
            borders = Vector4.zero;

                for (int i = 0; i < Anchors.Count; i++)
               { 
                    if (borders.x > Anchors[i].x) borders.x = Anchors[i].x;
                    if (borders.z < Anchors[i].x) borders.z = Anchors[i].x;
                    if (borders.y > Anchors[i].y) borders.y = Anchors[i].y;
                    if (borders.w < Anchors[i].y) borders.w = Anchors[i].y;
                }
        

            rectSize = new Vector2(borders.z - borders.x, borders.w - borders.y);
                        
            //Vector2 blockOffset = Vector2.zero;
            //while ((int)rectSize.x % blockSize.x != 0)
            //{
            //    blockSize.x++;
            //    blockOffset.x++;
            //}
            //blockCount.x = Mathf.Abs((int)(rectSize.x / blockSize.x));

            //while ((int)rectSize.y % blockSize.y != 0)
            //{
            //    blockSize.y++;
            //    blockOffset.y++;
            //}
            //blockCount.y = Mathf.Abs((int)(rectSize.y / blockSize.y));
        }

        void OnDrawAnchors()
        {

            if (Anchors.Count > 1)
            {
                for (int i = 0; i < Anchors.Count - 1; i++)
                {
                    Gizmos.DrawLine(this.transform.position + new Vector3(Anchors[i].x, 0f, Anchors[i].y),
                                    this.transform.position + new Vector3(Anchors[i + 1].x, 0f, Anchors[i + 1].y));
                }
                Gizmos.DrawLine(this.transform.position + new Vector3(Anchors[Anchors.Count - 1].x, 0f, Anchors[Anchors.Count - 1].y),
                                this.transform.position + new Vector3(Anchors[0].x, 0f, Anchors[0].y));
            }
            Gizmos.color = Color.grey;
            Gizmos.DrawLine(this.transform.position + new Vector3(borders.x, 0f, borders.y), this.transform.position + new Vector3(borders.x, 0f, borders.w));
            Gizmos.DrawLine(this.transform.position + new Vector3(borders.x, 0f, borders.w), this.transform.position + new Vector3(borders.z, 0f, borders.w));
            Gizmos.DrawLine(this.transform.position + new Vector3(borders.z, 0f, borders.w), this.transform.position + new Vector3(borders.z, 0f, borders.y));
            Gizmos.DrawLine(this.transform.position + new Vector3(borders.z, 0f, borders.y), this.transform.position + new Vector3(borders.x, 0f, borders.y));

            float offset = borders.x;// + (rectSize.x % blockSize.x)/2;

            //tempBlockSize.x = Mathf.Floor(rectSize.x / blockSize.x) + (rectSize.x % blockSize.x);
      //      tempBlockSize.y = Mathf.Floor(rectSize.y / blockSize.y) + (rectSize.y % ;
            blockCount = new Vector2(Mathf.Floor(rectSize.x / blockSize.x),Mathf.Floor(rectSize.y / blockSize.y));
            //Debug.Log(Mathf.Floor(rectSize.x / blockSize.x) + " " + Mathf.Floor(rectSize.y / blockSize.y));

            while (offset <= borders.z)
            {
                Gizmos.DrawLine(this.transform.position + new Vector3(offset, 0f, borders.y), this.transform.position + new Vector3(offset, 0f, borders.w));
                offset += blockSize.x;
            }

            offset = borders.y;// + (rectSize.y % blockSize.y) / 2;
            while (offset <= borders.w)
            {
                Gizmos.DrawLine(this.transform.position + new Vector3(borders.x, 0f, offset), this.transform.position + new Vector3(borders.z, 0f, offset));
                offset += blockSize.y;
            }
            Gizmos.color = Color.white;

        }

        void OnDrawGizmos()
        {
            if (DrawAnchors == DrawAnchorsMode.Always)
                OnDrawAnchors();
        }

        void OnDrawGizmosSelected()
        {
            if (DrawAnchors == DrawAnchorsMode.WhenSelected)
                OnDrawAnchors();
        }

        
        public void AddAnchor()
        {
            Vector2 newAnchor = (Anchors.Count > 1 ? 
                  new Vector2((Anchors[Anchors.Count - 1].x + Anchors[0].x)/2, (Anchors[Anchors.Count - 1].y + Anchors[0].y) / 2)
                : Anchors[0] + new Vector2(1f, 0f));
            Anchors.Add(newAnchor);
        }

        public void AddAnchorAtPos(int i)
        {
            Vector2 newAnchor = (Anchors.Count > 1 ?
                  new Vector2((Anchors[i].x + Anchors[i+1].x) / 2, (Anchors[i].y + Anchors[i+1].y) / 2)
                : Anchors[0] + new Vector2(1f, 0f));
            Anchors.Insert(i+1,newAnchor);
        }

        public void RemoveAnchor(int i)
        {
            Anchors.RemoveAt(i);
        }


        public void DebugWriteTable(int[,] table, int sizeX, int sizeY)
        {
            string debug = "";
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    debug += table[i, j] + " ";
                }
                debug += '\n';
            }
            Debug.Log(debug);
        }

        public void GenerateSettlement()
        {
            Debug.Log("Step 1: Determining block position");

            //TODO: This will be null if gizmos are not drawn on scene. Need to be changed.
         


            int[,] blockMap = new int[(int)blockCount.x, (int)blockCount.y];
            for (int i = 0; i < blockCount.x; i++)
                for (int j = 0; j < blockCount.y; j++)
                    blockMap[i, j] = 0;


            for (int iter = 0; iter < Anchors.Count; iter++)
            {
                int x0 = (int)Mathf.Abs(Anchors[iter].x / blockSize.x);
                int y0 = (int)Mathf.Abs(Anchors[iter].y / blockSize.y);
                int x1 = (int)Mathf.Abs(Anchors[(iter==Anchors.Count-1?0:iter + 1)].x / blockSize.x);
                int y1 = (int)Mathf.Abs(Anchors[(iter == Anchors.Count - 1 ? 0 : iter + 1)].y / blockSize.y);

                int dy = (int)(y1 - y0);
                int dx = (int)(x1 - x0);
                int stepx, stepy;

                if (dy < 0) { dy = -dy; stepy = -1; }
                else { stepy = 1; }
                if (dx < 0) { dx = -dx; stepx = -1; }
                else { stepx = 1; }
                dy <<= 1;
                dx <<= 1;

                float fraction = 0;

                blockMap[x0, y0] = 1;                
                if (dx > dy)
                {
                    fraction = dy - (dx >> 1);
                    while (Mathf.Abs(x0 - x1) > 1)
                    {
                        if (fraction >= 0)
                        {
                            y0 += stepy;
                            fraction -= dx;
                        }
                        x0 += stepx;
                        fraction += dy;
                        blockMap[x0, y0] = 1;
                    }
                }
                else {
                    fraction = dx - (dy >> 1);
                    while (Mathf.Abs(y0 - y1) > 1)
                    {
                        if (fraction >= 0)
                        {
                            x0 += stepx;
                            fraction -= dy;
                        }
                        y0 += stepy;
                        fraction += dx;
                        blockMap[x0, y0] = 1;
                    }

                }
            }
            
            DebugWriteTable(blockMap, (int)blockCount.x, (int)blockCount.y);

        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}