using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Estate;

namespace Settlement
{
    public enum DrawAnchorsMode
    {
        Always,
        WhenSelected,
        Never
    }

    public class Line {
        public Vector2 A;
        public Vector2 B;

        public Line(Vector2 A, Vector2 B)
        {
            this.A = A;
            this.B = B;
        }

        public float getY(float x)
        {
            return (((A.y - B.y) / (A.x - B.x)) * x) + (A.y - (((A.y - B.y) / (A.x - B.x)) * A.x));
        }


        public bool isXonSegment(float x)
        {
            if (A.x <= x && x <= B.x) return true;
            else return false;
        }

        public bool isYonSegment(float y)
        {
            if (A.y <= y && y <= B.y) return true;
            else return false;
        }

        public float intersectX (Line oth)
        {
            //TODO: PRZERÓB
            float lck =  (oth.A.y - ((oth.A.y - oth.B.y)/(oth.A.x-oth.B.x)) * oth.A.x);
                  lck -= (this.A.y - ((this.A.y - this.B.y) / (this.A.x - this.B.x)) * this.A.x);
            float mnk =  (this.A.y-this.B.y)/(this.A.x-this.B.x);
                  mnk -= (oth.A.y - oth.B.y) / (oth.A.x - oth.B.x);

            //float ret = (oth.A.y - ((oth.A.y - oth.B.y)/(oth.A.x - oth.B.x)) * oth.A.x)-(this.A.y - ((this.A.y - this.B.y)/(this.A.x - this.B.x)) * this.A.x)
             //(((this.A.y - this.B.y)/(this.A.x - this.B.x))-((oth.A.y - oth.B.y)/(oth.A.x - oth.B.x)));
            return lck/mnk;
        } 

        public bool insideSegment(float x)
        {
            if (x >= A.x && x <= B.x) return true;
            else return false;
        }

        public override string ToString()
        {
            return string.Format("A: {0} B: {1}", A.ToString(), B.ToString());
        }
    }


    public class SettlementBase : MonoBehaviour
    {
        public bool DrawValues;
        public bool DrawGrid;
        public bool DrawGhosts;
        public Vector4 borders;
        public Vector2 blockSize;
        private Vector2 tempBlockSize;

        public Vector2 blockCount;
        public Vector2 rectSize;

        public int[,] blockMap;
        
        public DrawAnchorsMode DrawAnchors;

        private List<int> outerFields;

        public List<Rect> estates;


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

            blockCount = new Vector2(Mathf.Ceil(rectSize.x / blockSize.x), Mathf.Ceil(rectSize.y / blockSize.y));

            GenerateSettlement();
           
            //Vector2 blockOffset = Vector2.zero;
            //while ((int)rectSize.x % blockSize.x != 0)
            //{
            //    blockSize.x++;
            //    blockOffset.x++;
            //}
            //blockCount.x = Mathf.Abs((int)(rectSize.x / blockSize.x));

            //while ((int)rect-Size.y % blockSize.y != 0)
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
            if (DrawGrid)
            {
                Gizmos.color = Color.grey;
                Gizmos.DrawLine(this.transform.position + new Vector3(borders.x, 0f, borders.y), this.transform.position + new Vector3(borders.x, 0f, borders.w));
                Gizmos.DrawLine(this.transform.position + new Vector3(borders.x, 0f, borders.w), this.transform.position + new Vector3(borders.z, 0f, borders.w));
                Gizmos.DrawLine(this.transform.position + new Vector3(borders.z, 0f, borders.w), this.transform.position + new Vector3(borders.z, 0f, borders.y));
                Gizmos.DrawLine(this.transform.position + new Vector3(borders.z, 0f, borders.y), this.transform.position + new Vector3(borders.x, 0f, borders.y));

                float offset = borders.x;// + (rectSize.x % blockSize.x)/2;

                //tempBlockSize.x = Mathf.Floor(rectSize.x / blockSize.x) + (rectSize.x % blockSize.x);
                //      tempBlockSize.y = Mathf.Floor(rectSize.y / blockSize.y) + (rectSize.y % ;

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
            }
            if (DrawGhosts)
            {
                Gizmos.color = new Color(1f, 1f, 1f, 0.75f);

                for (int i = 0; i < estates.Count; i++)
                {
                    Gizmos.DrawCube(this.transform.position + new Vector3(estates[i].x + estates[i].width / 2, 15f, estates[i].y + estates[i].height / 2),
                                    new Vector3(estates[i].width, 30f, estates[i].height));
                }
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

        /// <summary>
        /// If returns 0 - it's outside the figure
        /// If return
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int CheckRect(int x, int y)
        {
            Rect rect = new Rect(x*blockSize.x + (rectSize.x % blockSize.x)/2 + borders.x,
                                y * blockSize.y + (rectSize.x % blockSize.x)/2 + borders.y,
                                blockSize.x,blockSize.y);

            Line P;
            float ky;
            for (int i = 0; i < Anchors.Count; i++ )
            {
                P = new Line(Anchors[i], Anchors[(i != Anchors.Count - 1 ? i + 1 : 0)]);
                if(P.isXonSegment(rect.x))
                {
                    ky = P.getY(rect.x);
                    if (rect.y <= ky && ky <= rect.y + rect.height) 
                        return 1;            
                }
                else if(P.isXonSegment(rect.x+rect.width))
                {
                    ky = P.getY(rect.x+rect.width);
                    if (rect.y <= ky && ky <= rect.y + rect.height)
                        return 1;      
                }
            }
            return 0;
            //Line AB = new Line(new Vector2(rect.x, rect.y), new Vector2(rect.x + rect.width, rect.y));
            //Line BC = new Line(new Vector2(rect.x + rect.width, rect.y), new Vector2(rect.x + rect.width, rect.y + rect.height));
            //Line CD = new Line(new Vector2(rect.x + rect.width, rect.y + rect.height), new Vector2(rect.x, rect.y + rect.height));
            //Line DA = new Line(new Vector2(rect.x, rect.y + rect.height), new Vector2(rect.x, rect.y));

            //Line PQ;
            //float ABx, CDx;
            //bool isInside = false;

            //for(int i=0; i<Anchors.Count; i++)
            //{
            //    PQ = new Line(Anchors[i], Anchors[(i != Anchors.Count - 1 ? i + 1 : 0)]);
            //    isInside = false;
            //    ABx = AB.intersectX(PQ);
            //    CDx = CD.intersectX(PQ);

            //    //Horizontal lines test
            //    if (((AB.A.x <= ABx && ABx <= AB.B.x) && (PQ.A.x <= ABx && ABx <= PQ.B.x))
            //      || ((CD.A.x <= CDx && CDx <= CD.B.x) && (PQ.A.x <= CDx && CDx <= PQ.B.x)))                    
            //        return 1;

            //    //Vertical lines test
            //    if ((PQ.A.x <= DA.A.x && DA.A.x <= PQ.B.x) || (PQ.A.x <= BC.A.x && BC.A.x <= PQ.B.x))
            //        return 1;
            //}
            //return 0;
        }


        public void DebugWriteTable(int[,] table, int sizeX, int sizeY)
        {
            string debug = "";
            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    debug += table[j, i] + " ";
                }
                debug += '\n';
            }

            Debug.Log(debug);
        }


        public int CoordX (float x)
        {
            return (int)((x - borders.x) / blockSize.x);
        }

        public int CoordY (float y)
        {
            return (int)((y - borders.y) / blockSize.y);
        }

        public Vector2 WorldToArrayCoord(Vector2 coord)
        {
            return new Vector2(CoordX(coord.x), CoordY(coord.y));
        }

        public Vector2 ArrayToWorldCoord(Vector2 coord)
        {
            return new Vector2(borders.x + (blockSize.x * coord.x), borders.y + (blockSize.y * coord.y));
        }

        public void DrawLineBresenham(Line line) // Bresenham's line algorithm implementation
        {
            bool steep = Mathf.Abs(line.B.y - line.A.y) > Mathf.Abs(line.B.x - line.A.x);
            if (steep)
            {
                line.A = new Vector2(line.A.y, line.A.x);
                line.B = new Vector2(line.B.y, line.B.x);
            }

            if (line.A.x > line.B.x)
            {
                Vector2 tmp = line.A;
                line.A = line.B;
                line.B = line.A;
            }

            // Bresenham's line algorithm
            //const bool steep = (fabs(y2 - y1) > fabs(x2 - x1));
            //if (steep)
            //{
            //    std::swap(x1, y1);
            //    std::swap(x2, y2);
            //}

            //if (x1 > x2)
            //{
            //    std::swap(x1, x2);
            //    std::swap(y1, y2);
            //}


            float dx = line.B.x - line.A.x;
            float dy = Mathf.Abs(line.B.y - line.A.y);
            float error = dx / 2.0f;
            int yStep = (line.A.y < line.B.y) ? 1 : -1;
            int y = (int)line.A.y;
            int maxX = (int)line.B.x;

            //const float dx = x2 - x1;
            //const float dy = fabs(y2 - y1);

            //float error = dx / 2.0f;
            //const int ystep = (y1 < y2) ? 1 : -1;
            //int y = (int)y1;

            //const int maxX = (int)x2;
            int Xp, Yp;
            for(int x = (int)line.A.x; x < maxX; x++)
            {
                if(steep)
                {
                    if(y<blockCount.x && x < blockCount.y)
                    blockMap[y, x] = 1;
                }
                else
                {
                    if (x < blockCount.x && y < blockCount.y)
                    blockMap[x, y] = 1;
                }

                error -= dy;
                if(error < 0)
                {
                    y += yStep;
                    error += dx;
                }
            }

            //for (int x = (int)x1; x < maxX; x++)
            //{
            //    if (steep)
            //    {
            //        SetPixel(y, x, color);
            //    }
            //    else
            //    {
            //        SetPixel(x, y, color);
            //    }

            //    error -= dy;
            //    if (error < 0)
            //    {
            //        y += ystep;
            //        error += dx;
            //    }
            //}
        }

    

        public float GetY(Line line, float x)
        {
            float y = x * ((line.A.y - line.B.y) / (line.A.x - line.B.x)) + (line.A.y - (line.A.x * ((line.A.y - line.B.y) / (line.A.x - line.B.x))));
            return y;
        }

        public float GetX(Line line, float y)
        {
            float x = (((y - line.A.y) * (line.B.x - line.A.x)) / (line.B.y - line.A.y)) + line.A.x;
            return x;
        }

        public void DrawLine(Line line) // Custom algorithm
        {
            Vector2 arrayPoint;
            Vector2 pointA = WorldToArrayCoord(line.A);
            Vector2 pointB = WorldToArrayCoord(line.B);

            if (pointA.x != pointB.x)
            {
                float offsetX = line.A.x;
                float offsetY;
                while (line.A.x < line.B.x ? (offsetX < line.B.x) : (offsetX > line.B.x))
                {
                    offsetY = GetY(line, offsetX);
                    arrayPoint = WorldToArrayCoord(new Vector2(offsetX, offsetY));
                    blockMap[(int)arrayPoint.x, (int)arrayPoint.y] = 1;
                    offsetX += (line.A.x < line.B.x ? 1 : -1) * blockSize.x / 25;
                }
            }
            else
            {
                float offsetX;
                float offsetY = line.A.y;
                while (line.A.y < line.B.y ? (offsetY < line.B.y) : (offsetY > line.B.y))
                {
                    offsetX = GetX(line, offsetY);
                    arrayPoint = WorldToArrayCoord(new Vector2(offsetX, offsetY));
                    blockMap[(int)arrayPoint.x, (int)arrayPoint.y] = 1;
                    offsetY += (line.A.y < line.B.y ? 1 : -1) * blockSize.y / 25;
                }
            }
        }

        public void FloodFill (Vector2 point, int val)
        {
            if (point.x >= blockCount.x || point.y >= blockCount.y || point.x < 0 || point.y < 0)
            {
                if (!outerFields.Exists(x => x == val)) outerFields.Add(val);
                return;
            }

            if (blockMap[(int)point.x, (int)point.y] != 0) return;

            blockMap[(int)point.x, (int)point.y] = val;

            FloodFill(new Vector2(point.x, point.y - 1), val);
            FloodFill(new Vector2(point.x+1, point.y), val);
            FloodFill(new Vector2(point.x, point.y + 1), val);
            FloodFill(new Vector2(point.x - 1, point.y), val);
            return;
        }

        public void MakeGhost(int i, int j, int i2, int j2)
        {
            Vector2 newCoordA = ArrayToWorldCoord(new Vector2(i, j));
            Vector2 newCoordB = ArrayToWorldCoord(new Vector2(i2, j2));
            Rect ghost = new Rect(newCoordA, new Vector2(newCoordB.x - newCoordA.x + blockSize.x, newCoordB.y - newCoordA.y + blockSize.y));
            estates.Add(ghost);
        }

        public void GenerateSettlement()
        {
            Debug.Log("Step 1: Determining block position");

            blockMap = new int[Mathf.CeilToInt(blockCount.x), Mathf.CeilToInt(blockCount.y)];
            for (int j = 0; j < blockCount.y; j++)
            {
                for (int i = 0; i < blockCount.x; i++)
                {

                    blockMap[i, j] = 0;
                }
            }

            for(int i = 0; i < Anchors.Count-1; i++)
            {
                DrawLine(new Line(Anchors[i], Anchors[i + 1]));                                  
            }

            DrawLine(new Line(Anchors[Anchors.Count-1], Anchors[0]));

            int val = 1;
            outerFields = new List<int>();
            estates = new List<Rect>();

            for (int j = 0; j < blockCount.y; j++)
            {
                for (int i = 0; i < blockCount.x; i++)
                {
                    if (blockMap[i, j] == 0) FloodFill(new Vector2(i, j), ++val);
                }
            }

            for (int j = 0; j < blockCount.y; j++)
            {
                for (int i = 0; i < blockCount.x; i++)
                {
                    if (outerFields.Exists(x => x == blockMap[i, j])) blockMap[i, j] = 0;
                    else if (blockMap[i, j] != 1) blockMap[i, j] = 2;
                }
            }

            //Horizontal tests
            for (int j = 0; j < blockCount.y; j++)
            {
                for (int i = 0; i < blockCount.x; i++)
                {
                    if(blockMap[i, j]==2)
                    {                        
                        if(blockMap[i+1,j]==2)
                        {
                            blockMap[i, j] = blockMap[i + 1, j] = 3;

                            MakeGhost(i, j, i + 1, j);
                        }
                    }
                }
            }

            for (int j = 0; j < blockCount.y; j++)
            {
                for (int i = 0; i < blockCount.x; i++)
                {
                    if (blockMap[i, j] == 2)
                    {
                        MakeGhost(i, j, i, j);
                    }
                }
            }

            //for (int j = 0; j < blockCount.y; j++)
            //{
            //    if (blockMap[0, j] > 1) FloodFill(new Vector2(0, j), 0);
            //    if (blockMap[(int)blockCount.x - 1, j] > 1) FloodFill(new Vector2((int)blockCount.x - 1, j), 0);
            //}

            //for (int i = 0; i < blockCount.x; i++)
            //{
            //    if (blockMap[i, 0] != 1 && blockMap[i, 0] != 0) FloodFill(new Vector2(i, 0), 0);
            //    if (blockMap[i, (int)blockCount.y - 1] != 1 && blockMap[i, (int)blockCount.y - 1] != 0) FloodFill(new Vector2(i, (int)blockCount.y - 1), 0);
            //}

            

            //List<int> pool = new List<int>();
            //int prev, next;
            //bool fill = false;

            //for (int j = 0; j < blockCount.y; j++)
            //{
            //    pool.Clear();
            //    prev = 0;
            //    for (int i = 0; i < blockCount.x; i++)
            //    {
            //        if (fill) pool.Add(i);

            //        if (i+1 < blockCount.x) next = blockMap[i + 1, j];
            //        else next = 0;
            //        if (blockMap[i, j] == 1 && next == 0)
            //        {
            //            if (fill)
            //            {
            //                fill = false;
            //                for (int it = 0; it < pool.Count; it++) blockMap[it, j] = 2;
            //                pool.Clear();
            //            }
            //            else fill = true;
            //        }                
                                       
            //    }                
            //}
                    //int mark, previousVal = 0, nextVal;
                    //for(int j = 0; j < blockCount.y; j++)
                    //{
                    //    mark = 0;
                    //    for (int i = 0; i < blockCount.x; i++)
                    //    {
                    //        if(i< blockCount.x) nextVal = i
                    //        tmp = blockMap[i, j];
                    //        if (blockMap[i, j] == 1 && previousVal == 0 && mark == 0) mark = 2;
                    //        else if (blockMap[i, j] == 1 && previousVal == 0 &&  mark == 2) mark = 0;
                    //        else blockMap[i, j] = mark;
                    //        previousVal = tmp;
                    //    }
                    //}

                    //DebugWriteTable(blockMap, (int)blockCount.x, (int)blockCount.y); //TODO: Delete this

                    //Line AB = new Line(new Vector2(2, 2), new Vector2(4, 2));
                    //Line PQ = new Line(new Vector2(2, 1), new Vector2(4, 3));

                    //Debug.Log(AB.intersectX(PQ));
                }

        public void GenerateBuildings()
        {
            int buildNr = 1;
            for(int i=0; i<estates.Count; i++)
            {
                Rect rect = estates[i];

                GameObject estateObject = new GameObject();
                
                    estateObject.name = string.Format("Estate nr {0}", buildNr++);
                    estateObject.transform.parent = this.transform;
                    estateObject.transform.localPosition = new Vector3(rect.x,0f,rect.y);
                    EstateBase estate = estateObject.AddComponent<EstateBase>();
                    estate.outerBounds = new Rect(0,0,rect.width,rect.height);
                    estate.buildingBounds = new Rect(5, 5, rect.width - 10, rect.height - 10);
                    estate.DrawEstate();
            }
        }


        public void DestroyBuildings()
        {
            int children = transform.childCount;
            for (int i = children - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
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