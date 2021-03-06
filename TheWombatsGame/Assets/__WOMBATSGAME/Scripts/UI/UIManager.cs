using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public GameObject flyThruPanel;
    public GameObject gameUIPanel;
    public GameObject crashedPanel;
    public GameObject extraLifePanel;
    public RectTransform pauseScreen;
    public GameObject postAdCrashPanel;
    public GameObject receiveLifePanel;
    
    public TextMeshProUGUI playerPosition;

    public TextMeshProUGUI flyThroughCamCityName;
    

    [Header("Scoring Stuff")] 
    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI currentTrackName;

    [Header("Fly Over Variables")] 
    public Image[] blackImages;

    public RectTransform playerProgressLine,enemyLProgressLine,enemyRightProgressLine;
    public GameObject wombatLogoPlayer, wombatLogoenemyLeft, wombatLogoenemyRight;

    [Header("Crashed Panel")] 
    public GameObject continueBtn, restartBtn, rewardBtn,nothanksBtn;
    
    [Header("Win Screen")]
    public TextMeshProUGUI playerPosTXT;
    public GameObject positionTexts;
    public RectTransform raceFinishFadePanel,continueButton,shareBtn;

    public Image fullHeart, brokenHeart;
    //DO TWEEN ANIMATION UIS


    public GameObject redCrashedPanel;
    public GameObject boostBlurPanel;

    public TextMeshProUGUI resumeTimer;

    public Image sfxon, sfxoff, musicon, musicoff, hapticon, hapticoff;
    
    public TextMeshProUGUI playerOriginalPosi, playerOriginalSubText;

    public string[] allTips;
    public TextMeshProUGUI tips_txt;

    public TextMeshProUGUI cityName;

    public GameObject brakeBtn;
    public TMP_Dropdown controlToggleDD;
    public TMP_Dropdown graphicsToggleDD;
    
    private void OnEnable()
    {
        foreach (var VARIABLE in blackImages)
        {
            VARIABLE.gameObject.SetActive(true);
        }
        
        //blur boost panel off
        boostBlurPanel.SetActive(false);

        //Ini tips text with 1 random tip
        tips_txt.text = allTips[UnityEngine.Random.Range(0, allTips.Length)];

        
        if (PlayerPrefs.GetInt("Control") == 1)
        {
            brakeBtn.SetActive(true);
        }
        else if(PlayerPrefs.GetInt("Control") == 0)
        {
            brakeBtn.SetActive(false);
        }
        
        controlToggleDD.value = PlayerPrefs.GetInt("Control");

       
        
    }

    private void Start()
    {
        graphicsToggleDD.value =  PlayerPrefs.GetInt("Graphics");
        //Debug.Log( PlayerPrefs.GetInt("Graphics"));
    }

    public void settingsBtnDT()
    {
        
        pauseScreen.DOAnchorPos(Vector2.zero, 0.25f).SetEase(Ease.Flash).OnComplete(PauseMenu);
        currentTrackName.text = LevelManager.Instance._audioManager.musicTracks.MusicTrackAudioSource.clip.name;
        cityName.text = SceneManager.GetActiveScene().name;
    }
    
    public void settingsReturnDT()
    {
        Time.timeScale = 1;
        pauseScreen.DOAnchorPos(new Vector2(1500,0f), 0.25f).SetEase(Ease.Flash);
        
        //Random Tip
        tips_txt.text = allTips[UnityEngine.Random.Range(0, allTips.Length)];
    }
    
    
    
    public void PauseMenu()
    {
        Time.timeScale = 0;
        //LevelManager.Instance.isGameStarted = false;
        //GameManager.Instance.canControlCar = false;
    }
    

    public void SpotifyLink()
    {
        Application.OpenURL(LevelManager.Instance._audioManager.musicTracks.spotifyLinks[LevelManager.Instance._audioManager.i]);
    }

    public void FlyOverBlackPanelsTweening()
    {
        blackImages[0].GetComponent<RectTransform>().DOScale(new Vector3(13f,0f,13f), 2f).SetEase(Ease.Flash);
        blackImages[1].GetComponent<RectTransform>().DOScale(new Vector3(13f,0f,13f), 2f).SetEase(Ease.Flash);
    }

    public void LoadLevelSelection()
    {
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().enabled = false;
        
        AudioManager.Instance.musicTracks.MusicTrackAudioSource.Stop();
        Time.timeScale = 1;
        GameManager.Instance.LoadScene("LevelSelection");
    }
    
    public void ButtonClick()
    {
        GameManager.Instance.ButtonClick();
    }

    public void SkipLevel()
    {
        LevelManager.Instance.skip();
    }
    
    void OnApplicationPause(bool pauseStatus)
    {
        if (LevelManager.Instance.isGameStarted)
        {
            /*if (!pauseStatus)
            {
                Debug.Log("Resume");
                Time.timeScale = 1;
                pauseScreen.DOAnchorPos(new Vector2(1500,0f), 0f).SetEase(Ease.Flash);
        
                //Random Tip
                tips_txt.text = allTips[UnityEngine.Random.Range(0, allTips.Length)];
            }*/

            if (pauseStatus)
            {
                Debug.Log("pause");
                pauseScreen.anchoredPosition = Vector2.zero;
                currentTrackName.text = LevelManager.Instance._audioManager.musicTracks.MusicTrackAudioSource.clip.name;
                cityName.text = SceneManager.GetActiveScene().name;
                Time.timeScale = 0;
            } 
        }
        
    }

    public void ToggleControls()
    {
        if (controlToggleDD.value == 0)
            PlayerPrefs.SetInt("Control", 0);
        
        if (controlToggleDD.value == 1)
            PlayerPrefs.SetInt("Control", 1);
        
        if (PlayerPrefs.GetInt("Control") == 1)
        {
            brakeBtn.SetActive(true);
        }
        else if(PlayerPrefs.GetInt("Control") == 0)
        {
            brakeBtn.SetActive(false);
        }
    }

    public void BreakBtn()
    {
        LevelManager.Instance._gameControls.gestureState = GameControls.GestureState.Break;
    }

    public void ChangeGraphics()
    {
        if (graphicsToggleDD.value == 0)
        {
            QualitySettings.SetQualityLevel(2);
        }
        
        if (graphicsToggleDD.value == 1)
        {
            QualitySettings.SetQualityLevel(3);
        }
        
        PlayerPrefs.SetInt("Graphics", graphicsToggleDD.value);
    }

}
