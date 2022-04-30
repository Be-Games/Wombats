using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    
    [System.Serializable]
    public class CarEffects
    {
        public GameObject carTrailLineGO;
        public GameObject carBreakGO;
        public ParticleSystem NOSEffectsPS;
        public GameObject breakLight;
        public GameObject headLight;
        public ParticleSystem boostCapturedEffectPS;
        public ParticleSystem boostActivatedEffect;
    }
    
    [System.Serializable]
    public class PostCrashStuff
    {
        public ParticleSystem crashPS;
        public GameObject down_car;
        public GameObject up_car;
    }
    
    [System.Serializable]
    public class OverHeadBoostUI
    {
        public TextMeshProUGUI timerText;
        public Slider boostSlider;
    }
    
    
    
    [Header("Mis Settings")] 
    public Color universalCarColor;
    public CarSpeedSettings carSpeedSettings;
    public BodyTrigger bodyTrigger;
    public CarWheels carWheels;
    public CarEffects carEffects;
    public PostCrashStuff postCrashStuff;
    public OverHeadBoostUI overHeadBoostUI;
    
    private GameManager _gameManager;

    
}
