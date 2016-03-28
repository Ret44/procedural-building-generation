using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace OldSettlement
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

        public Vector4 borders;
        public Vector2 blockSize;
        private Vector2 tempBlockSize;

        public Vector2 blockCount;
        public Vector2 rectSize;

        public int[,] blockMap;
        
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

            blockCount = new Vector2(Mathf.Floor(rectSize.x / blockSize.x), Mathf.Floor(rectSize.y / blockSize.y));

           
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
            Gizmos.color = Color.grey;
            Gizmos.DrawLine(this.transform.position + new Vector3(borders.x, 0f, borders.y), this.transform.position + new Vector3(borders.x, 0f, borders.w));
            Gizmos.DrawLine(this.transform.position + new Vector3(borders.x, 0f, borders.w), this.transform.position + new Vector3(borders.z, 0f, borders.w));
            Gizmos.DrawLine(this.transform.position + new Vector3(borders.z, 0f, borders.w), this.transform.position + new Vector3(borders.z, 0f, borders.y));
            Gizmos.DrawLine(this.transform.position + new Vector3(borders.z, 0f, borders.y), this.transform.position + new Vector3(borders.x, 0f, borders.y));

            float offset = borders.x + (rectSize.x % blockSize.x)/2;

            //tempBlockSize.x = Mathf.Floor(rectSize.x / blockSize.x) + (rectSize.x % blockSize.x);
      //      tempBlockSize.y = Mathf.Floor(rectSize.y / blockSize.y) + (rectSize.y % ;
          
            //Debug.Log(Mathf.Floor(rectSize.x / blockSize.x) + " " + Mathf.Floor(rectSize.y / blockSize.y));

            while (offset <= borders.z)
            {
                Gizmos.DrawLine(this.transform.position + new Vector3(offset, 0f, borders.y), this.transform.position + new Vector3(offset, 0f, borders.w));
                offset += blockSize.x;
            }

            offset = borders.y + (rectSize.y % blockSize.y) / 2;
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

        public void GenerateSettlement()
        {
            Debug.Log("Step 1: Determining block position");

            blockMap = new int[Mathf.FloorToInt(blockCount.x), Mathf.FloorToInt(blockCount.y)];
            for (int j = 0; j < blockCount.y; j++)
            {
                for (int i = 0; i < blockCount.x; i++)
                {

                    blockMap[i, j] = CheckRect(i, j);
                }
            }

            DebugWriteTable(blockMap, (int)blockCount.x, (int)blockCount.y); //TODO: Delete this

            //Line AB = new Line(new Vector2(2, 2), new Vector2(4, 2));
            //Line PQ = new Line(new Vector2(2, 1), new Vector2(4, 3));

            //Debug.Log(AB.intersectX(PQ));
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