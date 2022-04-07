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
    
    // public List<WeatherEffects> weatherEffects;
    //
    // [System.Serializable]
    // public class WeatherEffects
    // {
    //     public GameObject weatherEffect;
    //     public string nameEffect;
    // }

    public GameObject[] weatherEffects;  //0 - rain , 1 - snow , 2 - fog . 3- Windy
    
    private void Start()
    {
        if(GameManager.Instance.lightingMode == 1)
            DayMode();
        if(GameManager.Instance.lightingMode == 2)
            NightMode();
    }

    public void NightMode()
    {
        nightLightingGO.SetActive(true);
        dayLightingGO.SetActive(false);
        RenderSettings.fogDensity = 0.017f;
    }
    
    public void DayMode()
    {
        nightLightingGO.SetActive(false);
        dayLightingGO.SetActive(true);
        RenderSettings.fogDensity = 0f;
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
