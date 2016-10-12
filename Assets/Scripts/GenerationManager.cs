using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Settlement
{
    [RequireComponent(typeof(StyleManager))]
    [RequireComponent(typeof(SettlementData))]
    public class GenerationManager : MonoBehaviour
    {
        
        public SettlementData defaultStyle;

        public StyleManager styleManager;

        public List<SettlementBase> settlements;

        public GameObject createNewSettlement(SettlementData style)
        {
            GameObject newSettlementObj = new GameObject();
            SettlementBase newSettlementBase = newSettlementObj.AddComponent<SettlementBase>();
            if (defaultStyle == null)
                defaultStyle = GetComponent<SettlementData>();
            newSettlementBase.transform.position = Vector3.zero; //TODO: USTAWIĆ NA WPROST KAMERY
            newSettlementBase.data = style;
            newSettlementBase.manager = this;
            
            newSettlementObj.name = "NewSettlement";
            
            settlements.Add(newSettlementBase);

            return newSettlementObj;
        }
       

        public GameObject cloneSettlement(SettlementBase settlement)
        {
            GameObject newSettlementObj = Instantiate(settlement.gameObject, Vector3.zero, Quaternion.identity) as GameObject;

            settlements.Add(newSettlementObj.GetComponent<SettlementBase>());

            return newSettlementObj;
        }

        public void refreshStyle(SettlementData style)
        {
            for(int i=0; i < settlements.Count ; i++)
            {
                if(settlements[i].data == style)
                {
                    settlements[i].DestroyBuildings();
                    settlements[i].GenerateBuildings();
                }
            }
        }

        public void deleteSettlement(SettlementBase settlement)
        {
            settlements.Remove(settlement);
            DestroyImmediate(settlement.gameObject);
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