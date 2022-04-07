using System.Collections;
using System.Collections.Generic;
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
    public GameObject crashedPanel;
    public GameObject extraLifePanel;
    public GameObject pauseScreen;
    public GameObject postAdCrashPanel;

    public TextMeshProUGUI playerPosition;

}
