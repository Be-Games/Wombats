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
    
    [Header("Car Settings")]
    public float Acc;
    public float Dec;
    public float targetSpeed;
    

    [Header("Mis Settings")] 
    public Color universalCarColor;
    
    
    
}
