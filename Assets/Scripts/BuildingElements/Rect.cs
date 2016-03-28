using UnityEngine;
using System.Collections;

namespace Building
{
    [System.Serializable]
    public class Rect
    {
        public Vector3 A;
        public Vector3 B;
        public Vector3 C;
        public Vector3 D;

        public Rect()
        {
            A = Vector3.zero;
            B = Vector3.zero;
            C = Vector3.zero;
            D = Vector3.zero;
        }
        public Rect(Vector3 A, Vector3 B, Vector3 C, Vector3 D)
        {
            this.A = A;
            this.B = B;
            this.C = C;
            this.D = D;
        }
    }
}