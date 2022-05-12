using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
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
    }

    public void settingsBtnDT()
    {
        
        pauseScreen.DOAnchorPos(Vector2.zero, 0.25f).SetEase(Ease.Flash).OnComplete(PauseMenu);
        currentTrackName.text = LevelManager.Instance._audioManager.musicTracks.MusicTrackAudioSource.clip.name;
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

}
