﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Building
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class Drawable : MonoBehaviour
    {

        public List<Vector3> vertices;
        public List<int> tris;
        public List<Vector2> uvs;
        public MeshFilter meshFilter;
        public Material material;
        private Mesh mesh;

        public MeshRenderer renderer;

        void Awake()
        {
            
        }

        public void AddRect(Rect points)
        {

        }

        public void Draw()
        {
            this.meshFilter = GetComponent<MeshFilter>();
            this.renderer = GetComponent<MeshRenderer>();

            mesh = new Mesh();            
            mesh.vertices = this.vertices.ToArray();
            mesh.triangles = this.tris.ToArray();
            if(this.uvs!=null)
                mesh.uv = this.uvs.ToArray();

            mesh.Optimize();
            mesh.RecalculateNormals();

            if(material==null) material = new Material(Shader.Find("Diffuse"));
            renderer.material = material;

            this.meshFilter.mesh = mesh;
            
        }

        // use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}