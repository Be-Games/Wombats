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

    public bool isEnemy;
    
    [System.Serializable]
    public class CarWheels
    {
        public GameObject[] wheels;
    }
    [System.Serializable]
    public class BodyTrigger
    {
        public GameObject body;
        public GameObject trigger;
    }
    
    [System.Serializable]
    public class CarSpeedSettings
    {
        public float Acc;
        public float Dec;
        public float normalSpeed;
        public float boostSpeed;
    }
    
    
    
    [Header("Mis Settings")] 
    public Color universalCarColor;

    public CarSpeedSettings carSpeedSettings;
    public BodyTrigger bodyTrigger;
    public CarWheels carWheels;

}
