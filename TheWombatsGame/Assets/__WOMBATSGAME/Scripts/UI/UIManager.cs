using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance
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
    
    [Header("UI Assets")] 
    public GameObject StatusIndicatorPanelGO;
    public TextMeshProUGUI position;
    public GameObject BoostBtn;

    [Header("Game Panels")] 
    public GameObject gameUIPanel;
    public GameObject crashedPanel;
    public GameObject extraLifePanel;
    public RectTransform pauseScreen;
    public GameObject postAdCrashPanel;

    public TextMeshProUGUI playerPosition;

    public TextMeshProUGUI flyThroughCamCityName;
    
    //DO TWEEN ANIMATION UIS
    
    public void settingsBtnDT()
    {
        pauseScreen.DOAnchorPos(Vector2.zero, 0.25f);
        
    }
    
    public void settingsReturnDT()
    {
        pauseScreen.DOAnchorPos(new Vector2(1500,0f), 0.25f);
    }
    
    
    
    public void PauseMenu()
    {
        LevelManager.Instance.isGameStarted = false;
        GameManager.Instance.canControlCar = false;
    }

}
