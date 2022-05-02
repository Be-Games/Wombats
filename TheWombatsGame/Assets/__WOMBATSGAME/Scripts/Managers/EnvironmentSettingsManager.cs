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

        /*if (LevelManager.Instance._playerVehicleManager != null)
        {
            LevelManager.Instance._playerVehicleManager.carEffects.headLight.SetActive(true);
        }*/
        
        /*LevelManager.Instance.enemyLeftVisual.GetComponent<VehicleManager>().carEffects.headLight.SetActive(true);
        LevelManager.Instance.enemyRightVisual.GetComponent<VehicleManager>().carEffects.headLight.SetActive(true);*/
        
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
        
        /*LevelManager.Instance._playerVehicleManager.carEffects.headLight.SetActive(false);
        LevelManager.Instance.enemyLeftVisual.GetComponent<VehicleManager>().carEffects.headLight.SetActive(false);
        LevelManager.Instance.enemyRightVisual.GetComponent<VehicleManager>().carEffects.headLight.SetActive(false);*/
        
        RenderSettings.fogMode = FogMode.Exponential;
        RenderSettings.fogColor = dayStuff.fogColor;
        RenderSettings.ambientLight = dayStuff.ambientColor;
        RenderSettings.skybox = dayStuff.daySkyboxMat;
        RenderSettings.fogDensity = dayStuff.fogDensity;
    }

    public void Clear()
    {
        foreach (var we in LevelManager.Instance.allEffects)
        {
            if(we !=null)
                 we.gameObject.SetActive(false);
        }
        foreach (var we in LevelManager.Instance.allEffects2)
        {
            if(we != null)
                we.gameObject.SetActive(false);
        }
    }
    
    public void Rain()
    {
        LevelManager.Instance.allEffects[0].gameObject.SetActive(true);
        LevelManager.Instance.allEffects2[0].gameObject.SetActive(true);
    }
    
    public void Snow()
    {
        LevelManager.Instance.allEffects[1].gameObject.SetActive(true);
        LevelManager.Instance.allEffects2[1].gameObject.SetActive(true);
    }
    
    public void Fog()
    {
        LevelManager.Instance.allEffects[2].gameObject.SetActive(true);
        LevelManager.Instance.allEffects2[2].gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        Clear();
    }
}
