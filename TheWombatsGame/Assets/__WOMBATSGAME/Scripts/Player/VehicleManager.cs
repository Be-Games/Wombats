using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleManager : MonoBehaviour
{
    private static VehicleManager _instance;

    public static VehicleManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    
    

    [System.Serializable]
    public class CarWheels
    {
        public GameObject[] wheels;
    }

    
}
