using UnityEngine;
using System.Collections;
using Building;

namespace Estate
{
    public class EstateBase : MonoBehaviour
    {

        public string name;
        public Vector3 worldPosition;
        public UnityEngine.Rect outerBounds;
        public UnityEngine.Rect buildingBounds;

        public BuildingBase building;

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(this.transform.position + new Vector3(outerBounds.x, this.transform.position.y, outerBounds.y),
                            this.transform.position + new Vector3(outerBounds.x + outerBounds.width, this.transform.position.y, outerBounds.y));

            Gizmos.DrawLine(this.transform.position + new Vector3(outerBounds.x + outerBounds.width, this.transform.position.y, outerBounds.y),
                            this.transform.position + new Vector3(outerBounds.x + outerBounds.width, this.transform.position.y, outerBounds.y + outerBounds.height));
            
            Gizmos.DrawLine(this.transform.position + new Vector3(outerBounds.x + outerBounds.width, this.transform.position.y, outerBounds.y + outerBounds.height),
                            this.transform.position + new Vector3(outerBounds.x, this.transform.position.y, outerBounds.y + outerBounds.height));

            Gizmos.DrawLine(this.transform.position + new Vector3(outerBounds.x, this.transform.position.y, outerBounds.y + outerBounds.height),
                            this.transform.position + new Vector3(outerBounds.x, this.transform.position.y, outerBounds.y));

           
            Gizmos.color = Color.red;
            Gizmos.DrawLine(this.transform.position + new Vector3(buildingBounds.x, this.transform.position.y, buildingBounds.y),
                            this.transform.position + new Vector3(buildingBounds.x + buildingBounds.width, this.transform.position.y, buildingBounds.y));

            Gizmos.DrawLine(this.transform.position + new Vector3(buildingBounds.x + buildingBounds.width, this.transform.position.y, buildingBounds.y),
                            this.transform.position + new Vector3(buildingBounds.x + buildingBounds.width, this.transform.position.y, buildingBounds.y + buildingBounds.height));

            Gizmos.DrawLine(this.transform.position + new Vector3(buildingBounds.x + buildingBounds.width, this.transform.position.y, buildingBounds.y + buildingBounds.height),
                            this.transform.position + new Vector3(buildingBounds.x, this.transform.position.y, buildingBounds.y + buildingBounds.height));

            Gizmos.DrawLine(this.transform.position + new Vector3(buildingBounds.x, this.transform.position.y, buildingBounds.y + buildingBounds.height),
                            this.transform.position + new Vector3(buildingBounds.x, this.transform.position.y, buildingBounds.y));

            if (building==null)
            {
                Gizmos.color = new Color(1f, 1f, 1f, 0.5f);
                Gizmos.DrawCube(this.transform.position + new Vector3(buildingBounds.x + (buildingBounds.width / 2), this.transform.position.y + 7.5f, buildingBounds.y + (buildingBounds.height / 2)),
                                new Vector3(buildingBounds.width, 15f, buildingBounds.height));
            }
            Gizmos.color = Color.white;
        }

        public void KillAllChildren() // :D
        {
            int children = transform.childCount;
            for (int i = children - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }


        public void CreateGrass()
        {

        }


        public void DrawEstate()
        {
            worldPosition = this.transform.position;
            KillAllChildren();
            //Building Generation
            GameObject buildingObject = new GameObject();
            buildingObject.name = string.Format("{0} Building", this.name);
            buildingObject.transform.parent = this.transform;
            buildingObject.transform.localPosition = new Vector3(buildingBounds.x, this.transform.position.y, buildingBounds.y);
            
            building = buildingObject.AddComponent<BuildingBase>();

            building.PointA = new Vector3(0, 0f, 0);
            building.PointB = new Vector3(buildingBounds.width, 0f, 0);
            building.PointC = new Vector3(buildingBounds.width, 0f,buildingBounds.height);
            building.PointD = new Vector3(0, 0f, buildingBounds.height);

            building.height = 15f;
            building.width = 15f;
            building.depth = 20f;

            building.foundationHeight = 0.5f;

            building.windowFrameSize = 0.1f;
            building.windowSize = new Vector2(1f, 1.2f);
            building.windowOffset = 1f;
            building.balconyLength = 0.5f;

            building.buildingMaterial = Resources.Load("Materials/building") as Material;

            building.DrawBuilding();

            //Everything else
            CreateGrass();
        }
        
    }
}