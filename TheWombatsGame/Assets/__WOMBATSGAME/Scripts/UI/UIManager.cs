using System;
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

    [Header("Scoring Stuff")] 
    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI currentTrackName;
    
    //DO TWEEN ANIMATION UIS
    
    public void settingsBtnDT()
    {
        
        pauseScreen.DOAnchorPos(Vector2.zero, 0.25f).SetEase(Ease.Flash).OnComplete(PauseMenu);
    }
    
    public void settingsReturnDT()
    {
        Time.timeScale = 1;
        pauseScreen.DOAnchorPos(new Vector2(1500,0f), 0.25f).SetEase(Ease.Flash);
    }
    
    
    
    public void PauseMenu()
    {
        Time.timeScale = 0;
        //LevelManager.Instance.isGameStarted = false;
        //GameManager.Instance.canControlCar = false;
    }

    private void Update()
    {
        currentTrackName.text = LevelManager.Instance._audioManager.musicTracks.MusicTrackAudioSource.clip.name;
    }

    public void NextTrack()
    {
        LevelManager.Instance._audioManager.i++;
        if (LevelManager.Instance._audioManager.i > LevelManager.Instance._audioManager.musicTracks.tracks.Length)
        {
            LevelManager.Instance._audioManager.i = 0;
        }
        LevelManager.Instance._audioManager.GameSwitchMusicCalled();
    }
    
    public void PrevTrack()
    {
        LevelManager.Instance._audioManager.i--;
        if (LevelManager.Instance._audioManager.i < 0)
        {
            LevelManager.Instance._audioManager.i = LevelManager.Instance._audioManager.musicTracks.tracks.Length-1;
        }
        LevelManager.Instance._audioManager.GameSwitchMusicCalled();
    }

    public void SpotifyLink()
    {
        Application.OpenURL(LevelManager.Instance._audioManager.musicTracks.spotifyLinks[LevelManager.Instance._audioManager.i]);
    }

}
