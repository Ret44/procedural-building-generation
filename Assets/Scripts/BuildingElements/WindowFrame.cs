using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Building
{
    public class WindowFrame : Drawable
    {
        public float windowFrameSize;

        public void Init()
        {
            this.vertices = new List<Vector3>();
            this.tris = new List<int>();
            this.uvs = new List<Vector2>();
        }

        public void AddFrame(Rect points)
        {
            int vertOffset = this.vertices.Count;

            this.vertices.Add(points.A);
            this.vertices.Add(points.B);
            this.vertices.Add(points.B + new Vector3(0f,0f,windowFrameSize));
            this.vertices.Add(points.A + new Vector3(0f, 0f, windowFrameSize));

            this.AddUVs(new Vector2(0.25f,0.75f));

            this.tris.Add(vertOffset);
            this.tris.Add(vertOffset + 1);
            this.tris.Add(vertOffset + 2);

            this.tris.Add(vertOffset + 2);
            this.tris.Add(vertOffset + 3);
            this.tris.Add(vertOffset);
            vertOffset += 4;

            this.vertices.Add(points.B);
            this.vertices.Add(points.C);
            this.vertices.Add(points.C + new Vector3(0f, 0f, windowFrameSize));
            this.vertices.Add(points.B + new Vector3(0f, 0f, windowFrameSize));

            this.AddUVs(new Vector2(0.25f, 0.75f));

            this.tris.Add(vertOffset);
            this.tris.Add(vertOffset + 1);
            this.tris.Add(vertOffset + 2);

            this.tris.Add(vertOffset + 2);
            this.tris.Add(vertOffset + 3);
            this.tris.Add(vertOffset);
            vertOffset += 4;

            this.vertices.Add(points.C);
            this.vertices.Add(points.D);
            this.vertices.Add(points.D + new Vector3(0f, 0f, windowFrameSize));
            this.vertices.Add(points.C + new Vector3(0f, 0f, windowFrameSize));

            this.AddUVs(new Vector2(0.25f, 0.75f));

            this.tris.Add(vertOffset);
            this.tris.Add(vertOffset + 1);
            this.tris.Add(vertOffset + 2);

            this.tris.Add(vertOffset + 2);
            this.tris.Add(vertOffset + 3);
            this.tris.Add(vertOffset);
            vertOffset += 4;

            this.vertices.Add(points.D - new Vector3(0f, 0f, windowFrameSize/2));
            this.vertices.Add(points.A - new Vector3(0f, 0f, windowFrameSize/2));
            this.vertices.Add(points.A + new Vector3(0f, 0f, windowFrameSize));
            this.vertices.Add(points.D + new Vector3(0f, 0f, windowFrameSize));

            this.AddUVs(new Vector2(0.25f, 0.75f));

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
           // this.material.color = Color.yellow;


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