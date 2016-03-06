using UnityEngine;
using Building;
using System.Collections;
using System.Collections.Generic;


namespace Building
{
    public class Wall : Drawable
    {

        public Vector2 wallSize;
        public Vector2 windowSize;
        public float windowOffset;
        public float windowFrameSize;
        public float foundationHeight;
        public Vector3 startingPoint;
        public Vector3 directionPoint;

        public Vector4 bigPanelTextureUV = new Vector4(0, 0, 117, 219);
        public Vector4 smallPanelTextureUV = new Vector4(117, 0, 102, 60);
           
        public void MirrorTris()
        {
            List<int> newTris = new List<int>();
            for (int i = this.tris.Count - 1; i >= 0; i--)
            {
                newTris.Add(this.tris[i]);
            }
            this.tris = newTris;
            base.Draw();
        }


        public List<int> CalculateStairways (int windowCount)
        {
            List<int> stairways = new List<int>();
            List<int> tmp = new List<int>();
            
            for (int i=0;i< windowCount;i++)
                tmp.Add(i);
            
                        
            while(tmp.Count>=5)
            {
                if (tmp.Count == 5)
                {
                    stairways.Add(tmp[tmp.Count / 2]);
                    return stairways;
                }

                if(tmp.Count == 6)
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
                    tmp.RemoveAt(tmp.Count-1);
            }

            return stairways;
        }

        public void Draw()
        {
            this.vertices = new List<Vector3>();
            this.tris = new List<int>();

            this.uvs = new List<Vector2>();

            int floorCount = Mathf.FloorToInt(wallSize.y / (windowSize.y * 2));
            float floorHeight = (wallSize.y / floorCount);// + (wallSize.y % floorCount);

            int windowCount = Mathf.FloorToInt(wallSize.x / (windowSize.x + windowOffset));

            List<int> stairways = CalculateStairways(windowCount);

            float singlePanelWidth = (wallSize.x - (windowSize.x * windowCount)) / (windowCount + 1);
            float smallPanelHeight = (floorHeight - windowSize.y) / 2;


            // Windows
            GameObject window = new GameObject();
            //    window.transform.position = new Vector3(offset.x + singlePanelWidth, offset.y + smallPanelHeight, startingPoint.z);
            window.name = this.name + " Windows";
            window.transform.parent = this.transform;
            window.transform.localPosition = Vector3.zero;
            Window windows = window.AddComponent<Window>();
            windows.windowFrameSize = this.windowFrameSize;
            windows.Init();

            // Frames
            GameObject frame = new GameObject();
            frame.name = this.name + " Frames";
            frame.transform.parent = this.transform;
            frame.transform.localPosition = Vector3.zero;
            WindowFrame frames = frame.AddComponent<WindowFrame>();
            frames.windowFrameSize = this.windowFrameSize;
            frames.Init();
            
            // Doors
            GameObject door = new GameObject();
            door.name = this.name + " Doors";
            door.transform.parent = this.transform;
            door.transform.localPosition = Vector3.zero;
            Door doors = door.AddComponent<Door>();
            doors.frameSize = this.windowFrameSize;
            doors.Init();

            Vector2 offset = new Vector2(0, 0);
            int vertOffset = 0;

            // Foundation
            if (foundationHeight > 0)
            {
                stairways.Sort();

                for (int i = 0; i < windowCount; ++i)
                {
                    offset = new Vector2(startingPoint.x + (i * (singlePanelWidth + windowSize.x)), 0);
                    if (stairways.Count == 0 || stairways[0] != i) // stairway?
                    {
                        this.vertices.Add(new Vector3(offset.x, offset.y, startingPoint.z));
                        this.vertices.Add(new Vector3(offset.x, offset.y + foundationHeight, startingPoint.z));
                        this.vertices.Add(new Vector3(offset.x + singlePanelWidth + windowSize.x, offset.y + foundationHeight, startingPoint.z));
                        this.vertices.Add(new Vector3(offset.x + singlePanelWidth + windowSize.x, offset.y, startingPoint.z));

                        this.uvs.Add(new Vector2(bigPanelTextureUV.x, bigPanelTextureUV.y + bigPanelTextureUV.w));
                        this.uvs.Add(new Vector2(bigPanelTextureUV.x, bigPanelTextureUV.y));
                        this.uvs.Add(new Vector2(bigPanelTextureUV.x + bigPanelTextureUV.z, bigPanelTextureUV.y));
                        this.uvs.Add(new Vector2(bigPanelTextureUV.x + bigPanelTextureUV.z, bigPanelTextureUV.y + bigPanelTextureUV.w));

                        this.tris.Add(vertOffset);
                        this.tris.Add(vertOffset + 1);
                        this.tris.Add(vertOffset + 2);

                        this.tris.Add(vertOffset + 2);
                        this.tris.Add(vertOffset + 3);
                        this.tris.Add(vertOffset);
                        vertOffset += 4;
                    }
                    else
                    {
                        stairways.RemoveAt(0);
                        stairways.Add(i);

                        this.vertices.Add(new Vector3(offset.x, offset.y, startingPoint.z));
                        this.vertices.Add(new Vector3(offset.x, offset.y + foundationHeight, startingPoint.z));
                        this.vertices.Add(new Vector3(offset.x + singlePanelWidth, offset.y + foundationHeight, startingPoint.z));
                        this.vertices.Add(new Vector3(offset.x + singlePanelWidth, offset.y, startingPoint.z));

                        this.uvs.Add(new Vector2(bigPanelTextureUV.x, bigPanelTextureUV.y + bigPanelTextureUV.w));
                        this.uvs.Add(new Vector2(bigPanelTextureUV.x, bigPanelTextureUV.y));
                        this.uvs.Add(new Vector2(bigPanelTextureUV.x + bigPanelTextureUV.z, bigPanelTextureUV.y));
                        this.uvs.Add(new Vector2(bigPanelTextureUV.x + bigPanelTextureUV.z, bigPanelTextureUV.y + bigPanelTextureUV.w));

                        this.tris.Add(vertOffset);
                        this.tris.Add(vertOffset + 1);
                        this.tris.Add(vertOffset + 2);

                        this.tris.Add(vertOffset + 2);
                        this.tris.Add(vertOffset + 3);
                        this.tris.Add(vertOffset);
                        vertOffset += 4;
                    }

                }

                offset = new Vector2(startingPoint.x + (windowCount * (singlePanelWidth + windowSize.x)), 0);
                this.vertices.Add(new Vector3(offset.x, offset.y, startingPoint.z));
                this.vertices.Add(new Vector3(offset.x, offset.y + foundationHeight, startingPoint.z));
                this.vertices.Add(new Vector3(offset.x + singlePanelWidth, offset.y + foundationHeight, startingPoint.z));
                this.vertices.Add(new Vector3(offset.x + singlePanelWidth, offset.y, startingPoint.z));

                this.uvs.Add(new Vector2(bigPanelTextureUV.x, bigPanelTextureUV.y + bigPanelTextureUV.w));
                this.uvs.Add(new Vector2(bigPanelTextureUV.x, bigPanelTextureUV.y));
                this.uvs.Add(new Vector2(bigPanelTextureUV.x + bigPanelTextureUV.z, bigPanelTextureUV.y));
                this.uvs.Add(new Vector2(bigPanelTextureUV.x + bigPanelTextureUV.z, bigPanelTextureUV.y + bigPanelTextureUV.w));

                this.tris.Add(vertOffset);
                this.tris.Add(vertOffset + 1);
                this.tris.Add(vertOffset + 2);

                this.tris.Add(vertOffset + 2);
                this.tris.Add(vertOffset + 3);
                this.tris.Add(vertOffset);
                vertOffset += 4;
            }
            
            for (int floor = 0; floor < floorCount; ++floor)
            {
                stairways.Sort();

                for (int i = 0; i < windowCount; ++i)
                {
                    offset = new Vector2(startingPoint.x + (i * (singlePanelWidth + windowSize.x)), floor * floorHeight + foundationHeight);
                    //Draw big panel first
                    this.vertices.Add(new Vector3(offset.x, offset.y, startingPoint.z));
                    this.vertices.Add(new Vector3(offset.x, offset.y + floorHeight, startingPoint.z));
                    this.vertices.Add(new Vector3(offset.x + singlePanelWidth, offset.y + floorHeight, startingPoint.z));
                    this.vertices.Add(new Vector3(offset.x + singlePanelWidth, offset.y, startingPoint.z));

                    this.uvs.Add(new Vector2(bigPanelTextureUV.x, bigPanelTextureUV.y+bigPanelTextureUV.w));
                    this.uvs.Add(new Vector2(bigPanelTextureUV.x, bigPanelTextureUV.y));
                    this.uvs.Add(new Vector2(bigPanelTextureUV.x+bigPanelTextureUV.z, bigPanelTextureUV.y));
                    this.uvs.Add(new Vector2(bigPanelTextureUV.x + bigPanelTextureUV.z, bigPanelTextureUV.y + bigPanelTextureUV.w));

                    this.tris.Add(vertOffset);
                    this.tris.Add(vertOffset + 1);
                    this.tris.Add(vertOffset + 2);

                    this.tris.Add(vertOffset + 2);
                    this.tris.Add(vertOffset + 3);
                    this.tris.Add(vertOffset);
                    vertOffset += 4;

                    if (stairways.Count==0 || stairways[0] != i) // stairway?
                    {
                        //Draw two small panels
                        this.vertices.Add(new Vector3(offset.x + singlePanelWidth, offset.y, startingPoint.z));
                        this.vertices.Add(new Vector3(offset.x + singlePanelWidth, offset.y + smallPanelHeight, startingPoint.z));
                        this.vertices.Add(new Vector3(offset.x + singlePanelWidth + windowSize.x, offset.y + smallPanelHeight, startingPoint.z));
                        this.vertices.Add(new Vector3(offset.x + singlePanelWidth + windowSize.x, offset.y, startingPoint.z));

                        this.uvs.Add(new Vector2(smallPanelTextureUV.x, smallPanelTextureUV.y + smallPanelTextureUV.w));
                        this.uvs.Add(new Vector2(smallPanelTextureUV.x, smallPanelTextureUV.y));
                        this.uvs.Add(new Vector2(smallPanelTextureUV.x + smallPanelTextureUV.z, smallPanelTextureUV.y));
                        this.uvs.Add(new Vector2(smallPanelTextureUV.x + smallPanelTextureUV.z, smallPanelTextureUV.y + smallPanelTextureUV.w));

                        this.tris.Add(vertOffset);
                        this.tris.Add(vertOffset + 1);
                        this.tris.Add(vertOffset + 2);

                        this.tris.Add(vertOffset + 2);
                        this.tris.Add(vertOffset + 3);
                        this.tris.Add(vertOffset);
                        vertOffset += 4;

                        this.vertices.Add(new Vector3(offset.x + singlePanelWidth, offset.y + floorHeight - smallPanelHeight, startingPoint.z));
                        this.vertices.Add(new Vector3(offset.x + singlePanelWidth, offset.y + floorHeight, startingPoint.z));
                        this.vertices.Add(new Vector3(offset.x + singlePanelWidth + windowSize.x, offset.y + floorHeight, startingPoint.z));
                        this.vertices.Add(new Vector3(offset.x + singlePanelWidth + windowSize.x, offset.y + floorHeight - smallPanelHeight, startingPoint.z));

                        this.uvs.Add(new Vector2(smallPanelTextureUV.x, smallPanelTextureUV.y + smallPanelTextureUV.w));
                        this.uvs.Add(new Vector2(smallPanelTextureUV.x, smallPanelTextureUV.y));
                        this.uvs.Add(new Vector2(smallPanelTextureUV.x + smallPanelTextureUV.z, smallPanelTextureUV.y));
                        this.uvs.Add(new Vector2(smallPanelTextureUV.x + smallPanelTextureUV.z, smallPanelTextureUV.y + smallPanelTextureUV.w));

                        this.tris.Add(vertOffset);
                        this.tris.Add(vertOffset + 1);
                        this.tris.Add(vertOffset + 2);

                        this.tris.Add(vertOffset + 2);
                        this.tris.Add(vertOffset + 3);
                        this.tris.Add(vertOffset);
                        vertOffset += 4;

                        //Create window and frame

                        Rect singleWindow = new Rect(new Vector3(offset.x + singlePanelWidth, offset.y + smallPanelHeight, startingPoint.z),
                                                     new Vector3(offset.x + singlePanelWidth, offset.y + floorHeight - smallPanelHeight, startingPoint.z),
                                                     new Vector3(offset.x + singlePanelWidth + windowSize.x, offset.y + floorHeight - smallPanelHeight, startingPoint.z),
                                                     new Vector3(offset.x + singlePanelWidth + windowSize.x, offset.y + smallPanelHeight, startingPoint.z));

                        windows.AddWindow(singleWindow);
                        frames.AddFrame(singleWindow);
                    }
                    else
                    {                    
                        stairways.RemoveAt(0);
                        stairways.Add(i);

                        if (floor == 0) //First floor
                        {
                            this.vertices.Add(new Vector3(offset.x + singlePanelWidth, offset.y + floorHeight - smallPanelHeight - foundationHeight, startingPoint.z));
                            this.vertices.Add(new Vector3(offset.x + singlePanelWidth, offset.y + floorHeight, startingPoint.z));
                            this.vertices.Add(new Vector3(offset.x + singlePanelWidth + windowSize.x, offset.y + floorHeight, startingPoint.z));
                            this.vertices.Add(new Vector3(offset.x + singlePanelWidth + windowSize.x, offset.y + floorHeight - smallPanelHeight - foundationHeight, startingPoint.z));

                            this.uvs.Add(new Vector2(smallPanelTextureUV.x, smallPanelTextureUV.y + smallPanelTextureUV.w));
                            this.uvs.Add(new Vector2(smallPanelTextureUV.x, smallPanelTextureUV.y));
                            this.uvs.Add(new Vector2(smallPanelTextureUV.x + smallPanelTextureUV.z, smallPanelTextureUV.y));
                            this.uvs.Add(new Vector2(smallPanelTextureUV.x + smallPanelTextureUV.z, smallPanelTextureUV.y + smallPanelTextureUV.w));

                            this.tris.Add(vertOffset);
                            this.tris.Add(vertOffset + 1);
                            this.tris.Add(vertOffset + 2);

                            this.tris.Add(vertOffset + 2);
                            this.tris.Add(vertOffset + 3);
                            this.tris.Add(vertOffset);
                            vertOffset += 4;

                            Rect singleDoor = new Rect(new Vector3(offset.x + singlePanelWidth, offset.y - foundationHeight, startingPoint.z),
                                                       new Vector3(offset.x + singlePanelWidth, offset.y + floorHeight - smallPanelHeight - foundationHeight, startingPoint.z),
                                                       new Vector3(offset.x + singlePanelWidth + windowSize.x, offset.y + floorHeight - smallPanelHeight - foundationHeight, startingPoint.z),
                                                       new Vector3(offset.x + singlePanelWidth + windowSize.x, offset.y - foundationHeight, startingPoint.z));
                            doors.AddDoor(singleDoor);
                            frames.AddFrame(singleDoor);
                        }
                        else {
                            Vector2 biasedWindow = windowSize * 0.75f;

                            this.vertices.Add(new Vector3(offset.x + singlePanelWidth, offset.y + biasedWindow.y , startingPoint.z));
                            this.vertices.Add(new Vector3(offset.x + singlePanelWidth, offset.y + floorHeight, startingPoint.z));
                            this.vertices.Add(new Vector3(offset.x + singlePanelWidth + windowSize.x, offset.y + floorHeight, startingPoint.z));
                            this.vertices.Add(new Vector3(offset.x + singlePanelWidth + windowSize.x, offset.y + biasedWindow.y, startingPoint.z));

                            this.uvs.Add(new Vector2(smallPanelTextureUV.x, smallPanelTextureUV.y + smallPanelTextureUV.w));
                            this.uvs.Add(new Vector2(smallPanelTextureUV.x, smallPanelTextureUV.y));
                            this.uvs.Add(new Vector2(smallPanelTextureUV.x + smallPanelTextureUV.z, smallPanelTextureUV.y));
                            this.uvs.Add(new Vector2(smallPanelTextureUV.x + smallPanelTextureUV.z, smallPanelTextureUV.y + smallPanelTextureUV.w));

                            this.tris.Add(vertOffset);
                            this.tris.Add(vertOffset + 1);
                            this.tris.Add(vertOffset + 2);

                            this.tris.Add(vertOffset + 2);
                            this.tris.Add(vertOffset + 3);
                            this.tris.Add(vertOffset);
                            vertOffset += 4;

                            Rect singleWindow = new Rect(new Vector3(offset.x + singlePanelWidth, offset.y, startingPoint.z),
                                                         new Vector3(offset.x + singlePanelWidth, offset.y + biasedWindow.y, startingPoint.z),
                                                         new Vector3(offset.x + singlePanelWidth + windowSize.x, offset.y + biasedWindow.y, startingPoint.z),
                                                         new Vector3(offset.x + singlePanelWidth + windowSize.x, offset.y, startingPoint.z));

                            windows.AddWindow(singleWindow);
                            frames.AddFrame(singleWindow);
                        }
                    }

                }
                //Draw last panel        
                offset = new Vector2(startingPoint.x + (windowCount * (singlePanelWidth + windowSize.x)), floor * floorHeight + foundationHeight);
                this.vertices.Add(new Vector3(offset.x, offset.y, startingPoint.z));
                this.vertices.Add(new Vector3(offset.x, offset.y + floorHeight, startingPoint.z));
                this.vertices.Add(new Vector3(offset.x + singlePanelWidth, offset.y + floorHeight, startingPoint.z));
                this.vertices.Add(new Vector3(offset.x + singlePanelWidth, offset.y, startingPoint.z));

                this.uvs.Add(new Vector2(bigPanelTextureUV.x, bigPanelTextureUV.y + bigPanelTextureUV.w));
                this.uvs.Add(new Vector2(bigPanelTextureUV.x, bigPanelTextureUV.y));
                this.uvs.Add(new Vector2(bigPanelTextureUV.x + bigPanelTextureUV.z, bigPanelTextureUV.y));
                this.uvs.Add(new Vector2(bigPanelTextureUV.x + bigPanelTextureUV.z, bigPanelTextureUV.y + bigPanelTextureUV.w));

                this.tris.Add(vertOffset);
                this.tris.Add(vertOffset + 1);
                this.tris.Add(vertOffset + 2);

                this.tris.Add(vertOffset + 2);
                this.tris.Add(vertOffset + 3);
                this.tris.Add(vertOffset);
                vertOffset += 4;
            }

            this.material = new Material(Shader.Find("Diffuse"));
            this.material.color = Color.grey;

            doors.Draw();
            windows.Draw();
            frames.Draw();
            base.Draw();
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