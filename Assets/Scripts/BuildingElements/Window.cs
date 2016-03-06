using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Building
{
    public class Window : Drawable
    {
        public Vector3 PointA;
        public Vector3 PointB;
        public Vector3 PointC;
        public Vector3 PointD;

        public float windowFrameSize;
        public void Init()
        {
            this.vertices = new List<Vector3>();
            this.tris = new List<int>();
        }

        public void AddWindow(Rect points)
        {
            int vertOffset = this.vertices.Count;
            this.vertices.Add(points.A + new Vector3(0f,0f, windowFrameSize));
            this.vertices.Add(points.B + new Vector3(0f, 0f, windowFrameSize));
            this.vertices.Add(points.C + new Vector3(0f, 0f, windowFrameSize));
            this.vertices.Add(points.D + new Vector3(0f, 0f, windowFrameSize));

            this.tris.Add(vertOffset);
            this.tris.Add(vertOffset + 1);
            this.tris.Add(vertOffset + 2);

            this.tris.Add(vertOffset + 2);
            this.tris.Add(vertOffset + 3);
            this.tris.Add(vertOffset);
        }

        public void Draw()
        {           
           
            this.material = new Material(Shader.Find("Diffuse"));
            this.material.color = Color.blue;

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