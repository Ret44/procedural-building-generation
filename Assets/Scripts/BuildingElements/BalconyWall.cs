using UnityEngine;
using Building;
using System.Collections;
using System.Collections.Generic;


namespace Building
{
    public class BalconyWall : Drawable
    {

        public Vector2 wallSize;
        public Vector2 windowSize;
        public float windowOffset;
        public float windowFrameSize;
        public float foundationHeight;
        public float balconyLength;
        public Vector3 startingPoint;
        public Vector3 directionPoint;

        public void Draw()
        {
            this.vertices = new List<Vector3>();
            this.tris = new List<int>();

            this.uvs = new List<Vector2>();

            int floorCount = Mathf.FloorToInt(wallSize.y / (windowSize.y * 2));
            float floorHeight = (wallSize.y / floorCount);// + (wallSize.y % floorCount);

            int windowCount = Mathf.FloorToInt(wallSize.x / (windowSize.x + windowOffset));

            //List<int> stairways = CalculateStairways(windowCount);

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
            windows.material = this.material;
            windows.Init();

            // Frames
            GameObject frame = new GameObject();
            frame.name = this.name + " Frames";
            frame.transform.parent = this.transform;
            frame.transform.localPosition = Vector3.zero;
            WindowFrame frames = frame.AddComponent<WindowFrame>();
            frames.windowFrameSize = this.windowFrameSize;
            frames.material = this.material;
            frames.Init();

            // Doors
            GameObject door = new GameObject();
            door.name = this.name + " Doors";
            door.transform.parent = this.transform;
            door.transform.localPosition = Vector3.zero;
            Door doors = door.AddComponent<Door>();
            doors.frameSize = this.windowFrameSize;
            doors.material = this.material;
            doors.Init();

            // Balconies
            GameObject balcony = new GameObject();
            balcony.name = this.name + " Balconies";
            balcony.transform.parent = this.transform;
            balcony.transform.localPosition = Vector3.zero;
            Balcony balconies = balcony.AddComponent<Balcony>();
            balconies.balconyLength = this.balconyLength;
            balconies.material = this.material;
            balconies.Init();

            Vector2 offset = new Vector2(0, 0);
            int vertOffset = 0;

            int windowLeft;

            // Foundation
            if (foundationHeight > 0)
            {
                for (int i = 0; i < windowCount; ++i)
                {
                    offset = new Vector2(startingPoint.x + (i * (singlePanelWidth + windowSize.x)), 0);
                    this.vertices.Add(new Vector3(offset.x, offset.y, startingPoint.z));
                    this.vertices.Add(new Vector3(offset.x, offset.y + foundationHeight, startingPoint.z));
                    this.vertices.Add(new Vector3(offset.x + singlePanelWidth + windowSize.x, offset.y + foundationHeight, startingPoint.z));
                    this.vertices.Add(new Vector3(offset.x + singlePanelWidth + windowSize.x, offset.y, startingPoint.z));

                    this.uvs.Add(new Vector2(0f,0.75f +(floorHeight-foundationHeight)*0.25f/floorHeight));
                    this.uvs.Add(new Vector2(0f, 1f));
                    this.uvs.Add(new Vector2(0.25f, 1f));
                    this.uvs.Add(new Vector2(0.25f, 0.75f + (floorHeight - foundationHeight) * 0.25f / floorHeight));

                    this.tris.Add(vertOffset);
                    this.tris.Add(vertOffset + 1);
                    this.tris.Add(vertOffset + 2);

                    this.tris.Add(vertOffset + 2);
                    this.tris.Add(vertOffset + 3);
                    this.tris.Add(vertOffset);
                    vertOffset += 4;
                }

                offset = new Vector2(startingPoint.x + (windowCount * (singlePanelWidth + windowSize.x)), 0);
                this.vertices.Add(new Vector3(offset.x, offset.y, startingPoint.z));
                this.vertices.Add(new Vector3(offset.x, offset.y + foundationHeight, startingPoint.z));
                this.vertices.Add(new Vector3(offset.x + singlePanelWidth, offset.y + foundationHeight, startingPoint.z));
                this.vertices.Add(new Vector3(offset.x + singlePanelWidth, offset.y, startingPoint.z));

                this.uvs.Add(new Vector2(0f, 0.75f + (floorHeight - foundationHeight) * 0.25f / floorHeight));
                this.uvs.Add(new Vector2(0f, 1f));
                this.uvs.Add(new Vector2((singlePanelWidth * 0.25f / (singlePanelWidth + windowSize.x)), 1f));
                this.uvs.Add(new Vector2((singlePanelWidth * 0.25f / (singlePanelWidth + windowSize.x)), 0.75f + (floorHeight - foundationHeight) * 0.25f / floorHeight));

                this.tris.Add(vertOffset);
                this.tris.Add(vertOffset + 1);
                this.tris.Add(vertOffset + 2);

                this.tris.Add(vertOffset + 2);
                this.tris.Add(vertOffset + 3);
                this.tris.Add(vertOffset);
                vertOffset += 4;
            }
            int iter;

            for(int floor = 0; floor<floorCount; ++floor)
            {
                windowLeft = windowCount;
                iter = 0;
                while(windowLeft > 0)
                {
                    offset = new Vector2(startingPoint.x + (iter * (singlePanelWidth + windowSize.x)), floor * floorHeight + foundationHeight);

                    if (windowLeft >= 2)
                    {
                        //Draw big panel first
                        this.vertices.Add(new Vector3(offset.x, offset.y, startingPoint.z));
                        this.vertices.Add(new Vector3(offset.x, offset.y + floorHeight, startingPoint.z));
                        this.vertices.Add(new Vector3(offset.x + singlePanelWidth + (windowSize.x / 2), offset.y + floorHeight, startingPoint.z));
                        this.vertices.Add(new Vector3(offset.x + singlePanelWidth + (windowSize.x / 2), offset.y, startingPoint.z));

                        this.uvs.Add(new Vector2(0f, 0.75f));
                        this.uvs.Add(new Vector2(0f, 1f));
                        this.uvs.Add(new Vector2(((singlePanelWidth + (windowSize.x / 2) )* 0.25f / (singlePanelWidth + windowSize.x)), 1f));
                        this.uvs.Add(new Vector2(((singlePanelWidth + (windowSize.x / 2)) * 0.25f / (singlePanelWidth + windowSize.x)), 0.75f));

                        this.tris.Add(vertOffset);
                        this.tris.Add(vertOffset + 1);
                        this.tris.Add(vertOffset + 2);

                        this.tris.Add(vertOffset + 2);
                        this.tris.Add(vertOffset + 3);
                        this.tris.Add(vertOffset);
                        vertOffset += 4;

                        ////Narrow panel
                        //this.vertices.Add(new Vector3(offset.x + singlePanelWidth,offset.y, startingPoint.z));
                        //this.vertices.Add(new Vector3(offset.x + singlePanelWidth, offset.y + windowSize.y + smallPanelHeight, startingPoint.z));
                        //this.vertices.Add(new Vector3(offset.x + singlePanelWidth + (windowSize.x/2), offset.y + windowSize.y + smallPanelHeight, startingPoint.z));
                        //this.vertices.Add(new Vector3(offset.x + singlePanelWidth + (windowSize.x / 2), offset.y, startingPoint.z));
                       
                        ////TODO: CHANGE
                        //this.AddUVs(new Vector2(0.25f, 0.75f));

                        //this.tris.Add(vertOffset);
                        //this.tris.Add(vertOffset + 1);
                        //this.tris.Add(vertOffset + 2);

                        //this.tris.Add(vertOffset + 2);
                        //this.tris.Add(vertOffset + 3);
                        //this.tris.Add(vertOffset);
                        //vertOffset += 4;

                        //Filler panel
                        this.vertices.Add(new Vector3(offset.x + singlePanelWidth + (windowSize.x / 2), offset.y + windowSize.y + smallPanelHeight, startingPoint.z));
                        this.vertices.Add(new Vector3(offset.x + singlePanelWidth + (windowSize.x / 2), offset.y + floorHeight, startingPoint.z));
                        this.vertices.Add(new Vector3(offset.x + singlePanelWidth + windowSize.x , offset.y + floorHeight, startingPoint.z));
                        this.vertices.Add(new Vector3(offset.x + singlePanelWidth + windowSize.x , offset.y + windowSize.y + smallPanelHeight, startingPoint.z));

                        this.uvs.Add(new Vector2(((singlePanelWidth + (windowSize.x / 2)) * 0.25f / (singlePanelWidth + windowSize.x)), 0.75f + (windowSize.y + smallPanelHeight) * 0.25f / floorHeight));
                        this.uvs.Add(new Vector2(((singlePanelWidth + (windowSize.x / 2)) * 0.25f / (singlePanelWidth + windowSize.x)), 1f));
                        this.uvs.Add(new Vector2(0.25f,1f));
                        this.uvs.Add(new Vector2(0.25f, 0.75f + (windowSize.y + smallPanelHeight) * 0.25f / floorHeight));

                        this.tris.Add(vertOffset);
                        this.tris.Add(vertOffset + 1);
                        this.tris.Add(vertOffset + 2);

                        this.tris.Add(vertOffset + 2);
                        this.tris.Add(vertOffset + 3);
                        this.tris.Add(vertOffset);
                        vertOffset += 4;

                        //Bigger wide panel
                        this.vertices.Add(new Vector3(offset.x + singlePanelWidth + windowSize.x , offset.y + windowSize.y + smallPanelHeight, startingPoint.z));
                        this.vertices.Add(new Vector3(offset.x + singlePanelWidth + windowSize.x, offset.y + floorHeight, startingPoint.z));
                        this.vertices.Add(new Vector3(offset.x + singlePanelWidth + (windowSize.x / 2) + (singlePanelWidth - (windowSize.x / 2)) + (2 * windowSize.x), offset.y + floorHeight, startingPoint.z));
                        this.vertices.Add(new Vector3(offset.x + singlePanelWidth + (windowSize.x / 2) + (singlePanelWidth - (windowSize.x / 2)) + (2 * windowSize.x), offset.y + windowSize.y + smallPanelHeight, startingPoint.z));

                        this.uvs.Add(new Vector2(0f, 0.75f + (windowSize.y + smallPanelHeight) * 0.25f / floorHeight));
                        this.uvs.Add(new Vector2(0f, 1f));
                        this.uvs.Add(new Vector2(0.25f, 1f));
                        this.uvs.Add(new Vector2(0.25f, 0.75f + (windowSize.y + smallPanelHeight) * 0.25f / floorHeight));

                        this.tris.Add(vertOffset);
                        this.tris.Add(vertOffset + 1);
                        this.tris.Add(vertOffset + 2);

                        this.tris.Add(vertOffset + 2);
                        this.tris.Add(vertOffset + 3);
                        this.tris.Add(vertOffset);
                        vertOffset += 4;


                        //Smaller wide panel
                        this.vertices.Add(new Vector3(offset.x + singlePanelWidth + (windowSize.x * 1.5f), offset.y, startingPoint.z));
                        this.vertices.Add(new Vector3(offset.x + singlePanelWidth + (windowSize.x * 1.5f), offset.y + smallPanelHeight, startingPoint.z));
                        this.vertices.Add(new Vector3(offset.x + singlePanelWidth + (windowSize.x / 2) + (singlePanelWidth - (windowSize.x / 2)) + (2 * windowSize.x), offset.y + smallPanelHeight, startingPoint.z));
                        this.vertices.Add(new Vector3(offset.x + singlePanelWidth + (windowSize.x / 2) + (singlePanelWidth - (windowSize.x / 2)) + (2 * windowSize.x), offset.y, startingPoint.z));

                        this.uvs.Add(new Vector2((((windowSize.x / 2)) * 0.25f / (singlePanelWidth + windowSize.x)), 0.75f));
                        this.uvs.Add(new Vector2((((windowSize.x / 2)) * 0.25f / (singlePanelWidth + windowSize.x)), 0.75f + smallPanelHeight * 0.25f / floorHeight));
                        this.uvs.Add(new Vector2(0.25f, 0.75f + smallPanelHeight * 0.25f / floorHeight));
                        this.uvs.Add(new Vector2(0.25f, 0.75f));

                        this.tris.Add(vertOffset);
                        this.tris.Add(vertOffset + 1);
                        this.tris.Add(vertOffset + 2);

                        this.tris.Add(vertOffset + 2);
                        this.tris.Add(vertOffset + 3);
                        this.tris.Add(vertOffset);
                        vertOffset += 4;

                        Rect singleDoor = new Rect( new Vector3(offset.x + singlePanelWidth + (windowSize.x / 2), offset.y, startingPoint.z),
                                                    new Vector3(offset.x + singlePanelWidth + (windowSize.x / 2), offset.y + smallPanelHeight + windowSize.y, startingPoint.z),
                                                    new Vector3(offset.x + singlePanelWidth + (windowSize.x * 1.5f), offset.y + smallPanelHeight + windowSize.y, startingPoint.z),
                                                    new Vector3(offset.x + singlePanelWidth + (windowSize.x * 1.5f), offset.y, startingPoint.z));
                        
                        Rect singleWindow = new Rect(new Vector3(offset.x + singlePanelWidth + (windowSize.x * 1.5f), offset.y + smallPanelHeight, startingPoint.z),
                                                    new Vector3(offset.x + singlePanelWidth + (windowSize.x * 1.5f), offset.y + floorHeight - smallPanelHeight, startingPoint.z),
                                                    new Vector3(offset.x + singlePanelWidth + (windowSize.x / 2) + (singlePanelWidth - (windowSize.x / 2)) + (2 * windowSize.x), offset.y + floorHeight - smallPanelHeight, startingPoint.z),
                                                    new Vector3(offset.x + singlePanelWidth + (windowSize.x / 2) + (singlePanelWidth - (windowSize.x / 2)) + (2 * windowSize.x), offset.y + smallPanelHeight, startingPoint.z));

                        Rect singleBalcony = new Rect(new Vector3(offset.x + singlePanelWidth + (windowSize.x / 2), offset.y, startingPoint.z),
                                                      new Vector3(offset.x + singlePanelWidth + (windowSize.x / 2), offset.y + smallPanelHeight, startingPoint.z),
                                                      new Vector3(offset.x + singlePanelWidth + (windowSize.x / 2) + (singlePanelWidth - (windowSize.x / 2)) + (2 * windowSize.x), offset.y + smallPanelHeight, startingPoint.z),
                                                      new Vector3(offset.x + singlePanelWidth + (windowSize.x / 2) + (singlePanelWidth - (windowSize.x / 2)) + (2 * windowSize.x), offset.y, startingPoint.z));

                        balconies.AddBalcony(singleBalcony);
                        windows.AddWindow(singleWindow);
                        doors.AddDoor(singleDoor,1);
                        frames.AddFrame(singleWindow);
                        frames.AddFrame(singleDoor);

                        iter += 2;
                        windowLeft -= 2;
                        if (windowLeft != 0)
                        {
                            offset = new Vector2(startingPoint.x + (iter * (singlePanelWidth + windowSize.x)), floor * floorHeight + foundationHeight);
                            //Draw big panel first
                            this.vertices.Add(new Vector3(offset.x, offset.y, startingPoint.z));
                            this.vertices.Add(new Vector3(offset.x, offset.y + floorHeight, startingPoint.z));
                            this.vertices.Add(new Vector3(offset.x + singlePanelWidth, offset.y + floorHeight, startingPoint.z));
                            this.vertices.Add(new Vector3(offset.x + singlePanelWidth, offset.y, startingPoint.z));

                            this.uvs.Add(new Vector2(0f, 0.75f));
                            this.uvs.Add(new Vector2(0f, 1f));
                            this.uvs.Add(new Vector2((singlePanelWidth * 0.25f / (singlePanelWidth + windowSize.x)), 1f));
                            this.uvs.Add(new Vector2((singlePanelWidth * 0.25f / (singlePanelWidth + windowSize.x)), 0.75f));

                            this.tris.Add(vertOffset);
                            this.tris.Add(vertOffset + 1);
                            this.tris.Add(vertOffset + 2);

                            this.tris.Add(vertOffset + 2);
                            this.tris.Add(vertOffset + 3);
                            this.tris.Add(vertOffset);
                            vertOffset += 4;

                            //Draw two small panels
                            this.vertices.Add(new Vector3(offset.x + singlePanelWidth, offset.y, startingPoint.z));
                            this.vertices.Add(new Vector3(offset.x + singlePanelWidth, offset.y + smallPanelHeight, startingPoint.z));
                            this.vertices.Add(new Vector3(offset.x + singlePanelWidth + windowSize.x, offset.y + smallPanelHeight, startingPoint.z));
                            this.vertices.Add(new Vector3(offset.x + singlePanelWidth + windowSize.x, offset.y, startingPoint.z));

                            this.uvs.Add(new Vector2((singlePanelWidth * 0.25f / (singlePanelWidth + windowSize.x)), 0.75f + ((floorHeight - smallPanelHeight) / floorHeight * 0.25f)));
                            this.uvs.Add(new Vector2((singlePanelWidth * 0.25f / (singlePanelWidth + windowSize.x)), 1f));
                            this.uvs.Add(new Vector2(0.25f, 1f));
                            this.uvs.Add(new Vector2(0.25f, 0.75f + ((floorHeight - smallPanelHeight) / floorHeight * 0.25f)));

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

                            this.uvs.Add(new Vector2((singlePanelWidth * 0.25f / (singlePanelWidth + windowSize.x)), 0.75f));
                            this.uvs.Add(new Vector2((singlePanelWidth * 0.25f / (singlePanelWidth + windowSize.x)), 0.75f + (smallPanelHeight / floorHeight * 0.25f)));
                            this.uvs.Add(new Vector2(0.25f, 0.75f + (smallPanelHeight / floorHeight * 0.25f)));
                            this.uvs.Add(new Vector2(0.25f, 0.75f));

                            this.tris.Add(vertOffset);
                            this.tris.Add(vertOffset + 1);
                            this.tris.Add(vertOffset + 2);

                            this.tris.Add(vertOffset + 2);
                            this.tris.Add(vertOffset + 3);
                            this.tris.Add(vertOffset);
                            vertOffset += 4;

                            //Create window and frame

                            singleWindow = new Rect(new Vector3(offset.x + singlePanelWidth, offset.y + smallPanelHeight, startingPoint.z),
                                                    new Vector3(offset.x + singlePanelWidth, offset.y + floorHeight - smallPanelHeight, startingPoint.z),
                                                    new Vector3(offset.x + singlePanelWidth + windowSize.x, offset.y + floorHeight - smallPanelHeight, startingPoint.z),
                                                    new Vector3(offset.x + singlePanelWidth + windowSize.x, offset.y + smallPanelHeight, startingPoint.z));

                            windows.AddWindow(singleWindow);
                            frames.AddFrame(singleWindow);

                            iter++;

                            windowLeft --;
                        }
                    }
                    else
                    {
                        //Draw big panel first
                        this.vertices.Add(new Vector3(offset.x, offset.y, startingPoint.z));
                        this.vertices.Add(new Vector3(offset.x, offset.y + floorHeight, startingPoint.z));
                        this.vertices.Add(new Vector3(offset.x + singlePanelWidth, offset.y + floorHeight, startingPoint.z));
                        this.vertices.Add(new Vector3(offset.x + singlePanelWidth, offset.y, startingPoint.z));

                        this.uvs.Add(new Vector2(0f, 0.75f));
                        this.uvs.Add(new Vector2(0f, 1f));
                        this.uvs.Add(new Vector2((singlePanelWidth * 0.25f / (singlePanelWidth + windowSize.x)), 1f));
                        this.uvs.Add(new Vector2((singlePanelWidth * 0.25f / (singlePanelWidth + windowSize.x)), 0.75f));

                        this.tris.Add(vertOffset);
                        this.tris.Add(vertOffset + 1);
                        this.tris.Add(vertOffset + 2);

                        this.tris.Add(vertOffset + 2);
                        this.tris.Add(vertOffset + 3);
                        this.tris.Add(vertOffset);
                        vertOffset += 4;

                        //Draw two small panels
                        this.vertices.Add(new Vector3(offset.x + singlePanelWidth, offset.y, startingPoint.z));
                        this.vertices.Add(new Vector3(offset.x + singlePanelWidth, offset.y + smallPanelHeight, startingPoint.z));
                        this.vertices.Add(new Vector3(offset.x + singlePanelWidth + windowSize.x, offset.y + smallPanelHeight, startingPoint.z));
                        this.vertices.Add(new Vector3(offset.x + singlePanelWidth + windowSize.x, offset.y, startingPoint.z));

                        this.uvs.Add(new Vector2((singlePanelWidth * 0.25f / (singlePanelWidth + windowSize.x)), 0.75f + ((floorHeight - smallPanelHeight) / floorHeight * 0.25f)));
                        this.uvs.Add(new Vector2((singlePanelWidth * 0.25f / (singlePanelWidth + windowSize.x)), 1f));
                        this.uvs.Add(new Vector2(0.25f, 1f));
                        this.uvs.Add(new Vector2(0.25f, 0.75f + ((floorHeight - smallPanelHeight) / floorHeight * 0.25f)));

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

                        this.uvs.Add(new Vector2((singlePanelWidth * 0.25f / (singlePanelWidth + windowSize.x)), 0.75f));
                        this.uvs.Add(new Vector2((singlePanelWidth * 0.25f / (singlePanelWidth + windowSize.x)), 0.75f + (smallPanelHeight / floorHeight * 0.25f)));
                        this.uvs.Add(new Vector2(0.25f, 0.75f + (smallPanelHeight / floorHeight * 0.25f)));
                        this.uvs.Add(new Vector2(0.25f, 0.75f));

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

                        iter++;

                        windowLeft--;
                    }
                }

                //Draw last panel        
                offset = new Vector2(startingPoint.x + (windowCount * (singlePanelWidth + windowSize.x)), floor * floorHeight + foundationHeight);
                this.vertices.Add(new Vector3(offset.x, offset.y, startingPoint.z));
                this.vertices.Add(new Vector3(offset.x, offset.y + floorHeight, startingPoint.z));
                this.vertices.Add(new Vector3(offset.x + singlePanelWidth, offset.y + floorHeight, startingPoint.z));
                this.vertices.Add(new Vector3(offset.x + singlePanelWidth, offset.y, startingPoint.z));

                this.uvs.Add(new Vector2(0f, 0.75f));
                this.uvs.Add(new Vector2(0f, 1f));
                this.uvs.Add(new Vector2((singlePanelWidth * 0.25f / (singlePanelWidth + windowSize.x)), 1f));
                this.uvs.Add(new Vector2((singlePanelWidth * 0.25f / (singlePanelWidth + windowSize.x)), 0.75f));

                this.tris.Add(vertOffset);
                this.tris.Add(vertOffset + 1);
                this.tris.Add(vertOffset + 2);

                this.tris.Add(vertOffset + 2);
                this.tris.Add(vertOffset + 3);
                this.tris.Add(vertOffset);
                vertOffset += 4;
            }

          //  this.material = new Material(Shader.Find("Diffuse"));
          //  this.material.color = Color.grey;

            balconies.Draw();
            doors.Draw();
            windows.Draw();
            frames.Draw();
            base.Draw();
        }
    }
}
