using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Building
{

    public class Door : Drawable
    {

        public float frameSize;

        public void Init()
        {
            this.vertices = new List<Vector3>();
            this.tris = new List<int>();
            this.uvs = new List<Vector2>();
        }

        public void AddDoor(Rect points, int dType = 0)
        {
            int vertOffset = this.vertices.Count;
            this.vertices.Add(points.A + new Vector3(0f, 0f, frameSize));
            this.vertices.Add(points.B + new Vector3(0f, 0f, frameSize));
            this.vertices.Add(points.C + new Vector3(0f, 0f, frameSize));
            this.vertices.Add(points.D + new Vector3(0f, 0f, frameSize));

            float uvOffset = (float)dType / 4;

            this.AddUVs(new Vector2(uvOffset, 0.25f));

            this.tris.Add(vertOffset);
            this.tris.Add(vertOffset + 1);
            this.tris.Add(vertOffset + 2);

            this.tris.Add(vertOffset + 2);
            this.tris.Add(vertOffset + 3);
            this.tris.Add(vertOffset);
        }

        public void Draw()
        {

           // this.material = new Material(Shader.Find("Diffuse"));
           // this.material.color = Color.red;

            base.Draw();
        }
    }
}