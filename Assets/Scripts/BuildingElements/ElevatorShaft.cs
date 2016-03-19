using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Building
{

    public class ElevatorShaft : Drawable
    {
        public float windowFrameSize;
        public Vector2 windowSize;
        public float doorSize;
        private Door doors;
        private WindowFrame frames;

        public void Init()
        {
            this.vertices = new List<Vector3>();
            this.tris = new List<int>();
            this.uvs = new List<Vector2>();

            // Frames
            GameObject frame = new GameObject();
            frame.name = this.name + " Frames";
            frame.transform.parent = this.transform;
            frame.transform.localPosition = Vector3.zero;
            frames = frame.AddComponent<WindowFrame>();
            frames.windowFrameSize = this.windowFrameSize;
            frames.material = this.material;
            frames.Init();

            // Doors
            GameObject door = new GameObject();
            door.name = this.name + " Doors";
            door.transform.parent = this.transform;
            door.transform.localPosition = Vector3.zero;
            doors = door.AddComponent<Door>();
            doors.frameSize = this.windowFrameSize;
            doors.material = this.material;
            doors.Init();
        }

        public void AddShaft(Rect points, float floorHeight)
        {
            int vertOffset = this.vertices.Count;
            //North face
            this.vertices.Add(points.C);
            this.vertices.Add(points.C + new Vector3(0f, floorHeight, 0f));
            this.vertices.Add(points.D + new Vector3(0f, floorHeight, 0f));
            this.vertices.Add(points.D);

            this.AddUVs(new Vector2(0f, 0.75f));

            this.tris.Add(vertOffset);
            this.tris.Add(vertOffset + 1);
            this.tris.Add(vertOffset + 2);

            this.tris.Add(vertOffset + 2);
            this.tris.Add(vertOffset + 3);
            this.tris.Add(vertOffset);
            vertOffset += 4;

            //East face
            this.vertices.Add(points.B);
            this.vertices.Add(points.B + new Vector3(0f, floorHeight, 0f));
            this.vertices.Add(points.C + new Vector3(0f, floorHeight, 0f));
            this.vertices.Add(points.C);

            this.AddUVs(new Vector2(0f, 0.75f));

            this.tris.Add(vertOffset);
            this.tris.Add(vertOffset + 1);
            this.tris.Add(vertOffset + 2);

            this.tris.Add(vertOffset + 2);
            this.tris.Add(vertOffset + 3);
            this.tris.Add(vertOffset);
            vertOffset += 4;

            //South face
            this.vertices.Add(points.A);
            this.vertices.Add(points.A + new Vector3(0f, floorHeight, 0f));
            this.vertices.Add(points.B + new Vector3(0f, floorHeight, 0f));
            this.vertices.Add(points.B);

            this.AddUVs(new Vector2(0f, 0.75f));

            this.tris.Add(vertOffset);
            this.tris.Add(vertOffset + 1);
            this.tris.Add(vertOffset + 2);

            this.tris.Add(vertOffset + 2);
            this.tris.Add(vertOffset + 3);
            this.tris.Add(vertOffset);
            vertOffset += 4;

            //West face
            float shaftWidth = Vector3.Distance(points.A, points.D);

            this.vertices.Add(points.A);
            this.vertices.Add(new Vector3(points.A.x, points.A.y + floorHeight, points.A.z));
            this.vertices.Add(new Vector3(points.A.x, points.A.y + floorHeight, points.A.z + (shaftWidth / 2) - windowSize.x));
            this.vertices.Add(new Vector3(points.A.x, points.A.y, points.A.z + (shaftWidth / 2) - windowSize.x));

            this.AddUVs(new Vector2(0f, 0.75f));

            this.tris.Add(vertOffset + 2);
            this.tris.Add(vertOffset + 1);
            this.tris.Add(vertOffset);

            this.tris.Add(vertOffset);
            this.tris.Add(vertOffset + 3);
            this.tris.Add(vertOffset + 2);
            vertOffset += 4;

            this.vertices.Add(new Vector3(points.A.x, points.A.y + doorSize, points.A.z + (shaftWidth / 2) - windowSize.x));
            this.vertices.Add(new Vector3(points.A.x, points.A.y + floorHeight, points.A.z + (shaftWidth / 2) - windowSize.x));
            this.vertices.Add(new Vector3(points.A.x, points.A.y + floorHeight, points.A.z + (shaftWidth / 2)));
            this.vertices.Add(new Vector3(points.A.x, points.A.y + doorSize, points.A.z + (shaftWidth / 2)));

            this.AddUVs(new Vector2(0f, 0.75f));

            this.tris.Add(vertOffset + 2);
            this.tris.Add(vertOffset + 1);
            this.tris.Add(vertOffset);

            this.tris.Add(vertOffset);
            this.tris.Add(vertOffset + 3);
            this.tris.Add(vertOffset + 2);
            vertOffset += 4;

            this.vertices.Add(new Vector3(points.A.x, points.A.y, points.A.z + (shaftWidth / 2)));
            this.vertices.Add(new Vector3(points.A.x, points.A.y + floorHeight, points.A.z + (shaftWidth / 2)));
            this.vertices.Add(new Vector3(points.A.x, points.A.y + floorHeight, points.D.z));
            this.vertices.Add(new Vector3(points.A.x, points.A.y, points.D.z));

            this.AddUVs(new Vector2(0f, 0.75f));

            this.tris.Add(vertOffset + 2);
            this.tris.Add(vertOffset + 1);
            this.tris.Add(vertOffset);

            this.tris.Add(vertOffset);
            this.tris.Add(vertOffset + 3);
            this.tris.Add(vertOffset + 2);
            vertOffset += 4;

            Rect singleDoor = new Rect(new Vector3(points.A.x, points.A.y, points.A.z + (shaftWidth / 2)),
                                       new Vector3(points.A.x, points.A.y + doorSize, points.A.z + (shaftWidth / 2)),
                                       new Vector3(points.A.x, points.A.y + doorSize, points.A.z + (shaftWidth / 2) - windowSize.x),
                                       new Vector3(points.A.x, points.A.y, points.A.z + (shaftWidth / 2) - windowSize.x));

            doors.AddDoor(singleDoor, 2);
            frames.AddFrame(singleDoor);


            //Roof
            this.vertices.Add(points.A + new Vector3(0f,floorHeight,0f));
            this.vertices.Add(points.B + new Vector3(0f, floorHeight, 0f));
            this.vertices.Add(points.C + new Vector3(0f, floorHeight, 0f));
            this.vertices.Add(points.D + new Vector3(0f, floorHeight, 0f));

            this.AddUVs(new Vector2(0f, 0.75f));

            this.tris.Add(vertOffset + 2);
            this.tris.Add(vertOffset + 1);
            this.tris.Add(vertOffset);

            this.tris.Add(vertOffset);
            this.tris.Add(vertOffset + 3);
            this.tris.Add(vertOffset + 2);
            vertOffset += 4;

        }

        public void Draw()
        {
            doors.Draw();
            frames.Draw();
            base.Draw();
        }
        
    }
}
