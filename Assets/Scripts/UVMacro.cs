using UnityEngine;
using System.Collections;
namespace Building
{
     public static class UV 
    {
        public static Vector2 MainWall
        {
        get { return new Vector2(0f,0.75f); }
        }
        public static Vector2 AdditionalWall1
        {
            get { return new Vector2(0.25f, 0.75f); }
        }
        public static Vector2 AdditionalWall2
        {
            get { return new Vector2(0.5f, 0.75f); }
        }
        public static Vector2 AdditionalWall3
        {
            get { return new Vector2(0.75f, 0.75f); }
        }

        public static Vector2 Window(int id)
        {
            return new Vector2(id * 0.25f , 0.5f);
        }

        public static Vector2 Window1
        {
            get { return new Vector2(0f, 0.5f); }
        }
        public static Vector2 Window2
        {
            get { return new Vector2(0.25f, 0.5f); }
        }
        public static Vector2 Window3
        {
            get { return new Vector2(0.5f, 0.5f); }
        }
        public static Vector2 Window4
        {
            get { return new Vector2(0.75f, 0.5f); }
        }

        public static Vector2 Door1
        {
            get { return new Vector2(0f, 0.25f); }
        }
        public static Vector2 Door2
        {
            get { return new Vector2(0.25f, 0.25f); }
        }
        public static Vector2 Door3
        {
            get { return new Vector2(0.5f, 0.25f); }
        }
        public static Vector2 Door4
        {
            get { return new Vector2(0.75f, 0.25f); }
        }

        public static Vector2 Rail
        {
            get { return new Vector2(0f, 0f); }
        }
        public static Vector2 Roof
        {
            get { return new Vector2(0.25f, 0f); }
        }
        public static Vector2 Detail
        {
            get { return new Vector2(0.5f, 0f); }
        }
        public static Vector2 BareWall
        {
            get { return new Vector2(0.75f, 0f); }
        }
    }
}