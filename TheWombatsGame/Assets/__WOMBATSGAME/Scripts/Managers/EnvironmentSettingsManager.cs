using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSettingsManager : MonoBehaviour
{
    private static EnvironmentSettingsManager _instance;

    public static EnvironmentSettingsManager Instance
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
    
    
    public GameObject nightLightingGO,dayLightingGO;
    public GameObject dayObstacles, nightObstacles;

    [System.Serializable]
    public class DayStuff
    {
        public Material daySkyboxMat;
        public Color fogColor;
        public Color ambientColor;
        public float fogDensity;
    }
    
    [System.Serializable]
    public class NightStuff
    {
        public Material nightSkyboxMat;
        public Color fogColor;
        public Color ambientColor;
        public float fogDensity;
    }

    public GameObject[] weatherEffects;  //0 - rain , 1 - snow , 2 - fog . 3- Windy
    public DayStuff dayStuff;
    public NightStuff nightStuff;
    
    
    private void Start()
    {
        if(GameManager.Instance.lightingMode == 1)
            DayMode();
        if(GameManager.Instance.lightingMode == 2)
            NightMode();
    }

    public void NightMode()
    {
        nightObstacles.SetActive(true);
        dayObstacles.SetActive(false);
        nightLightingGO.SetActive(true);
        dayLightingGO.SetActive(false);
        
        RenderSettings.fogMode = FogMode.Exponential;
        RenderSettings.fogColor = nightStuff.fogColor;
        RenderSettings.ambientLight = nightStuff.ambientColor;
        RenderSettings.skybox = nightStuff.nightSkyboxMat;
        RenderSettings.fogDensity = nightStuff.fogDensity;
    }
    
    public void DayMode()
    {
        dayObstacles.SetActive(true);
        nightObstacles.SetActive(false);
        nightLightingGO.SetActive(false);
        dayLightingGO.SetActive(true);
        
        RenderSettings.fogMode = FogMode.Exponential;
        RenderSettings.fogColor = dayStuff.fogColor;
        RenderSettings.ambientLight = dayStuff.ambientColor;
        RenderSettings.skybox = dayStuff.daySkyboxMat;
        RenderSettings.fogDensity = dayStuff.fogDensity;
    }

    public void Clear()
    {
        foreach (var we in weatherEffects)
        {
            we.SetActive(false);
        }
    }
    
    public void Rain()
    {
        for (int i = 0; i < 4; i++)
        {
            if(i == 0)
                weatherEffects[i].SetActive(true);
            else
            {
                weatherEffects[i].SetActive(false);
            }   
        }
    }
    
    public void Snow()
    {
        for (int i = 0; i < 4; i++)
        {
            if(i == 1)
                weatherEffects[i].SetActive(true);
            else
            {
                weatherEffects[i].SetActive(false);
            }   
        }
    }
    
    public void Fog()
    {
        for (int i = 0; i < 4; i++)
        {
            if(i == 2)
                weatherEffects[i].SetActive(true);
            else
            {
                weatherEffects[i].SetActive(false);
            }   
        }
    }
    
    public void Windy()
    {
        for (int i = 0; i < 4; i++)
        {
            if(i == 3)
                weatherEffects[i].SetActive(true);
            else
            {
                weatherEffects[i].SetActive(false);
            }   
        }
    }
    
}
