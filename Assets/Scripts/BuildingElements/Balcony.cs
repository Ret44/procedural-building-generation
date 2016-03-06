using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Building
{
    public class Balcony : Drawable
    {
        public float balconyLength;

        public void Init()
        {
            this.vertices = new List<Vector3>();
            this.tris = new List<int>();
        }

        public void AddBalcony(Rect points)
        {
            int vertOffset = this.vertices.Count;

            //Platform
            this.vertices.Add(new Vector3(points.A.x, points.A.y, points.A.z - balconyLength));
            this.vertices.Add(new Vector3(points.A.x, points.A.y, points.A.z));
            this.vertices.Add(new Vector3(points.D.x, points.D.y, points.D.z));
            this.vertices.Add(new Vector3(points.D.x, points.D.y, points.D.z - balconyLength));

            this.tris.Add(vertOffset);
            this.tris.Add(vertOffset + 1);
            this.tris.Add(vertOffset + 2);

            this.tris.Add(vertOffset + 2);
            this.tris.Add(vertOffset + 3);
            this.tris.Add(vertOffset);

            this.tris.Add(vertOffset + 2);
            this.tris.Add(vertOffset + 1);
            this.tris.Add(vertOffset);

            this.tris.Add(vertOffset);
            this.tris.Add(vertOffset + 3);
            this.tris.Add(vertOffset + 2);

            vertOffset += 4;
            //Front railing

            this.vertices.Add(new Vector3(points.A.x, points.A.y, points.A.z - balconyLength));
            this.vertices.Add(new Vector3(points.B.x, points.B.y, points.B.z - balconyLength));
            this.vertices.Add(new Vector3(points.C.x, points.C.y, points.C.z - balconyLength));
            this.vertices.Add(new Vector3(points.D.x, points.D.y, points.D.z - balconyLength));

            this.tris.Add(vertOffset);
            this.tris.Add(vertOffset + 1);
            this.tris.Add(vertOffset + 2);

            this.tris.Add(vertOffset + 2);
            this.tris.Add(vertOffset + 3);
            this.tris.Add(vertOffset);

            this.tris.Add(vertOffset + 2);
            this.tris.Add(vertOffset + 1);
            this.tris.Add(vertOffset);

            this.tris.Add(vertOffset);
            this.tris.Add(vertOffset + 3);
            this.tris.Add(vertOffset + 2);

            vertOffset += 4;

            //Left railing
            this.vertices.Add(new Vector3(points.A.x, points.A.y, points.A.z - balconyLength));
            this.vertices.Add(new Vector3(points.B.x, points.B.y, points.B.z - balconyLength));
            this.vertices.Add(new Vector3(points.B.x, points.B.y, points.B.z));
            this.vertices.Add(new Vector3(points.A.x, points.A.y, points.A.z));

            this.tris.Add(vertOffset);
            this.tris.Add(vertOffset + 1);
            this.tris.Add(vertOffset + 2);

            this.tris.Add(vertOffset + 2);
            this.tris.Add(vertOffset + 3);
            this.tris.Add(vertOffset);

            this.tris.Add(vertOffset + 2);
            this.tris.Add(vertOffset + 1);
            this.tris.Add(vertOffset);

            this.tris.Add(vertOffset);
            this.tris.Add(vertOffset + 3);
            this.tris.Add(vertOffset + 2);

            vertOffset += 4;

            //Right railing
            this.vertices.Add(new Vector3(points.D.x, points.D.y, points.D.z - balconyLength));
            this.vertices.Add(new Vector3(points.C.x, points.C.y, points.C.z - balconyLength));
            this.vertices.Add(new Vector3(points.C.x, points.C.y, points.C.z));
            this.vertices.Add(new Vector3(points.D.x, points.D.y, points.D.z));

            this.tris.Add(vertOffset);
            this.tris.Add(vertOffset + 1);
            this.tris.Add(vertOffset + 2);

            this.tris.Add(vertOffset + 2);
            this.tris.Add(vertOffset + 3);
            this.tris.Add(vertOffset);

            this.tris.Add(vertOffset + 2);
            this.tris.Add(vertOffset + 1);
            this.tris.Add(vertOffset);

            this.tris.Add(vertOffset);
            this.tris.Add(vertOffset + 3);
            this.tris.Add(vertOffset + 2);
        }

        public void Draw()
        {


            this.material = new Material(Shader.Find("Diffuse"));
            this.material.color = Color.green;

            base.Draw();
        }
    }
}