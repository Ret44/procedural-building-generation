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
            newSettlementBase.data = style;
            newSettlementBase.manager = this;
            
            newSettlementObj.name = "NewSettlement";
            
            settlements.Add(newSettlementBase);

            return newSettlementObj;
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