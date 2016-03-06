using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Building
{

    public class Panel : Drawable
    {

        public Vector3 PointA;
        public Vector3 PointB;
        public Vector3 PointC;
        public Vector3 PointD;

        public void Draw()
        {
            this.vertices = new List<Vector3>();
            this.tris = new List<int>();

            this.vertices.Add(PointA);
            this.vertices.Add(PointB);
            this.vertices.Add(PointC);
            this.vertices.Add(PointD);

            this.tris.Add(0);
            this.tris.Add(1);
            this.tris.Add(2);

            this.tris.Add(2);
            this.tris.Add(3);
            this.tris.Add(0);

            base.Draw();
        }

    }
}