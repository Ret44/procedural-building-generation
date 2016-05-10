//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;

//[ExecuteInEditMode]
//public class DistrictManager : MonoBehaviour
//{

//    private List<SettlementData> _districts;
//    public List<SettlementData> districts
//    {
//        get
//        {
//            if (_districts == null) _districts = new List<SettlementData>();
//            return _districts;
//        }
//    }

//    private static DistrictManager _instance;
//    public static DistrictManager instance
//    {
//        get
//        {
//            if (_instance == null)
//            {
//                DistrictManager[] allInstances = FindObjectsOfType<DistrictManager>();
//                _instance = allInstances[0];
//            }

//            if (_instance == null)
//            {
//                throw new System.Exception(); //TODO: Create exception
//            }

//            return _instance;
//        }
//    }
//    public static SettlementData defaultData;

//    public void OnEnable()
//    {
//        gameObject.hideFlags = HideFlags.None;
//    }

//    public static SettlementData GetDistrict(int ind)
//    {
//        //if (instance.districts != null)
//        //    return instance.districts[ind];
//        //else {
//        //    if (defaultData == null) defaultData = new SettlementData("Default", Color.white);
//        //    return defaultData;
//        //}

//        return null;
//    }

//    public static void AddDistrict()
//    {
//        instance.districts.Add(new SettlementData());
//    }

//    public static void RemoveDistrict(int ind)
//    {
//        instance.districts.RemoveAt(ind);
//    }

//    public static string[] GetOptions()
//    {
//        string[] options = new string[instance.districts.Count];
//        for (int i = 0; i < instance.districts.Count; i++) options[i] = instance.districts[i].name;
//        return options;
//    }

//    // Use this for initialization
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }


//}
